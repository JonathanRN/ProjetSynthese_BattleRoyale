using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using Playmode.Entity.Senses;
using Playmode.Util.Values;

namespace Playmode.Ennemy.Strategies
{
    public class NormalStrategy : IEnnemyStrategy
    {
        private readonly Mover mover;
        private readonly HandController handController;
        private EnnemyController enemyController;
        private EnnemySensor enemySensor;
        private GameObject target;
		private Transform enemyTransformer;

		public NormalStrategy(Mover mover, HandController handController, EnnemySensor enemySensor, Transform transformer, TimedRotation timedRotation, EnnemyController enemyController)
		{
            this.mover = mover;
            this.handController = handController;

			this.enemyTransformer = transformer;
			this.enemySensor = enemySensor;
			this.enemyController = enemyController;

			enemySensor.OnEnnemySeen += OnEnnemySeen;
			enemySensor.OnEnnemySightLost += OnEnnemySightLost;
            
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

        public void Act()
        {

			if (target != null)
			{
				enemyController.MoveTowardsTarget(target.transform);
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
