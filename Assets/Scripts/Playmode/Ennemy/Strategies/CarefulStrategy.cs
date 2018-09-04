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
	public class CarefulStrategy : BaseEnnemyStrategy
	{
		private float speedWhenBacking = 2;
		private float speedWhenWounded = 5;
		private bool needMedKit;		
		private const float minLifeBeforeSearchingMedKit = 20;

		protected override void Act()
		{
			CalculateDistanceBetweenEnemies();
			if (DoesEnemyNeedMedKit())
			{
				SearchForMedKit();
			}
			else
			{
				mover.MoveSpeed = 4;
				if (HasTarget())
				{
					BackFromEnemyIfTooClose();
					enemyController.ShootTowardsTarget(target.transform);
				}
				else if(enemyController.IsUnderFire)
				{
					mover.HitReact();
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



		private bool DoesEnemyNeedMedKit()
		{
			return enemyController.Health.HealthPoints < minLifeBeforeSearchingMedKit;
		}

		private void SearchForMedKit()
		{
			mover.MoveSpeed = speedWhenWounded;
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
			if (distanceBetweenEnemy < maxDistanceBetweenEnemy)
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
