using System;
using UnityEngine;

namespace Playmode.Movement
{
    public abstract class Mover : MonoBehaviour
    {
        public static readonly Vector3 Foward = Vector3.up;
        public static readonly Vector3 Backward = Vector3.down;
        public const float Clockwise = 1f;
        
        public float MoveSpeed = 4;
        [SerializeField] protected float rotateSpeed = 200f;

        protected void Awake()
        {
            ValidateSerialisedFields(); 
        }

        private void ValidateSerialisedFields()
        {
            if (MoveSpeed < 0)
                throw new ArgumentException("Speed can't be lower than 0.");
            if (rotateSpeed < 0)
                throw new ArgumentException("RotateSpeed can't be lower than 0.");
        }

        public abstract void Move(Vector3 direction);

        public abstract void Rotate(float direction);

        public abstract void MoveTowardsTarget(Transform target);

        public abstract void RotateTowardsTarget(Transform target);

    }
}