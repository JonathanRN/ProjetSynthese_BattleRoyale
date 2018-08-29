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
		private float distanceBetweenEnemy;
		private bool isOutOfMap;
		private bool needMedKit;
		private GameObject pickable;
		private PickableType pickableType;

		private bool hasFoundMedKit;

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
			
			SearchForMedKit();
			if (hasFoundMedKit)
			{
				MoveTowardsMedKit();
			}
			else
			{
				RoamAround();
			}
		}

		private void SearchForMedKit()
		{
			if (hasFoundMedKit) return;
			if (pickable == null) return;
			if (!pickableType.IsMedKit()) return;
			
			Debug.Log("I found a medkit!");
			hasFoundMedKit = true;
		}

		private void MoveTowardsMedKit()
		{
			if (pickable == null) return;
			
			enemyController.MoveTowardsTarget(pickable.transform);
			
			//TODO: set hasFoundMedKit to false when enemy is next to it
		}
		
		private void RoamAround()
		{
			enemyController.Roam();
		}
	}
}
