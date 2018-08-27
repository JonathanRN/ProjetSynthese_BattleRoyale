using System;
using Playmode.Ennemy.BodyParts;
using Playmode.Ennemy.Strategies;
using Playmode.Entity.Destruction;
using Playmode.Entity.Senses;
using Playmode.Entity.Status;
using Playmode.Movement;
using UnityEngine;

namespace Playmode.Ennemy
{
    public class EnnemyController : MonoBehaviour
    {
        [Header("Body Parts")] [SerializeField] private GameObject body;
        [SerializeField] private GameObject hand;
        [SerializeField] private GameObject sight;
        [SerializeField] private GameObject typeSign;
        [Header("Type Images")] [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite carefulSprite;
        [SerializeField] private Sprite cowboySprite;
        [SerializeField] private Sprite camperSprite;
        [Header("Behaviour")] [SerializeField] private GameObject startingWeaponPrefab;

		[Header("Variables")]
		[SerializeField]
		public float outOfRangeRotationSpeed = 5f;
		[SerializeField]
		public float cameraHalfHeight = 8.5f;
		[SerializeField]
		public float cameraHalfWidth = 20.5f;
		[SerializeField]
		public float speed = 10f;
		public float senseRotation = 1f;
        public bool onFire;

        private float randomBehaviour;
		public Health HealthPoints { get; set; }
        private Mover mover;
        private Destroyer destroyer;
        private EnnemySensor ennemySensor;
        private HitSensor hitSensor;
		private PickableSensor pickableSensor;
        private HandController handController;
        private Transform transformer;
        private TimedRotation timedRotation;
		private Vector3 vectorBetweenEnemy;
       

		private IEnnemyStrategy strategy;

        private void Awake()
        {
            ValidateSerialisedFields();
            InitializeComponent();
            CreateStartingWeapon();
        }

        private void ValidateSerialisedFields()
        {
            if (body == null)
                throw new ArgumentException("Body parts must be provided. Body is missing.");
            if (hand == null)
                throw new ArgumentException("Body parts must be provided. Hand is missing.");
            if (sight == null)
                throw new ArgumentException("Body parts must be provided. Sight is missing.");
            if (typeSign == null)
                throw new ArgumentException("Body parts must be provided. TypeSign is missing.");
            if (normalSprite == null)
                throw new ArgumentException("Type sprites must be provided. Normal is missing.");
            if (carefulSprite == null)
                throw new ArgumentException("Type sprites must be provided. Careful is missing.");
            if (cowboySprite == null)
                throw new ArgumentException("Type sprites must be provided. Cowboy is missing.");
            if (camperSprite == null)
                throw new ArgumentException("Type sprites must be provided. Camper is missing.");
            if (startingWeaponPrefab == null)
                throw new ArgumentException("StartingWeapon prefab must be provided.");
        }

        private void InitializeComponent()
        {
            HealthPoints = GetComponent<Health>();
            mover = GetComponent<RootMover>();
            destroyer = GetComponent<RootDestroyer>();
            transformer = transform.root;
            timedRotation = GetComponent<TimedRotation>();

            var rootTransform = transform.root;
            ennemySensor = rootTransform.GetComponentInChildren<EnnemySensor>();
            hitSensor = rootTransform.GetComponentInChildren<HitSensor>();
			pickableSensor = rootTransform.GetComponentInChildren<PickableSensor>();
            handController = hand.GetComponent<HandController>();

			strategy = new CarefulStrategy(mover, handController, ennemySensor, transformer, timedRotation, this,pickableSensor);
		}

        private void CreateStartingWeapon()
        {
            handController.Hold(Instantiate(
                startingWeaponPrefab,
                Vector3.zero,
                Quaternion.identity
            ));
        }

		private void OnEnable()
		{
			timedRotation.OnRotationChanged += OnRotationChanged;
			hitSensor.OnHit += OnHit;
			HealthPoints.OnDeath += OnDeath;
			pickableSensor.OnPickUp += OnPickUp;
		}

		private void OnRotationChanged()
		{
            onFire = false;
            randomBehaviour = UnityEngine.Random.Range(-1, 2);
			senseRotation *= -1;
		}

		private void Update()
        {
            strategy.Act();
        }

		public void MoveTowardsTarget(Transform target)
		{
			mover.Move(new Vector3(0, 3));
            RotateTowardsTarget(target);
		}

        public void RotateTowardsTarget(Transform target)
        {
            vectorBetweenEnemy = new Vector3(transformer.position.x - target.transform.position.x, transformer.position.y - target.transform.position.y);
            if (Vector3.Dot(vectorBetweenEnemy, transformer.right) < -0.5)
            {
                mover.Rotate(1f * Time.deltaTime);
            }
            else if (Vector3.Dot(vectorBetweenEnemy, transformer.right) > 0.5)
            {
                mover.Rotate(-1f * Time.deltaTime);
            }
        }

		public void Roam()
		{
			mover.Move(new Vector3(0, speed * Time.deltaTime));

            if (transformer.position.y >= cameraHalfHeight)
            {
                transformer.rotation = Quaternion.Slerp(transformer.rotation, Quaternion.Euler(0, 0, 180), Time.deltaTime * outOfRangeRotationSpeed);
            }
            else if (transformer.position.y <= -cameraHalfHeight)
            {
                transformer.rotation = Quaternion.Slerp(transformer.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * outOfRangeRotationSpeed);
            }
            else if (transformer.position.x >= cameraHalfWidth)
            {
                transformer.rotation = Quaternion.Slerp(transformer.rotation, Quaternion.Euler(0, 0, 90), Time.deltaTime * outOfRangeRotationSpeed);
            }
            else if (transformer.position.x <= -cameraHalfWidth)
            {
                transformer.rotation = Quaternion.Slerp(transformer.rotation, Quaternion.Euler(0, 0, -90), Time.deltaTime * outOfRangeRotationSpeed);
            }
            else
            {
                if (randomBehaviour > 0)
                {
                    mover.Rotate(senseRotation);
                }
            }
        }

        public bool OutOfMapHandler()
        {
            if (transformer.position.y >= cameraHalfHeight + 0.5f)
            {
                transform.SetPositionAndRotation(new Vector3(transformer.position.x, cameraHalfHeight - 0.2f), transformer.rotation);
                return true;
            }
            else if (transformer.position.y <= -cameraHalfHeight - 0.5f)
            {
                transform.SetPositionAndRotation(new Vector3(transformer.position.x, cameraHalfHeight + 0.2f), transformer.rotation);
                return true;
            }
            else if (transformer.position.x >= cameraHalfWidth + 0.5f)
            {
                transform.SetPositionAndRotation(new Vector3(cameraHalfWidth - 0.2f, transformer.position.y), transformer.rotation);
                return true;
            }
            else if (transformer.position.x <= -cameraHalfWidth - 0.5f)
            {
                transform.SetPositionAndRotation(new Vector3(cameraHalfWidth + 0.2f, transformer.position.y), transformer.rotation);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void HitReact()
        {
            mover.Rotate(1f);
        }

		private void OnDisable()
        {
            hitSensor.OnHit -= OnHit;
            HealthPoints.OnDeath -= OnDeath;
			pickableSensor.OnPickUp -= OnPickUp;
		}

		public void Configure(EnnemyStrategy strategy, Color color)
        {
            body.GetComponent<SpriteRenderer>().color = color;
            sight.GetComponent<SpriteRenderer>().color = color;
            
            switch (strategy)
            {
                case EnnemyStrategy.Careful:
                    typeSign.GetComponent<SpriteRenderer>().sprite = carefulSprite;
                    //this.strategy = new CarefulStrategy(mover, handController, ennemySensor, transformer, timedRotation, this, pickableSensor);
                    break;
                case EnnemyStrategy.Cowboy:
                    typeSign.GetComponent<SpriteRenderer>().sprite = cowboySprite;
                    //this.strategy = new NormalStrategy(mover, handController, ennemySensor, transformer, timedRotation, this);
                    break;
                case EnnemyStrategy.Camper:
                    typeSign.GetComponent<SpriteRenderer>().sprite = camperSprite;
                    //this.strategy = new NormalStrategy(mover, handController, ennemySensor, transformer, timedRotation, this);
                    break;
                default:
                    typeSign.GetComponent<SpriteRenderer>().sprite = normalSprite;
                    //this.strategy = new NormalStrategy(mover, handController, ennemySensor, transformer, timedRotation, this);
                    break;
            }
        }

        private void OnHit(int hitPoints)
		{
            HealthPoints.Hit(hitPoints);
            onFire = true;
        }

        private void OnDeath()
        {
            destroyer.Destroy();
        }

		private void OnPickUp(GameObject pickable)
		{
			Debug.Log("Item picked up: " + pickable.GetComponentInChildren<PickableType>().GetType());

			pickable.gameObject.GetComponentInChildren<PickableUse>().Use(gameObject);
			Destroy(pickable.gameObject);
		}
	}
}