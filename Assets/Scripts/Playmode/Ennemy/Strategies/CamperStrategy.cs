using System.Collections;
using System.Collections.Generic;
using Playmode.Ennemy;
using Playmode.Ennemy.BodyParts;
using Playmode.Ennemy.Strategies;
using Playmode.Entity.Senses;
using Playmode.Environment;
using Playmode.Movement;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
	public class CamperStrategy : BaseEnnemyStrategy
	{
		private float currentRotationDirection = 1f;
		private bool isNextToMedKit;
		private GameObject savedMedKit;
		private const int MinimumDistanceToPickable = 2;

		protected override void Act()
		{
			/*Trouillard. Tente en premier lieu de trouver un MedicalKit. Une fois trouvé, il s’installe à côté
			et commence à tirer sur tout ce qui passe dans son champ de vision, sans bouger. Si sa vie
			est trop basse, il utilise le MedicalKit et se met à la recherche d’un autre MedicalKit. S’il croise
			une arme, il se dirige tout de suite dessus.*/
			
			if (HasFoundMedKit())
			{
				MoveNextToMedKit();
			}
			else if (!isNextToMedKit && !HasFoundWeapon())
			{
				RoamAround();
			}

			if (HasFoundWeapon() && !isNextToMedKit)
			{
				mover.MoveTowardsTarget(pickable.transform);
			}

			if (isNextToMedKit)
			{
				if (DoesEnemyNeedMedKit())
				{
					PickUpSavedMedKit();
					ResetAct();
				}
				else if (target != null)
				{
					enemyController.ShootTowardsTarget(target.transform);
				}
				else
				{
					ScanAround();
				}
			}

			if (gameController.IsObjectOutOfMap(transform.gameObject))
			{
				ResetAct();
			}
		}

		private void PickUpSavedMedKit()
		{
			if (savedMedKit != null)
			{
				mover.MoveTowardsTarget(savedMedKit.transform);
			}
		}

		private void ScanAround()
		{
			mover.Rotate(Mover.Clockwise);
		}

		private bool HasFoundMedKit()
		{
			return pickable != null && pickableType.IsMedKit();
		}

		private bool HasFoundWeapon()
		{
			return pickable != null && pickableType.IsWeapon();
		}

		private void MoveNextToMedKit()
		{
			if (isNextToMedKit) return;
			
			mover.MoveTowardsTarget(pickable.transform);

			isNextToMedKit = (Vector3.Distance(transform.root.position, pickable.transform.position) < MinimumDistanceToPickable);
			savedMedKit = pickable;
		}
		
		private void RoamAround()
		{
			enemyController.Roam();
		}

		private bool DoesEnemyNeedMedKit()
		{
			return enemyController.Health.HealthPoints < 30;
		}

		private void ResetAct()
		{
			pickable = null;
			target = null;
			isNextToMedKit = false;
		}
	}
}
