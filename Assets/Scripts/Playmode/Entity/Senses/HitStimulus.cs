using System;
using UnityEngine;

namespace Playmode.Entity.Senses
{
	public delegate void HitStimulusEventHandler();

	public class HitStimulus : MonoBehaviour
	{
		[Header("Behaviour")] [SerializeField] private int hitPoints = 10;

		public event HitStimulusEventHandler OnHit;

		private void NotifyHit()
		{
			if (OnHit != null) OnHit();
		}
		
		private void Awake()
		{
			ValidateSerializeFields();
		}

		private void ValidateSerializeFields()
		{
			if (hitPoints < 0)
				throw new ArgumentException("Hit points can't be less than 0.");
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<HitSensor>() == null) return;
			
			other.GetComponent<Entity.Senses.HitSensor>()?.Hit(hitPoints);
			NotifyHit();
		}
	}
}