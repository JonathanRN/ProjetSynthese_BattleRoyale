using System.Collections;
using System.Collections.Generic;
using Playmode.Ennemy;
using Playmode.Entity.Senses;
using UnityEngine;

namespace Playmode
{
	public class WaterZone : MonoBehaviour {

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<HitSensor>() == null) return;
			
			other.transform.root.GetComponentInChildren<Enemy>().SetSpeedToSwim();
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.GetComponent<HitSensor>() == null) return;
			
			other.transform.root.GetComponentInChildren<Enemy>().SetSpeedToWalk();
		}
	}
}