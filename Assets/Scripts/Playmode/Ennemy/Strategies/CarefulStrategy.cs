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

        public CarefulStrategy(Mover mover, HandController handController, EnnemySensor enemySensor, Transform transformer, TimedRotation timedRotation, EnnemyController enemyController,PickableSensor pickableSensor)
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
            Debug.Log("I've seen an ennemy!! Ya so dead noob!!!");
            target = ennemy.gameObject;
        }

        private void OnEnnemySightLost(EnnemyController ennemy)
        {
            Debug.Log("I've lost sight of an ennemy...Yikes!!!");
            target = null;
        }

        private void OnPickableSeen(GameObject pickable)
        {
            Debug.Log("I've seen a PICKABLE!!");
        }

        public void Act()
        {
            if(enemyController.HealthPoints.HealthPoints <= 50)
            {
                enemyController.Roam();
            }
            else if (target != null)
            {
                //enemyController.MoveTowardsTarget(target.transform);
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
