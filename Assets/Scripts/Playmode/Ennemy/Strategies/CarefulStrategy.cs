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
        private EnnemyController enemyController;
        private EnnemySensor enemySensor;
        private PickableSensor pickableSensor;
        private GameObject target;
        private Transform enemyTransformer;
        private float distanceBetweenEnemy;
        private bool outOfMap;
        private bool needMedKit;
        private GameObject pickable;
		private PickableType pickableType;
		[SerializeField]
		private float maxDistanceWantedBetweenEnemy = 6;

        public CarefulStrategy(Mover mover, HandController handController, EnnemySensor enemySensor, Transform transformer, TimedRotation timedRotation, EnnemyController enemyController, PickableSensor pickableSensor)
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
            needMedKit = CheckIfEnemyNeedsMedKit();
			if (needMedKit)
			{
				SearchForMedKit();
			}
			else
			{
				if (target != null)
				{
					outOfMap = enemyController.CheckIfOutOfMap();					
					BackFromEnemyIfTooClose();

					enemyController.RotateTowardsTarget(target.transform);
					handController.Use();
				}
				else if(pickable != null)
				{
					if (pickableType.GetType() != PickableTypes.MedicalKit)
					{
						enemyController.MoveTowardsTarget(pickable.transform);
					}
					else
					{
						enemyController.Roam();
					}
				}
				else
				{
					if (!enemyController.onFire)
					{
						enemyController.Roam();
					}
					else
					{
						enemyController.HitReact();
					}
				}
			}
        }

        private bool CheckIfEnemyNeedsMedKit()
        {
            if(enemyController.HealthPoints.HealthPoints < 50)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		private void SearchForMedKit()
		{
			if (pickable != null)
			{
				if (pickableType.GetType() == PickableTypes.MedicalKit)
				{
					enemyController.MoveTowardsTarget(pickable.transform);
				}
				else
				{
					enemyController.Roam();
				}
			}
			else
			{
				enemyController.Roam();
			}
		}

		private void BackFromEnemyIfTooClose()
		{
			distanceBetweenEnemy = Vector3.Distance(enemyTransformer.position, target.transform.position);
			if (distanceBetweenEnemy < maxDistanceWantedBetweenEnemy)
			{
				if (!outOfMap)
				{
					mover.Move(new Vector3(0, -Mover.Clockwise));
				}
				else
				{
					mover.Move(new Vector3(0, Mover.Clockwise));
				}
			}
		}
    }
}
