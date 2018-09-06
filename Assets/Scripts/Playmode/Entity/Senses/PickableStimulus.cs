using Playmode.Pickables;
using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Entity.Senses
{
	public class PickableStimulus : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{	
			if(other.CompareTag(Tags.Sight))
            {
                other.transform.root.GetComponentInChildren<PickableSensor>()?.PickableSeen(transform.parent.GetComponentInChildren<Pickable>());
            }
            else
            {
                other.transform.root.GetComponentInChildren<PickableSensor>()?.PickUp(transform.parent.GetComponentInChildren<Pickable>());
            }
		}
	}
}
