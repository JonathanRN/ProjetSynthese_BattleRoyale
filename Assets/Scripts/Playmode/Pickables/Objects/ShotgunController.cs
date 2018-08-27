using Playmode.Ennemy.BodyParts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : PickableUse {

	[SerializeField] private GameObject handPrefab;
	private HandController handController;

	private void Awake()
	{
		handController = handPrefab.GetComponent<HandController>();
	}

	public override void Use(GameObject enemy)
	{
		Debug.Log("Used a shotgun!");
		handController.Hold(transform.root.gameObject);
	}
}
