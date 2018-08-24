using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Entity.Senses
{
	public delegate void PickableSensorEventHandler(GameObject pickable);

	public class PickableSensor : MonoBehaviour
	{
		public event PickableSensorEventHandler OnPickUp;

		public void PickUp(GameObject pickable)
		{
			NotifyPickUp(pickable);
		}

		private void NotifyPickUp(GameObject pickable)
		{
			if (OnPickUp != null)
			{
				OnPickUp(pickable);
			}
		}
	}
}
