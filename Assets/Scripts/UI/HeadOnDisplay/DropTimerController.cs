using Playmode.Pickables;
using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropTimerController : MonoBehaviour {

	private float countDownTimer = 0f;

	private Text text;
	private PickableSpawner pickableSpawner;

	private void Awake()
	{
		pickableSpawner = GameObject.FindWithTag(Tags.PickableSpawner).GetComponent<PickableSpawner>();
		text = GetComponent<Text>();

		ResetTimer();
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
		countDownTimer = pickableSpawner.spawnTimeDelay;
	}

	private void Update()
	{
		text.text = "Next drop in: " + countDownTimer.ToString();
		if (countDownTimer <= 0)
		{
			ResetTimer();
		}
	}
}
