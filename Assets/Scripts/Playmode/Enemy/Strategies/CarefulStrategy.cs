using UnityEngine;

namespace Playmode.Enemy.Strategies
{
	public class CarefulStrategy : BaseEnemyStrategy
	{
		private const float SpeedWhenBacking = 2;
		private const float SpeedWhenWounded = 5;
		private const float MinLifeBeforeSearchingMedKit = 20;
		private bool needsMedKit;

		protected override void Act()
		{
			CalculateDistanceBetweenEnemies();
			if (DoesNeedMedKit())
			{
				SearchForMedKit();
			}
			else
			{
				if (HasTarget())
				{
					BackFromEnemyIfTooClose();
					Enemy.ShootTowardsTarget(Target.transform);
				}
				else if(Enemy.IsUnderFire)
				{
					Mover.HitReact();
				}
				else if(IsPickableAWeapon())
				{
					Mover.MoveTowardsTarget(Pickable.transform);
				}
				else
				{
					Enemy.Roam();
				}
			}
		}

		private bool DoesNeedMedKit()
		{
			return Enemy.Health.HealthPoints < MinLifeBeforeSearchingMedKit;
		}

		private void SearchForMedKit()
		{
			Mover.MoveSpeed = SpeedWhenWounded;
			if (Pickable != null && Pickable.IsMedicalKit())
			{
				Mover.MoveTowardsTarget(Pickable.transform);
			}
			else
			{
				Enemy.Roam();
			}
		}

		private void BackFromEnemyIfTooClose()
		{
			if (DistanceBetweenEnemy < MaxDistanceBetweenEnemy)
			{
				Mover.RotateTowardsTarget(Target.transform);
				Mover.MoveSpeed = SpeedWhenBacking;
				Mover.Move(new Vector3(0,-1));
			}
			else
			{
				Mover.MoveTowardsTarget(Target.transform);
			}				
		}
	}
}
