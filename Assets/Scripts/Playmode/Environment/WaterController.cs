using Playmode.Entity.Senses;
using UnityEngine;

namespace Playmode.Environment
{
	public delegate void WaterCollisionEventHandler();

	public class WaterController : MonoBehaviour {

		public event WaterCollisionEventHandler OnEnter;
		public event WaterCollisionEventHandler OnExit;
		public event WaterCollisionEventHandler OnStay;

		private void NotifyEnter()
		{
			if (OnEnter != null) OnEnter();
		}
		
		private void NotifyExit()
		{
			if (OnExit != null) OnExit();
		}

		private void NotifyStay()
		{
			if (OnStay != null) OnStay();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<HitSensor>() == null) return;
			NotifyEnter();
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.GetComponent<HitSensor>() == null) return;
			NotifyExit();
		}
	}
}