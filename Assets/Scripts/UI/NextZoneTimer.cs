using System.Collections;
using Playmode.Environment;
using Playmode.Util.Values;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class NextZoneTimer : MonoBehaviour {

		private float countDownTimer = 0f;

		private Text text;
		private CameraController cameraController;

		private void Awake()
		{
			InitializeComponents();

			ResetTimer();
		}

		private void InitializeComponents()
		{
			cameraController = GameObject.FindWithTag(Tags.MainCamera).GetComponent<CameraController>();
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
			countDownTimer = cameraController.ZoneChangeDelay;
		}

		private void Update()
		{
			if (cameraController.ZoneIsShrinking)
			{
				text.text = "Next zone in: Shrinking";
			}
			else
			{
				text.text = "Next zone in: " + countDownTimer;		
			}
			if (countDownTimer <= 0)
			{
				ResetTimer();
			}
		
		}
	}
}
