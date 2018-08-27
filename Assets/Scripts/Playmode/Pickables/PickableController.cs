using Playmode.Entity.Senses;
using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableController : MonoBehaviour {

	public PickableTypes pickableType { get; set; }

	[Header("Pickup prefabs")]
	[SerializeField] public GameObject[] pickables;
}
