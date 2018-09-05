using Playmode.Pickables;
using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerController : MonoBehaviour
{
	private Text text;
	
	private EnemiesCounter _enemiesCounter;

	[SerializeField] private GameObject[] objectsToDisable;

	private void Awake()
	{
		text = GetComponent<Text>();
		_enemiesCounter = transform.parent.GetComponentInChildren<EnemiesCounter>();
		text.text = "";
	}

	private void Update()
	{
		if (!_enemiesCounter.ThereIsAWinner()) return;
		text.text = "Winner Winner Chicken Dinner";
		DisableObjects();
	}

	private void DisableObjects()
	{
		foreach (var obj in objectsToDisable)
		{
			obj.SetActive(false);
		}
	}
}
