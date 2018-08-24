using Playmode.Entity.Destruction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Entity.Senses
{
	public class PickableStimulus : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			other.GetComponent<Entity.Senses.PickableSensor>()?.PickUp(transform.parent.gameObject);
		}
	}
}
