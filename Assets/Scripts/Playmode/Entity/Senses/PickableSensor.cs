using Playmode.Pickables;
using UnityEngine;

namespace Playmode.Entity.Senses
{
	public delegate void PickableSensorEventHandler(Pickable pickable);

	public class PickableSensor : MonoBehaviour
	{
		public event PickableSensorEventHandler OnPickUp;
        public event PickableSensorEventHandler OnPickableSeen;
		
		public void PickUp(Pickable pickable)
		{
			NotifyPickUp(pickable);
		}

        public void PickableSeen(Pickable pickable)
        {
            NotifyPickableSeen(pickable);
        }

		private void NotifyPickUp(Pickable pickable)
		{
			if (OnPickUp != null)
			{
				OnPickUp(pickable);
			}
		}

        private void NotifyPickableSeen(Pickable pickable)
        {
            if (OnPickableSeen != null)
            {
                OnPickableSeen(pickable);
            }
        }
    }
}
