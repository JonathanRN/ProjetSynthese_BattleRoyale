using Playmode.Pickables;
using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberOfPlayerLeftController : MonoBehaviour {
	
	private Text text;

	private void Awake()
	{
		text = GetComponent<Text>();
	}

	private float CountPlayersLeft()
	{
		 return GameObject.FindGameObjectsWithTag(Tags.Enemy).Length;
	}

	public bool ThereIsAWinner()
	{
		return CountPlayersLeft() < 2;
	}

	private void Update()
	{
		text.text = "OwO left:" + CountPlayersLeft().ToString();
	}
}
