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
            if(other.tag == "Sight")
            {
                other.GetComponent<Entity.Senses.PickableSensor>()?.PickableSeen(transform.parent.gameObject);
            }
            else
            {
                other.GetComponent<Entity.Senses.PickableSensor>()?.PickUp(transform.parent.gameObject);
            }
        }
	}
}
