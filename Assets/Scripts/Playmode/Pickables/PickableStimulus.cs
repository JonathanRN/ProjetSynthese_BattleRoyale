using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Entity.Senses
{
	public class PickableStimulus : MonoBehaviour
	{
		private PickableController pickableController;

		private void Awake()
		{
			pickableController = GetComponent<PickableController>();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			gameObject.SetActive(false);
		}
	}
}
