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
		private Vector3 dodgeDirection = Vector3.right;
		
		private void Start()
		{
			maxDistanceBetweenEnemy = 4.6f;
		}

		protected override void Act()
		{
			CalculateDistanceBetweenEnemies();
			if (HasTarget())
			{
				if (distanceBetweenEnemy < maxDistanceBetweenEnemy)
				{
					DodgeBullets();
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
		
		private void OnEnable()
		{
			StartCoroutine(ChangeDodgeDirection());
		}

		private IEnumerator ChangeDodgeDirection()
		{
			while (true)
			{
				yield return new WaitForSeconds(Random.Range(1,2));
				DodgeDirectionChange();
			}
		}

		private void DodgeDirectionChange()
		{
			dodgeDirection = dodgeDirection == Vector3.right ? Vector3.left : Vector3.right;
		}

		private void DodgeBullets()
		{
			mover.Move(dodgeDirection);
		}
	}
}
