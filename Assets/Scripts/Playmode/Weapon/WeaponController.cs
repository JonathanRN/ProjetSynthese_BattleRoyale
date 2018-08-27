using System;
using UnityEngine;

namespace Playmode.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float fireDelayInSeconds = 1f;

        private float lastTimeShotInSeconds;

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
        }

        public void Shoot()
        {
            if (CanShoot)
            {
				var rotation = transform.rotation;

                Instantiate(bulletPrefab, transform.position, new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w));
                Instantiate(bulletPrefab, transform.position, new Quaternion(rotation.x, rotation.y, rotation.z - 0.5f, rotation.w));
                Instantiate(bulletPrefab, transform.position, new Quaternion(rotation.x, rotation.y, rotation.z - 1f, rotation.w));
                //Instantiate(bulletPrefab, transform.position, new Quaternion(rotation.x, rotation.y, rotation.z - 0.5f, rotation.w));
                //Instantiate(bulletPrefab, transform.position, new Quaternion(rotation.x, rotation.y, rotation.z + 0.5f, rotation.w));

				lastTimeShotInSeconds = Time.time;
            }
        }
    }
}