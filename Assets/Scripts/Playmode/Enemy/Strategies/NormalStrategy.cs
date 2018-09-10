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
					//BEN_REVIEW : Je suis d'avis que vos strarégies auraient pu utiliser directement "Hand".
					//			   Après tout, vous le faites déjà avec "Mover".
					//
					//			   C'est le défaut de vos stratégies et de la classe "Ennemy" en général. Me voir pour de plus amples explications.
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