using System.Collections;
using UnityEngine;

namespace Playmode.Enemy.Strategies
{
	public class CowboyStrategy : BaseEnemyStrategy
	{
		private Vector3 dodgeDirection = Vector3.right;
		
		private void Start()
		{
			MaxDistanceBetweenEnemy = 4.6f;
		}

		private new void OnEnable()
		{
			base.OnEnable();
			StartCoroutine(DodgeDirectionRoutine());
		}
		
		protected override void Act()
		{
			CalculateDistanceBetweenEnemies();
			if (HasTarget())
			{
				if (DistanceBetweenEnemy < MaxDistanceBetweenEnemy)
				{
					DodgeBullets();
				}
				else
				{
					Mover.MoveTowardsTarget(Target.transform);
				}
				Enemy.ShootTowardsTarget(Target.transform);			
			}
			else if(Enemy.IsUnderFire)
			{
				Mover.HitReact();
			}
			else if (IsPickableAWeapon())
			{
				Mover.MoveTowardsTarget(Pickable.transform);
			}
			else
			{
				Enemy.Roam();
			}
		}

		private IEnumerator DodgeDirectionRoutine()
		{
			while (true)
			{
				yield return new WaitForSeconds(Random.Range(1,2));
				ChangeDodgeDirection();
			}
		}

		private void ChangeDodgeDirection()
		{
			dodgeDirection = dodgeDirection == Vector3.right ? Vector3.left : Vector3.right;
		}

		private void DodgeBullets()
		{
			Mover.Move(dodgeDirection);
		}
	}
}
