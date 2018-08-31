using System.Collections;
using System.Collections.Generic;
using Playmode.Entity.Senses;
using UnityEngine;

namespace Playmode
{
	public delegate void WaterCollisionEventHandler();

	public class WaterController : MonoBehaviour {

		public event WaterCollisionEventHandler OnEnter;
		public event WaterCollisionEventHandler OnExit;

		private void NotifyEnter()
		{
			if (OnEnter != null) OnEnter();
		}
		
		private void NotifyExit()
		{
			if (OnExit != null) OnExit();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<HitSensor>() == null) return;
			NotifyEnter();
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.GetComponent<HitSensor>() == null) return;
			NotifyExit();
		}
	}
}