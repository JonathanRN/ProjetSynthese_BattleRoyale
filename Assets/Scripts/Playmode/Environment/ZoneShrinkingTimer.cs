using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Environment
{
	public delegate void ZoneChangedEventHandler();

	public class ZoneShrinkingTimer : MonoBehaviour {

		public event ZoneChangedEventHandler OnZoneChanged;

		//BEN_REVIEW : SerializeFields sont en haut (en premier) par convention.
		[SerializeField] private float timeBeforeShrink = 10f;

		private void OnEnable()
		{
			StartCoroutine(ZoneShrinkRoutine());
		}

		private IEnumerator ZoneShrinkRoutine()
		{
			while (true)
			{
				yield return new WaitForSeconds(timeBeforeShrink);
				NotifyZoneChanged();
			}
		}

		private void NotifyZoneChanged()
		{
			if (OnZoneChanged != null) OnZoneChanged();
		}
	}
}