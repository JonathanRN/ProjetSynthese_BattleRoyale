using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickable : MonoBehaviour
{
	//TODO
	//public abstract PickableTypes Type { get;}
	public abstract void Use(GameObject enemy);
}
