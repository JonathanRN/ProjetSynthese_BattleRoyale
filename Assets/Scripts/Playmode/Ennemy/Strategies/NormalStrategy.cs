using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using Playmode.Entity.Senses;
using Playmode.Environment;
using Playmode.Util.Values;

namespace Playmode.Ennemy.Strategies
{
	public class NormalStrategy : IEnnemyStrategy
	{
		private readonly Mover mover;
		private readonly HandController handController;
		private EnnemyController enemyController;
		private GameController gameController;
		private GameObject target;
		private Transform enemyTransformer;
		private float distanceBetweenEnemy;
		private float maxDistanceBetweenEnemy = 5f;

		public NormalStrategy(Mover mover, HandController handController, EnnemySensor enemySensor,
			Transform transformer, TimedRotation timedRotation, EnnemyController enemyController)
		{
			this.mover = mover;
			this.handController = handController;

			this.enemyTransformer = transformer;
			this.enemyController = enemyController;

			enemySensor.OnEnnemySeen += OnEnnemySeen;
			enemySensor.OnEnnemySightLost += OnEnnemySightLost;
		}

		private void OnEnnemySeen(EnnemyController ennemy)
		{
			if (target != null) return;
			target = ennemy.gameObject;
		}

		private void OnEnnemySightLost(EnnemyController ennemy)
		{
			target = null;
		}

		public void Act()
		{
			if (target != null)
			{
				distanceBetweenEnemy = Vector3.Distance(enemyTransformer.position, target.transform.position);
				if (distanceBetweenEnemy <= maxDistanceBetweenEnemy)
				{
					enemyController.ShootTowardsTarget(target.transform);
				    mover.Move(Vector3.left);
				}
				else
				{
					mover.MoveTowardsTarget(target.transform);
				}				
				handController.Use();
			}
			else
			{
				if (!enemyController.IsUnderFire)
				{
					enemyController.Roam();
				}
				else
				{
					mover.HitReact();
				}
			}
		}
	}
}