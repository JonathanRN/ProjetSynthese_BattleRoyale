using Playmode.Entity.Movement;
using Playmode.Pickables;
using UnityEngine;

namespace Playmode.Enemy.Strategies
{
	public class CamperStrategy : BaseEnemyStrategy
	{
		private bool isNextToMedKit;
		private Pickable savedMedKit;
		private const int MinimumDistanceToPickable = 2;

		protected override void Act()
		{
			TryToFindMedkit();

			NextToMedkitBehaviour();

			TryToFindWeapon();

			ResetIfOutOfMap();
		}

		private void TryToFindWeapon()
		{
			if (HasFoundWeapon() && !isNextToMedKit)
			{
				Mover.MoveTowardsTarget(Pickable.transform);
			}
		}

		private void ResetIfOutOfMap()
		{
			if (CameraController.IsObjectOutOfMap(transform.gameObject))
			{
				ResetAct();
			}
		}

		private void NextToMedkitBehaviour()
		{
			if (!isNextToMedKit) return;
			
			if (DoesEnemyNeedMedKit())
			{
				PickUpSavedMedKit();
				ResetAct();
			}
			else if (Target != null)
			{
				Enemy.ShootTowardsTarget(Target.transform);
			}
			else
			{
				Mover.Rotate(Mover.Clockwise); //Scans around
			}
		}

		private void TryToFindMedkit()
		{
			if (HasFoundMedKit())
			{
				MoveNextToMedKit();
			}
			else if (!isNextToMedKit && !HasFoundWeapon())
			{
				Enemy.Roam();
			}
		}

		private void PickUpSavedMedKit()
		{
			if (savedMedKit != null)
			{
				Mover.MoveTowardsTarget(savedMedKit.transform);
			}
		}

		private bool HasFoundMedKit()
		{
			return Pickable != null && Pickable.IsMedicalKit();
		}

		private bool HasFoundWeapon()
		{
			return Pickable != null && Pickable.IsWeapon();
		}

		private void MoveNextToMedKit()
		{
			if (isNextToMedKit) return;
			
			Mover.MoveTowardsTarget(Pickable.transform);

			isNextToMedKit = (Vector3.Distance(transform.root.position, Pickable.transform.position) < MinimumDistanceToPickable);
			savedMedKit = Pickable;
		}

		private bool DoesEnemyNeedMedKit()
		{
			return Enemy.Health.HealthPoints < 30;
		}

		private void ResetAct()
		{
			Pickable = null;
			Target = null;
			isNextToMedKit = false;
		}
	}
}
