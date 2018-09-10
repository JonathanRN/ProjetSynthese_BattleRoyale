using System;
using UnityEngine;

namespace Playmode.Entity.Movement
{
    public abstract class Mover : MonoBehaviour
    {
        //BEN_REVIEW : Débutez par une minuscule même si c'est protected.
        [SerializeField] protected float OutOfRangeRotationSpeed = 5f;
        
        public static readonly Vector3 Forward = Vector3.up;
        public static readonly Vector3 Backward = Vector3.down;
        
        public const float Clockwise = 1f;
        public const float NormalRotateSpeed = 200f;
        public const float HitReactRotateSpeed = 400f;
        
        //BEN_CORRECTION : Attributs devraient être privés. Créez propriétés si accès extérieur nécessaire.
        public float MoveSpeed = 4;
        public float RotateSpeed = NormalRotateSpeed;
        
        protected void Awake()
        {
            ValidateSerialisedFields(); 
        }

        private void ValidateSerialisedFields()
        {
            if (MoveSpeed < 0)
                throw new ArgumentException("Speed can't be lower than 0.");
            if (RotateSpeed < 0)
                throw new ArgumentException("RotateSpeed can't be lower than 0.");
        }

        public abstract void Move(Vector3 direction);

        public abstract void Rotate(float direction);

        public abstract void MoveTowardsTarget(Transform target);

        public abstract void RotateTowardsTarget(Transform target);

        public abstract void RotateTowardsARotation(Quaternion rotation);

        public abstract void HitReact();
    }
}