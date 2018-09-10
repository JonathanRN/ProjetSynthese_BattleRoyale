using System;
using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Weapon
{
	//BEN_REVIEW : Vous avez commencé un renommage, mais vous avez oublié d'enlever "Controller" de WeaponController.
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
			//BEN_CORRECTION : Rider n'a pas toujours raison sur tout. Préférer un "if/else" pour la lisibilité.
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
			//BEN_CORRECTION : Votre algorythme marche seulement pour un nombre pair de bullets. Défaut de logique.
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