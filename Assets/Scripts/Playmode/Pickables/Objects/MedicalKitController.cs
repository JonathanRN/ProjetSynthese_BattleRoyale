using Playmode.Ennemy;
using System.Collections;
using System.Collections.Generic;
using Playmode.Enemy;
using Playmode.Util.Values;
using UnityEngine;

public class MedicalKitController : Pickable
{
	//public override PickableTypes Type => PickableTypes.MedicalKit;

	public override void Use(GameObject enemy)
	{
		enemy.GetComponent<Enemy>().Health.Hit(-50);
		enemy.transform.parent.GetComponentInChildren<HealthBar>().UpdateHealthBar();
	}
}