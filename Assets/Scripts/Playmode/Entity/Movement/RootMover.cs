using UnityEngine;

namespace Playmode.Entity.Movement
{
	public class RootMover : Mover
	{
		private Transform rootTransform;

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
			rootTransform.Translate(direction.normalized * MoveSpeed * Time.deltaTime);
		}

		public override void Rotate(float direction)
		{
			rootTransform.Rotate(
				Vector3.forward,
				(direction < 0 ? RotateSpeed : -RotateSpeed) * Time.deltaTime
			);
		}

		public override void MoveTowardsTarget(Transform target)
		{
			Move(new Vector3(0, 1));
			RotateTowardsTarget(target);
		}

		public override void RotateTowardsTarget(Transform target)
		{
			var vectorBetweenEnemy = new Vector3(rootTransform.position.x - target.transform.position.x,
				rootTransform.position.y - target.transform.position.y);
			if (Vector3.Dot(vectorBetweenEnemy, rootTransform.right) < -0.5)
			{
				Rotate(1f * Time.deltaTime);
			}
			else if (Vector3.Dot(vectorBetweenEnemy, rootTransform.right) > 0.5)
			{
				Rotate(-1f * Time.deltaTime);
			}
		}

		public override void RotateTowardsARotation(Quaternion rotation)
		{
			rootTransform.rotation = Quaternion.Slerp(rootTransform.rotation, rotation,
				Time.deltaTime * OutOfRangeRotationSpeed);
		}

		public override void HitReact()
		{
			Rotate(Clockwise);
		}
	}
}