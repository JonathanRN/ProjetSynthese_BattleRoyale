using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using Playmode.Entity.Senses;
using Playmode.Util.Values;

namespace Playmode.Ennemy.Strategies
{
	public class CowboyStrategy : BaseEnnemyStrategy
	{
		protected override void Act()
		{
			
			if (HasTarget())
			{
				if (distanceBetweenEnemy < maxDistanceBetweenEnemy)
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
				mover.HitReact();
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
