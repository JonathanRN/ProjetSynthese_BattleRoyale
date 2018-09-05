using UnityEngine;

namespace Playmode.Enemy.Strategies
{
	public class NormalStrategy : BaseEnemyStrategy
	{
		private void Start()
		{
			MaxDistanceBetweenEnemy = 3f;
		}

		protected override void Act()
		{
			CalculateDistanceBetweenEnemies();
			
			if (HasTarget())
			{				
				if (DistanceBetweenEnemy <= MaxDistanceBetweenEnemy)
				{
					Enemy.ShootTowardsTarget(Target.transform);
				    Mover.Move(Vector3.left);
				}
				else
				{
					Mover.MoveTowardsTarget(Target.transform);
				}				
				Enemy.Shoot();
			}
			else
			{
				if (!Enemy.IsUnderFire)
				{
					Enemy.Roam();
				}
				else
				{
					Mover.HitReact();
				}
			}
		}
	}
}