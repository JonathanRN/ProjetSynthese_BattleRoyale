using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using Playmode.Entity.Senses;

namespace Playmode.Ennemy.Strategies
{
    public class NormalStrategy : IEnnemyStrategy
    {
        private readonly Mover mover;
        private readonly HandController handController;
        private EnnemyController enemyController;
        private EnnemySensor enemySensor;
        private GameObject target;
        private Transform transformer;
        private Vector3 vectorBetweenEnemy;
        



        public NormalStrategy(Mover mover, HandController handController, EnnemySensor enemySensor, Transform transformer)
        {
            this.mover = mover;
            this.handController = handController;
            this.transformer = transformer;

            this.enemySensor = enemySensor;
            enemySensor.OnEnnemySeen += OnEnnemySeen;
            enemySensor.OnEnnemySightLost += OnEnnemySightLost;
        }

        private void OnEnnemySeen(EnnemyController ennemy)
        {
            Debug.Log("I've seen an ennemy!! Ya so dead noob!!!");
            target = ennemy.gameObject;
        }

        private void OnEnnemySightLost(EnnemyController ennemy)
        {
            Debug.Log("I've lost sight of an ennemy...Yikes!!!");
           
        }


        public void Act()
        {
            mover.Move(new Vector3(0, 3));
            if(target != null)
            {
                vectorBetweenEnemy = new Vector3(transformer.position.x - target.transform.position.x, transformer.position.y - target.transform.position.y);
                if(Vector3.Dot(vectorBetweenEnemy,transformer.right) < -0.5)
                {
                    mover.Rotate(10f * Time.deltaTime);
                }
                else if(Vector3.Dot(vectorBetweenEnemy, transformer.right) > 0.5)
                {
                    mover.Rotate(-10f * Time.deltaTime);
                }
                else
                {
                    handController.Use();
                }
             
            }
            
           
        }
    }
}
