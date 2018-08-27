using Playmode.Ennemy.BodyParts;
using System;
using UnityEngine;

namespace Playmode.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float fireDelayInSeconds = 1f;

		private HandController handController;

        private float lastTimeShotInSeconds;
		private int nbOfShotgunBullets;
		private GameObject[] shotgunBullets;

        private bool CanShoot => Time.time - lastTimeShotInSeconds > fireDelayInSeconds;

        private void Awake()
        {
            ValidateSerialisedFields();
            InitializeComponent();
        }

        private void ValidateSerialisedFields()
        {
            if (fireDelayInSeconds < 0)
                throw new ArgumentException("FireRate can't be lower than 0.");
        }

        private void InitializeComponent()
        {
            lastTimeShotInSeconds = 0;
			nbOfShotgunBullets = 3;
			shotgunBullets = new GameObject[nbOfShotgunBullets];
        }

        public void Shoot()
        {
			if (CanShoot)
            {
				var rotation = transform.root.rotation;

				UpdateAndShootWithCurrentWeapon();

				lastTimeShotInSeconds = Time.time;
            }
        }

		private void UpdateAndShootWithCurrentWeapon()
		{
			var weapon = HandController.currentWeapon;

			if (weapon != null)
			{
				if (weapon.GetComponentInChildren<PickableType>().GetType() == Util.Values.PickableTypes.Shotgun)
				{
					fireDelayInSeconds = 1.5f;

					var rotation = transform.root.rotation;

					for (int i = 0; i < nbOfShotgunBullets; i++)
					{
						shotgunBullets[i] = Instantiate(bulletPrefab, transform.position, transform.rotation);
					}
				}
				else if (weapon.GetComponentInChildren<PickableType>().GetType() == Util.Values.PickableTypes.Uzi)
				{
					fireDelayInSeconds = 0.1f;
					Instantiate(bulletPrefab, transform.position, transform.rotation);
				}
			}
			else
			{
				Instantiate(bulletPrefab, transform.position, transform.rotation); //Normal shot
			}
		}
	}
}