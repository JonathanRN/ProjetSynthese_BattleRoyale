using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using Playmode.Entity.Senses;
using Playmode.Util.Values;

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
        private TimedRotation timedRotation;
        public float senseRotation = 1f;

        [SerializeField]
        float cameraHalfHeight = 8.5f;
        [SerializeField]
        float cameraHalfWidth = 24.5f;
        

        
    

        public NormalStrategy(Mover mover, HandController handController, EnnemySensor enemySensor, Transform transformer,TimedRotation timedRotation)
        {
            this.mover = mover;
            this.handController = handController;
            this.transformer = transformer;
            this.timedRotation = timedRotation;



            this.enemySensor = enemySensor;
            enemySensor.OnEnnemySeen += OnEnnemySeen;
            enemySensor.OnEnnemySightLost += OnEnnemySightLost;
            timedRotation.OnRotationChanged += OnRotationChanged;
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
        private void OnRotationChanged()
        {
            if (target == null)
            {
                senseRotation *= -1;
            }
        }
      
        public void Act()
        {
                        
            if(target != null)
            {
                mover.Move(new Vector3(0, 3));
                vectorBetweenEnemy = new Vector3(transformer.position.x - target.transform.position.x, transformer.position.y - target.transform.position.y);
                if(Vector3.Dot(vectorBetweenEnemy,transformer.right) < -0.5)
                {
                    mover.Rotate(1f * Time.deltaTime);
                }
                else if(Vector3.Dot(vectorBetweenEnemy, transformer.right) > 0.5)
                {
                    mover.Rotate(-1f * Time.deltaTime);
                }
                else
                {
                    handController.Use();
                }
             
            }
            else
            {
                if (transformer.position.y >= cameraHalfHeight || transformer.position.y <= -cameraHalfHeight || transformer.position.x >= cameraHalfWidth || transformer.position.x <= -cameraHalfWidth)
                {

                    mover.Move(new Vector3(0, 1));
                    //transformer.SetPositionAndRotation(transformer.position, new Quaternion(0, 0, 180, 0));
                    float speed = 2;
                    transformer.rotation = Quaternion.Slerp(transformer.rotation, Quaternion.Euler(0, 0, 180), Time.deltaTime * speed);
                    
                }
                else
                {
                    mover.Move(new Vector3(0, 3));
                    mover.Rotate(senseRotation);
                }
            }

        
            
            
           
        }
    }
}
