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
	public class NormalStrategy : BaseEnnemyStrategy
	{
		private void Start()
		{
			maxDistanceBetweenEnemy = 3f;
		}

		protected override void Act()
		{
			CalculateDistanceBetweenEnemies();
			
			if (HasTarget())
			{				
				if (distanceBetweenEnemy <= maxDistanceBetweenEnemy)
				{
					enemyController.ShootTowardsTarget(target.transform);
				    mover.Move(Vector3.left);
				}
				else
				{
					mover.MoveTowardsTarget(target.transform);
				}				
				enemyController.Shoot();
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