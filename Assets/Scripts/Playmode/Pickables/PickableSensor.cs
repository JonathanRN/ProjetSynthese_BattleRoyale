using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Entity.Senses
{
	public delegate void PickableSensorEventHandler(GameObject pickable);

	public class PickableSensor : MonoBehaviour
	{
		public event PickableSensorEventHandler OnPickUp;
        public event PickableSensorEventHandler OnPickableSeen;

		public bool IsSight => CompareTag("Sight");
		
		public void PickUp(GameObject pickable)
		{
			NotifyPickUp(pickable);
		}

        public void PickableSeen(GameObject pickable)
        {
            NotifyPickableSeen(pickable);
        }

		private void NotifyPickUp(GameObject pickable)
		{
			if (OnPickUp != null)
			{
				OnPickUp(pickable);
			}
		}

        private void NotifyPickableSeen(GameObject pickable)
        {
            if (OnPickableSeen != null)
            {
                OnPickableSeen(pickable);
            }
        }
    }
}
