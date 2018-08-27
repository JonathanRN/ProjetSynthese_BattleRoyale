using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableType : MonoBehaviour {

	[SerializeField] private PickableTypes type;

	public new PickableTypes GetType()
	{
		return type;
	}
}
