using Playmode.Ennemy.BodyParts;
using System;
using Playmode.Movement;
using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] private GameObject bulletPrefab;
        [SerializeField] public float fireDelayInSeconds = 1f;

        private float lastTimeShotInSeconds;
		private int nbOfShotgunBullets = 6;
		private GameObject[] shotgunBullets;

        private bool CanShoot => Time.time - lastTimeShotInSeconds > fireDelayInSeconds;

	    [SerializeField] public PickableTypes weaponType;

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
	        if (!CanShoot) return;

	        if (weaponType == PickableTypes.Shotgun)
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
		    return weaponType;
	    }

	    private void ShootWithShotgun()
	    {
		    for (int i = 0; i < nbOfShotgunBullets; i++)
		    {
			    shotgunBullets[i] = Instantiate(bulletPrefab, transform.position, transform.rotation);
			    if (i % 2 == 0)
			    {
				    shotgunBullets[i].transform.Rotate(Vector3.forward * 1.5f * i, Space.Self);
			    }
			    else
			    {
				    shotgunBullets[i].transform.Rotate(Vector3.forward * -1.5f * i, Space.Self);
			    }
		    }
	    }
	}
}