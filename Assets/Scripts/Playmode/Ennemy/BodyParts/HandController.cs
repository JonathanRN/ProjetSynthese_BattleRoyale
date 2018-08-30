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

		private void Awake()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			mover = GetComponent<AnchoredMover>();
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
			//TODO : Utilisez ce que vous savez des vecteurs(rien) pour implémenter cette méthode
			throw new NotImplementedException();
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
	}
}