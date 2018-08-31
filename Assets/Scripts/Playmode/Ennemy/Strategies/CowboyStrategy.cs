using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using Playmode.Entity.Senses;
using Playmode.Util.Values;

namespace Playmode.Ennemy.Strategies
{
	public class CowboyStrategy : IEnnemyStrategy
	{

		private readonly Mover mover;
		private readonly EnnemyController enemyController;
		private EnnemySensor enemySensor;
		private PickableSensor pickableSensor;
		private GameObject target;
		private readonly Transform enemyTransformer;
		private float distanceBetweenEnemy;
		private GameObject pickable;
		private PickableType pickableType;
		[SerializeField] private const float maxDistanceWantedBetweenEnemy = 6;
		[SerializeField] private const float minLifeBeforeSearchingMedKit = 50;

		public CowboyStrategy(Mover mover, EnnemySensor enemySensor,
			Transform transformer, EnnemyController enemyController,
			PickableSensor pickableSensor)
		{
			this.mover = mover;

			this.enemyTransformer = transformer;
			this.enemySensor = enemySensor;
			this.pickableSensor = pickableSensor;
			this.enemyController = enemyController;

			enemySensor.OnEnnemySeen += OnEnnemySeen;
			enemySensor.OnEnnemySightLost += OnEnnemySightLost;
			pickableSensor.OnPickableSeen += OnPickableSeen;

		}

		private void OnEnnemySeen(EnnemyController ennemy)
		{
			target = ennemy.gameObject;
		}

		private void OnEnnemySightLost(EnnemyController ennemy)
		{
			target = null;
		}

		private void OnPickableSeen(GameObject pickable)
		{
			Debug.Log("I've seen a " + pickable.GetComponentInChildren<PickableType>().GetType());
			pickableType = pickable.GetComponentInChildren<PickableType>();
			this.pickable = pickable;
		}

		public void Act()
		{
			
			if (HasTarget())
			{
				distanceBetweenEnemy = Vector3.Distance(enemyTransformer.position, target.transform.position);
				if (distanceBetweenEnemy < maxDistanceWantedBetweenEnemy)
				{
					mover.Move(Vector3.right);
				}
				else
				{
					mover.MoveTowardsTarget(target.transform);
				}
				enemyController.ShootTowardsTarget(target.transform);			
			}
			else if(enemyController.IsUnderFire)
			{
				enemyController.HitReact();
			}
			else if (IsPickableAWeapon())
			{
				mover.MoveTowardsTarget(pickable.transform);
			}
			else
			{
				enemyController.Roam();
			}
			
		}

		private bool IsPickableAWeapon()
		{
			return pickable != null && pickableType.IsWeapon();
		}

		private bool HasTarget()
		{
			return target != null;
		}
		
	}
}
