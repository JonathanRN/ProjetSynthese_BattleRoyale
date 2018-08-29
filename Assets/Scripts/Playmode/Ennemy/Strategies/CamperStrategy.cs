using System.Collections;
using System.Collections.Generic;
using Playmode.Ennemy;
using Playmode.Ennemy.BodyParts;
using Playmode.Ennemy.Strategies;
using Playmode.Entity.Senses;
using Playmode.Movement;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
	public class CamperStrategy : IEnnemyStrategy
	{
		private readonly Mover mover;
		private readonly HandController handController;
		private EnnemyController enemyController;
		private EnnemySensor enemySensor;
		private PickableSensor pickableSensor;
		private GameObject target;
		private Transform enemyTransformer;
		private GameObject pickable;
		private PickableType pickableType;

		private bool hasFoundMedKit;
		private bool hasFoundWeapon;
		private bool hasPickedWeapon;
		private bool isNextToMedKit;
		private const int MinimumDistanceToPickable = 2;
		private GameObject lastMedKitFound;

		public CamperStrategy(Mover mover, HandController handController, EnnemySensor enemySensor,
			Transform transformer, TimedRotation timedRotation, EnnemyController enemyController,
			PickableSensor pickableSensor)
		{
			this.mover = mover;
			this.handController = handController;

			this.enemyTransformer = transformer;
			this.enemySensor = enemySensor;
			this.pickableSensor = pickableSensor;
			this.enemyController = enemyController;

			enemySensor.OnEnnemySeen += OnEnnemySeen;
			enemySensor.OnEnnemySightLost += OnEnnemySightLost;
			pickableSensor.OnPickableSeen += OnPickableSeen;
		}

		private void OnEnnemySeen(EnnemyController ennemy)
		{
			target = ennemy.gameObject;
		}

		private void OnEnnemySightLost(EnnemyController ennemy)
		{
			target = null;
		}

		private void OnPickableSeen(GameObject pickable)
		{
			Debug.Log("I've seen a " + pickable.GetComponentInChildren<PickableType>().GetType());
			pickableType = pickable.GetComponentInChildren<PickableType>();
			this.pickable = pickable;
		}

		public void Act()
		{
			/*Trouillard. Tente en premier lieu de trouver un MedicalKit. Une fois trouvé, il s’installe à côté
			et commence à tirer sur tout ce qui passe dans son champ de vision, sans bouger. Si sa vie
			est trop basse, il utilise le MedicalKit et se met à la recherche d’un autre MedicalKit. S’il croise
			une arme, il se dirige tout de suite dessus.*/
			
			if (hasFoundMedKit)
			{
				if (!hasPickedWeapon)
				{
					LookForWeapon();
				}

				if (!hasFoundWeapon)
				{
					RoamAround();
				}
			}
			else if (!hasPickedWeapon)
			{
				FindMedKit();
				RoamAround();
			}

			if (hasPickedWeapon)
			{
				hasFoundMedKit = false;
				MoveNextToFoundMedKit();
			}

			if (isNextToMedKit && hasPickedWeapon)
			{
				ScanAround();
				//shoot
			}
			
			/*if (HasFoundMedKit())
			{
				lastMedKitFound = pickable;
				Debug.Log("I found a medkit!");
				MoveNextToFoundMedKit();
				//LookForWeapon();

				/*if (hasPickedWeapon)
				{
					Debug.Log("Now moving back to medkit.");
					MoveNextToFoundMedKit();
				}#1#
			}
			else if(!isNextToMedKit)
			{
				RoamAround();
			}

			if (isNextToMedKit)
			{
				ScanAround();
				//shoot
			}*/
		}

		private void LookForWeapon()
		{
			FindWeapon();
			
			if (!hasFoundWeapon)
			{
				return;
			}

			//Move towards weapon
			mover.MoveTowardsTarget(pickable.transform);
				
			hasPickedWeapon = (Vector3.Distance(enemyTransformer.root.position, pickable.transform.position) < 2);
		}

		private void ScanAround()
		{
			mover.Rotate(Mover.Clockwise);
		}

		private void FindMedKit()
		{
			hasFoundMedKit = pickable != null && pickableType.IsMedKit();
			lastMedKitFound = pickable;
		}

		private void FindWeapon()
		{
			hasFoundWeapon = pickable != null && pickableType.IsWeapon();
		}

		private void MoveNextToFoundMedKit()
		{
			if (isNextToMedKit) return;
			
			mover.MoveTowardsTarget(lastMedKitFound.transform);

			isNextToMedKit = (Vector3.Distance(enemyTransformer.root.position, pickable.transform.position) < MinimumDistanceToPickable);
		}
		
		private void RoamAround()
		{
			enemyController.Roam();
		}
	}
}
