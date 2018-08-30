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
		private readonly HandController handController;
		private readonly EnnemyController enemyController;
		private EnnemySensor enemySensor;
		private PickableSensor pickableSensor;
		private GameObject target;
		private readonly Transform enemyTransformer;
		private float distanceBetweenEnemy;
		private bool isOutOfMap;
		private bool needMedKit;
		private GameObject pickable;
		private PickableType pickableType;
		[SerializeField] private const float maxDistanceWantedBetweenEnemy = 6;
		[SerializeField] private const float minLifeBeforeSearchingMedKit = 50;

		public CarefulStrategy(Mover mover, HandController handController, EnnemySensor enemySensor,
			Transform transformer, TimedRotation timedRotation, EnnemyController enemyController,
			PickableSensor pickableSensor)
		{
			this.mover = mover;
			this.handController = handController;

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
			isOutOfMap = enemyController.IsEnemyOutOfMap();
			distanceBetweenEnemy = Vector3.Distance(enemyTransformer.position, target.transform.position);

			if (!(distanceBetweenEnemy < maxDistanceWantedBetweenEnemy)) return;

			mover.Move(!isOutOfMap ? new Vector3(0, -Mover.Clockwise) : new Vector3(0, Mover.Clockwise));
		}
	}
}
