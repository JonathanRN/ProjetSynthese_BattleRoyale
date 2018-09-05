using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Movement;
using Playmode.Weapon;
using UnityEngine;

namespace Playmode.Enemy.BodyParts
{
	public class Hand : MonoBehaviour
	{
		private Mover mover;
		public WeaponController WeaponController;
		private Sight sight;

		private void Awake()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			mover = GetComponent<RootMover>();
			sight = transform.parent.GetComponentInChildren<Sight>();
		}
		
		public void Hold(GameObject gameObject)
		{
			RemoveCurrentHoldingWeapons();
			if (gameObject != null)
			{
				gameObject.transform.parent = transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;

				WeaponController = gameObject.GetComponentInChildren<WeaponController>();
			}
			else
			{
				WeaponController = null;
			}
		}

		public void AimTowards(GameObject target)
		{
			if (!DoesEnemyAimHimself())
			{
				var vectorBetweenEnemy = new Vector3(transform.position.x - target.transform.position.x,
					transform.position.y - target.transform.position.y);
				if (Vector3.Dot(vectorBetweenEnemy, transform.right) < -0.5)
				{
					transform.root.Rotate(Vector3.forward, -1);
				}
				else if (Vector3.Dot(vectorBetweenEnemy, transform.right) > 0.5)
				{
					transform.root.Rotate(Vector3.forward, 1);
				}

				sight.LookTowardsTarget(target);
			}
			else
			{
				transform.parent.rotation = transform.rotation;
			}
		}

		public void Use()
		{
			if (WeaponController != null) WeaponController.Shoot();
		}

		private void RemoveCurrentHoldingWeapons()
		{
			if (transform.childCount <= 0) return;
			for (int i = 0; i < transform.childCount; i++)
			{
				Destroy(transform.GetChild(i).gameObject);
			}
		}

		public WeaponController GetCurrentHoldingWeapon()
		{
			return WeaponController != null ? WeaponController : null;
		}

		private bool DoesEnemyAimHimself()
		{
			return transform.rotation.z > 145 || transform.rotation.z < -145;
		}
	}
}