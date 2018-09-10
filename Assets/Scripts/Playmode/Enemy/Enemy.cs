using System;
using System.Collections;
using Playmode.Enemy.BodyParts;
using Playmode.Enemy.Strategies;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Destruction;
using Playmode.Entity.Movement;
using Playmode.Entity.Senses;
using Playmode.Entity.Status;
using Playmode.Environment;
using Playmode.Pickables;
using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Enemy
{
	public class Enemy : MonoBehaviour
	{
		[Header("Body Parts")] [SerializeField]
		private GameObject body;

		[SerializeField] private GameObject handPrefab;
		[SerializeField] private GameObject sightPrefab;
		[SerializeField] private GameObject typeSignPrefab;
		[SerializeField] private GameObject hitParticlesPrefab;

		[Header("Type Images")] [SerializeField]
		private Sprite normalSprite;

		[SerializeField] private Sprite carefulSprite;
		[SerializeField] private Sprite cowboySprite;
		[SerializeField] private Sprite camperSprite;

		[Header("Weapons")] [SerializeField] private GameObject startingWeaponPrefab;
		[SerializeField] private GameObject shotgunPrefab;
		[SerializeField] private GameObject uziPrefab;

		[Header("Variables")] [SerializeField] public float outOfRangeRotationSpeed = 5f;
		[SerializeField] public float speed = 1f;
		[SerializeField] public float waterSpeed = 1.5f;

		public float senseRotation = 1f;
		private float originalMoveSpeed;

		private float randomBehaviour; //BEN_CORRECTION : Nommage. Je devrais être capable de comprendre à quoi sert l'attribut en un coup d'oeil.
		public Health Health { get; set; } //BEN_CORRECTION : set devrait être private.
		public bool IsUnderFire { get; set; } //BEN_CORRECTION : set devrait être private.
		
		private Mover mover;
		private Destroyer destroyer;
		private HitSensor hitSensor;
		private PickableSensor pickableSensor;
		private Hand hand;
		private Sight sight;
		private Transform transformer;
		private RotationTimer rotationTimer;
		private Vector3 vectorBetweenEnemy;
		
		private CameraController cameraController;

		private BaseEnemyStrategy strategy;
		private Coroutine underFireRoutine;

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
			if (handPrefab == null)
				throw new ArgumentException("Body parts must be provided. Hand is missing.");
			if (sightPrefab == null)
				throw new ArgumentException("Body parts must be provided. Sight is missing.");
			if (typeSignPrefab == null)
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
			if (shotgunPrefab == null)
				throw new ArgumentException("StartingWeapon prefab must be provided.");
			if (uziPrefab == null)
				throw new ArgumentException("StartingWeapon prefab must be provided.");
		}

		private void InitializeComponent()
		{
			Health = GetComponent<Health>();
			mover = GetComponent<RootMover>();
			destroyer = GetComponent<RootDestroyer>();
			transformer = transform.root;
			rotationTimer = GetComponent<RotationTimer>();

			var rootTransform = transform.root; //BEN_CORRECTION : Erreur de logique. Voir trois lignes plus haut.
			hitSensor = rootTransform.GetComponentInChildren<HitSensor>();
			pickableSensor = rootTransform.GetComponentInChildren<PickableSensor>();
			hand = handPrefab.GetComponent<Hand>();
			sight = sightPrefab.GetComponent<Sight>();
			cameraController = GameObject.FindWithTag(Tags.MainCamera).GetComponent<CameraController>();

			originalMoveSpeed = mover.MoveSpeed;
		}

		private void CreateStartingWeapon()
		{
			hand.Hold(Instantiate(
				startingWeaponPrefab,
				Vector3.zero,
				Quaternion.identity
			));
		}

		private void OnEnable()
		{
			rotationTimer.OnRotationChanged += OnRotationTimerChanged;
			hitSensor.OnHit += OnHit;
			Health.OnDeath += OnDeath;
			pickableSensor.OnPickUp += OnPickUp;
		}

		private void OnDisable()
		{
			hitSensor.OnHit -= OnHit;
			Health.OnDeath -= OnDeath;
			pickableSensor.OnPickUp -= OnPickUp;
		}

		private void OnRotationTimerChanged()
		{
			randomBehaviour = UnityEngine.Random.Range(-1, 2);
			senseRotation *= -1;
		}

		public void Roam()
		{
			mover.Move(Mover.Forward);			
			RotateAllToForward();

			if (cameraController.IsObjectOutOfMap(transformer.gameObject))
			{
				mover.RotateTowardsARotation(OutOfMapRotation());
			}
			else if (randomBehaviour > 0)
			{
				mover.Rotate(senseRotation);
			}
		}

		private void RotateAllToForward()
		{
			hand.transform.rotation = transformer.rotation;
			sight.transform.rotation = transformer.rotation;
		}

		private Quaternion OutOfMapRotation()
		{
			var rotationDown = Quaternion.Euler(0, 0, 180);
			var rotationUp = Quaternion.Euler(0, 0, 0);
			var rotationLeft = Quaternion.Euler(0, 0, 90);
			var rotationRight = Quaternion.Euler(0, 0, -90);

			if (transformer.position.y >= cameraController.CameraHalfHeight)
				return rotationDown;
			if (transformer.position.y <= -cameraController.CameraHalfHeight)
				return rotationUp;
			return transformer.position.x >= cameraController.CameraHalfWidth ? rotationLeft : rotationRight;
		}

		public void Configure(EnemyStrategy strategy, Color color)
		{
			body.GetComponent<SpriteRenderer>().color = color;
			sightPrefab.GetComponent<SpriteRenderer>().color = color;

			switch (strategy)
			{
				case EnemyStrategy.Careful:
					typeSignPrefab.GetComponent<SpriteRenderer>().sprite = carefulSprite;
					gameObject.AddComponent<CarefulStrategy>();
					break;
				case EnemyStrategy.Cowboy:
					typeSignPrefab.GetComponent<SpriteRenderer>().sprite = cowboySprite;
					gameObject.AddComponent<CowboyStrategy>();
					break;
				case EnemyStrategy.Camper:
					typeSignPrefab.GetComponent<SpriteRenderer>().sprite = camperSprite;
					gameObject.AddComponent<CamperStrategy>();
					break;
				default:
					typeSignPrefab.GetComponent<SpriteRenderer>().sprite = normalSprite;
					gameObject.AddComponent<NormalStrategy>();
					break;
			}
		}

		private void OnHit(int hitPoints)
		{
			Health.Hit(hitPoints);
			StartUnderFireBehaviour();

			Instantiate(hitParticlesPrefab, transformer.position, transformer.rotation, transformer);
		}

		private void StartUnderFireBehaviour()
		{			
			IsUnderFire = true;
			mover.RotateSpeed = Mover.HitReactRotateSpeed;

			if (underFireRoutine != null)
				StopCoroutine(underFireRoutine);

			underFireRoutine = StartCoroutine(CancelUnderFireRoutine());
		}

		private IEnumerator CancelUnderFireRoutine()
		{
			yield return new WaitForSeconds(1f);
			IsUnderFire = false;
			underFireRoutine = null;
			mover.RotateSpeed = Mover.NormalRotateSpeed;
		}

		private void OnDeath()
		{
			destroyer.Destroy();
		}
		
		private void OnPickUp(Pickable pickable)
		{
			var type = pickable.Type;

			if (type == PickableTypes.Shotgun)
			{
				hand.Hold(Instantiate(shotgunPrefab));
			}
			else if (type == PickableTypes.Uzi)
			{
				hand.Hold(Instantiate(uziPrefab));
			}

			pickable.Use(gameObject);
			Destroy(pickable.transform.parent.gameObject);
		}

		public void ShootTowardsTarget(Transform target)
		{
			hand.AimTowards(target.gameObject);
			hand.Use();
		}
		
		public void Shoot()
		{
			hand.Use();
		}

		public void SetSpeedToSwim()
		{
			mover.MoveSpeed = waterSpeed;
		}

		public void SetSpeedToWalk()
		{
			mover.MoveSpeed = originalMoveSpeed;
		}
	}
}