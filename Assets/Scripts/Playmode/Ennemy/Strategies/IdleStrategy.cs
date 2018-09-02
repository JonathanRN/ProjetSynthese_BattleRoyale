using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
	public class IdleStrategy : BaseEnnemyStrategy
	{

		private readonly Mover mover;
		private readonly HandController handController;

		public IdleStrategy(Mover mover, HandController handController)
		{
			this.mover = mover;
			this.handController = handController;
		}

		public void Act()
		{

		}
	}
}
