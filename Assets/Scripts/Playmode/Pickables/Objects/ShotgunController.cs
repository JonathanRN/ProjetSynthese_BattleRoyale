using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : PickableUse {

	public override void Use(GameObject enemy)
	{
		Debug.Log("Used a shotgun!");
	}
}
