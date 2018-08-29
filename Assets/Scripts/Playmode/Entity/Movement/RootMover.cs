using UnityEngine;

namespace Playmode.Movement
{
    public class RootMover : Mover
    {
        private Transform rootTransform;
        
        [SerializeField]
        public float outOfRangeRotationSpeed = 5f;
        [SerializeField]
        public float cameraHalfHeight = 8.5f;
        [SerializeField]
        public float cameraHalfWidth = 20.5f;

        private new void Awake()
        {
            base.Awake();

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            rootTransform = transform.root;
        }

        public override void Move(Vector3 direction)
        {
            rootTransform.Translate(direction.normalized * speed * Time.deltaTime);
        }

        public override void Rotate(float direction)
        {
            rootTransform.Rotate(
                Vector3.forward,
                (direction < 0 ? rotateSpeed : -rotateSpeed) * Time.deltaTime
            );
        }

        public override void MoveTowardsTarget(Transform target)
        {
            Move(new Vector3(0, 1));
            RotateTowardsTarget(target);
        }

        public override void RotateTowardsTarget(Transform target)
        {
            var vectorBetweenEnemy = new Vector3(rootTransform.position.x - target.transform.position.x, rootTransform.position.y - target.transform.position.y);
            if (Vector3.Dot(vectorBetweenEnemy, rootTransform.right) < -0.5)
            {
                Rotate(1f * Time.deltaTime);
            }
            else if (Vector3.Dot(vectorBetweenEnemy, rootTransform.right) > 0.5)
            {
                Rotate(-1f * Time.deltaTime);
            }
        }

        public override void Roam()
        {
            throw new System.NotImplementedException();
        }
    }
}