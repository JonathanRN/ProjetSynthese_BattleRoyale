using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Movement;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class TurnAndShootStragegy : IEnnemyStrategy
    {
        private readonly Mover mover;
        private readonly HandController handController;

        public TurnAndShootStragegy(Mover mover, HandController handController, EnnemySensor enemySensor, Transform transformer)
        {
            this.mover = mover;
            this.handController = handController;
        }

        public void Act()
        {
            mover.Rotate(Mover.Clockwise);

            handController.Use();
        }
    }
}