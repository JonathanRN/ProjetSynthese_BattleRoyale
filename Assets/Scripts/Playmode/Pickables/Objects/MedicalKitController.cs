using Playmode.Ennemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalKitController : PickableUse
{

	public override void Use(GameObject enemy)
	{
		enemy.GetComponent<EnnemyController>().Health.Hit(-50);
		enemy.transform.parent.GetComponentInChildren<HealthBarController>().UpdateHealthBar();
		
		Debug.Log("I used a medkit!");
	}
}