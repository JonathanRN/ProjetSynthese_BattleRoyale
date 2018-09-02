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
		
		protected override void Act()
		{
			if (target != null)
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