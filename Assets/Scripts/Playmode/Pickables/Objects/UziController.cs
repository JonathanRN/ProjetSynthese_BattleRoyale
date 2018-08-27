using Playmode.Ennemy;
using Playmode.Ennemy.BodyParts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziController : PickableUse {

	[SerializeField] private GameObject handPrefab;
	private HandController handController;

	private void Awake()
	{
		handController = handPrefab.GetComponent<HandController>();
	}

	public override void Use(GameObject enemy)
	{
		Debug.Log("Used a Uzi!");
		HandController.currentWeapon = transform.root.gameObject;
	}
}
