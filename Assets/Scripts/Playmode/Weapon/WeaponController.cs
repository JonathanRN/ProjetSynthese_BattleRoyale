using System;
using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Weapon
{
	public class WeaponController : MonoBehaviour
	{
		[Header("Behaviour")] [SerializeField] private GameObject bulletPrefab;
		[SerializeField] public float FireDelayInSeconds = 1f;
		[SerializeField] public PickableTypes WeaponType;

		private float lastTimeShotInSeconds;
		private const int NbOfShotgunBullets = 6;
		private GameObject[] shotgunBullets;

		private bool CanShoot => Time.time - lastTimeShotInSeconds > FireDelayInSeconds;

		private void Awake()
		{
			ValidateSerialisedFields();
			InitializeComponent();
		}

		private void ValidateSerialisedFields()
		{
			if (FireDelayInSeconds < 0)
				throw new ArgumentException("FireRate can't be lower than 0.");
		}

		private void InitializeComponent()
		{
			lastTimeShotInSeconds = 0;
			shotgunBullets = new GameObject[NbOfShotgunBullets];
		}
		
		public void Shoot()
		{
			if (!CanShoot) return;

			if (WeaponType == PickableTypes.Shotgun)
			{
				ShootWithShotgun();
			}
			else
			{
				Instantiate(bulletPrefab, transform.position, transform.rotation);
			}

			lastTimeShotInSeconds = Time.time;
		}

		public PickableTypes GetWeaponType()
		{
			return WeaponType;
		}

		private void ShootWithShotgun()
		{
			for (int i = 0; i < NbOfShotgunBullets; i++)
			{
				shotgunBullets[i] = Instantiate(bulletPrefab, transform.position, transform.rotation);
				if (i % 2 == 0)
				{
					shotgunBullets[i].transform.Rotate(Vector3.forward * 0.5f * i, Space.Self);
				}
				else
				{
					shotgunBullets[i].transform.Rotate(Vector3.forward * -0.5f * i, Space.Self);
				}
			}
		}
	}
}