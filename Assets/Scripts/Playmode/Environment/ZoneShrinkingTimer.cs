using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Environment
{
	public delegate void ZoneChangedEventHandler();

	public class ZoneShrinkingTimer : MonoBehaviour {

		public event ZoneChangedEventHandler OnZoneChanged;

		[SerializeField] private float timeBeforeShrink = 10f;

		private void OnEnable()
		{
			StartCoroutine(ChangeRotationRoutine());
		}

		private IEnumerator ChangeRotationRoutine()
		{
			while (true)
			{
				yield return new WaitForSeconds(timeBeforeShrink);
				OnZoneChanged();
			}
		}

		public void ZoneChanged()
		{
			if (OnZoneChanged != null) OnZoneChanged();
		}
	}
}