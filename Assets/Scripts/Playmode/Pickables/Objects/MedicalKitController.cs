using Playmode.Ennemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalKitController : PickableUse {

	public override void Use(GameObject enemy)
	{
		enemy.GetComponent<EnnemyController>().HealthPoints.Hit(-50);
		Debug.Log("I used a medkit!");
	}
}
