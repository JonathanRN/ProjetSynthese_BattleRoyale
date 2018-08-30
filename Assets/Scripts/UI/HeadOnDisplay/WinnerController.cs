using Playmode.Pickables;
using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerController : MonoBehaviour
{
	private Text text;
	
	private NumberOfPlayerLeftController numberOfPlayerLeftController;

	[SerializeField] private GameObject[] objectsToDisable;

	private void Awake()
	{
		text = GetComponent<Text>();
		numberOfPlayerLeftController = transform.parent.GetComponentInChildren<NumberOfPlayerLeftController>();
		text.text = "";
	}

	private void Update()
	{
		if (!numberOfPlayerLeftController.ThereIsAWinner()) return;
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
