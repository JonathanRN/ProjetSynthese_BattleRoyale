using Playmode.Pickables;
using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropTimerController : MonoBehaviour {

	private const string Format = "{0:00}";

	private Text text;
	private PickableSpawner pickableSpawner;

	private void Awake()
	{
		pickableSpawner = GameObject.FindWithTag(Tags.PickableSpawner).GetComponent<PickableSpawner>();
		text = GetComponent<Text>();
	}

	private void Update()
	{
		text.text = "Next drop in: " + pickableSpawner.spawnTimeDelay.ToString();
	}
}
