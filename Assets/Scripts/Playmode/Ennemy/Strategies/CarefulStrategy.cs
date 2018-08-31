using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using Playmode.Entity.Senses;
using Playmode.Util.Values;

namespace Playmode.Ennemy.Strategies
{
	public class CarefulStrategy : IEnnemyStrategy
	{
		private readonly Mover mover;
		private readonly EnnemyController enemyController;
		private HandController handController;
		private readonly GameController gameController;
		private GameObject target;
		private readonly Transform enemyTransformer;
		private float distanceBetweenEnemy;
		private float speedWhenBacking = 2;
		private bool isOutOfMap;
		private bool needMedKit;
		private GameObject pickable;
		private PickableType pickableType;
		
		private const float maxDistanceWantedBetweenEnemy = 6;
		private const float minLifeBeforeSearchingMedKit = 20;

		public CarefulStrategy(Mover mover, EnnemySensor enemySensor,
			Transform transformer, EnnemyController enemyController, GameController gameController,HandController handController,
			PickableSensor pickableSensor)
		{
			this.mover = mover;

			this.enemyTransformer = transformer;
			this.enemyController = enemyController;
			this.gameController = gameController;
			this.handController = handController;

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
			if (DoesEnemyNeedMedKit())
			{
				SearchForMedKit();
			}
			else
			{
				mover.MoveSpeed = 4;
				if (target != null)
				{
					BackFromEnemyIfTooClose();
					enemyController.ShootTowardsTarget(target.transform);
				}
				else if(enemyController.IsUnderFire)
				{
					enemyController.HitReact();
				}
				else if(IsPickableAWeapon())
				{
					mover.MoveTowardsTarget(pickable.transform);
				}
				else
				{
					enemyController.Roam();
				}
			}
		}

		private bool IsPickableAWeapon()
		{
			return pickable != null && pickableType.IsWeapon();
		}

		private bool DoesEnemyNeedMedKit()
		{
			return enemyController.Health.HealthPoints < minLifeBeforeSearchingMedKit;
		}

		private void SearchForMedKit()
		{
			mover.MoveSpeed = 5;
			if (pickable != null && pickableType.IsMedKit())
			{
				mover.MoveTowardsTarget(pickable.transform);
			}
			else
			{
				enemyController.Roam();
			}
		}

		private void BackFromEnemyIfTooClose()
		{
			distanceBetweenEnemy = Vector3.Distance(enemyTransformer.position, target.transform.position);

			if ((distanceBetweenEnemy < maxDistanceWantedBetweenEnemy))
			{
				mover.RotateTowardsTarget(target.transform);
				mover.MoveSpeed = speedWhenBacking;
				mover.Move(new Vector3(0,-1));
			}
			else
			{
				mover.MoveTowardsTarget(target.transform);
			}
				
		}
	}
}
