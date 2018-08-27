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
            Debug.Log("I've seen a PICKABLE!!");
        }

        public void Act()
        {
            //if(enemyController.HealthPoints.HealthPoints <= 50)
            //{
            //    enemyController.Roam();
            //}
            if (target != null)
            {
                enemyController.OutOfMapHandler();
                distanceBetweenEnemy = Vector3.Distance(enemyTransformer.position, target.transform.position);
                if(distanceBetweenEnemy < 6)
                {
                    mover.Move(new Vector3(0, -5));
                }
                //enemyController.MoveTowardsTarget(target.transform);
                enemyController.RotateTowardsTarget(target.transform);
                handController.Use();
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
}
