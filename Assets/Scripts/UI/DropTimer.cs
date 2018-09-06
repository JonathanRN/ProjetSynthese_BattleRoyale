using System.Collections;
using Playmode.Pickables;
using Playmode.Util.Values;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class DropTimer : MonoBehaviour {

		private float countDownTimer = 0f;

		private Text text;
		private PickableSpawner pickableSpawner;

		private void Awake()
		{
			InitializeComponents();

			ResetTimer();
		}

		private void InitializeComponents()
		{
			pickableSpawner = GameObject.FindWithTag(Tags.PickableSpawner).GetComponent<PickableSpawner>();
			text = GetComponent<Text>();
		}

		private void OnEnable()
		{
			StartCoroutine(TimerDropCountdown());
		}

		private IEnumerator TimerDropCountdown()
		{
			while (true)
			{
				yield return new WaitForSeconds(1f);
				countDownTimer--;
			}
		}

		private void ResetTimer()
		{
			countDownTimer = pickableSpawner.SpawnTimeDelay;
		}

		private void Update()
		{
			text.text = "Next drop in: " + countDownTimer;
			if (countDownTimer <= 0)
			{
				ResetTimer();
			}
		}
	}
}
