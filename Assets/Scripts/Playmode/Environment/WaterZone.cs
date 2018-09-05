using Playmode.Entity.Senses;
using UnityEngine;

namespace Playmode.Environment
{
	public class WaterZone : MonoBehaviour {

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<HitSensor>() == null) return;
			
			other.transform.root.GetComponentInChildren<Enemy.Enemy>().SetSpeedToSwim();
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.GetComponent<HitSensor>() == null) return;
			
			other.transform.root.GetComponentInChildren<Enemy.Enemy>().SetSpeedToWalk();
		}
	}
}