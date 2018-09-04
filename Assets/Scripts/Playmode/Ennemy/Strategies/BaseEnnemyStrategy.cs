using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Environment;
using Playmode.Movement;
using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class BaseEnnemyStrategy : MonoBehaviour
    {
        protected Mover mover;
        protected Enemy enemyController;
        protected CameraController cameraController;
        protected EnnemySensor enemySensor;
        protected GameObject target;
        protected PickableSensor pickableSensor;
        protected GameObject pickable;
        protected PickableType pickableType;
        
        protected float distanceBetweenEnemy;
        protected float maxDistanceBetweenEnemy = 5f;
      
        private void Awake()
        {
            mover = GetComponent<RootMover>();
            enemyController = GetComponent<Enemy>();
            cameraController = GameObject.FindWithTag(Tags.MainCamera).GetComponent<CameraController>();
            pickableSensor = transform.root.GetComponentInChildren<PickableSensor>();
            enemySensor = transform.root.GetComponentInChildren<EnnemySensor>();
            
            enemySensor.OnEnnemySeen += OnEnnemySeen;
            enemySensor.OnEnnemySightLost += OnEnnemySightLost;
            pickableSensor.OnPickableSeen += OnPickableSeen;
        }

        private void Update()
        {
            Act();
        }
        
        protected virtual void Act()
        {
 
        }
        
        protected void OnEnnemySeen(Enemy ennemy)
        {
            if (target != null) return;
            target = ennemy.gameObject;
        }

        protected void OnEnnemySightLost(Enemy ennemy)
        {
            target = null;
        }
        protected void OnPickableSeen(GameObject pickable)
        {
            Debug.Log("I've seen a " + pickable.GetComponentInChildren<PickableType>().GetType());
            pickableType = pickable.GetComponentInChildren<PickableType>();
            this.pickable = pickable;
        }
        
        protected bool IsPickableAWeapon()
        {
            return pickable != null && pickableType.IsWeapon();
        }

        protected void CalculateDistanceBetweenEnemies()
        {
            if (HasTarget())
            {
                distanceBetweenEnemy = Vector3.Distance(transform.position, target.transform.position);
            }
        }
        
        protected bool HasTarget()
        {
            return target != null;
        }
    }

    public enum EnnemyStrategy
    {
        Normal,
        Careful,
        Cowboy,
        Camper
    }
}