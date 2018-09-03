using System;
using Playmode.Movement;
using Playmode.Util.Values;
using Playmode.Weapon;
using UnityEngine;

namespace Playmode.Ennemy.BodyParts
{
	public class HandController : MonoBehaviour
	{
		private Mover mover;
		public WeaponController weaponController;
		private SightController sightController;

		private void Awake()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			mover = GetComponent<RootMover>();
			sightController = transform.parent.GetComponentInChildren<SightController>();
		}
		
		public void Hold(GameObject gameObject)
		{
			RemoveCurrentHoldingWeapons();
			if (gameObject != null)
			{
				gameObject.transform.parent = transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;

				weaponController = gameObject.GetComponentInChildren<WeaponController>();
			}
			else
			{
				weaponController = null;
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
					transform.Rotate(Vector3.forward, -1);
				}
				else if (Vector3.Dot(vectorBetweenEnemy, transform.right) > 0.5)
				{
					transform.Rotate(Vector3.forward, 1);
				}

				sightController.CheckTowards(target);
			}
			else
			{
				transform.parent.rotation = transform.rotation;
			}
		}

		public void Use()
		{
			if (weaponController != null) weaponController.Shoot();
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
			return weaponController != null ? weaponController : null;
		}

		private bool DoesEnemyAimHimself()
		{
			return transform.rotation.z > 145 || transform.rotation.z < -145;
		}
	}
}