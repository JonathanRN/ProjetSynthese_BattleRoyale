using Playmode.Util.Values;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Pickables
{
	public class PickableSpawner : MonoBehaviour
	{
        [SerializeField] private GameObject pickablePrefab;

		private void Start()
		{
			SpawnPickable(transform.GetChild(GetRandomSpawnPoint()).position);
		}

		private void SpawnPickable(Vector3 position)
		{
			Instantiate(pickablePrefab, position, Quaternion.identity)
				.GetComponentInChildren<PickableController>()
				.ConfigureSprite(GetRandomPickableType());
		}

		private int GetRandomSpawnPoint()
		{
			return new System.Random().Next(0, transform.childCount);
		}

		private PickableTypes GetRandomPickableType()
		{
			var random = Enum.GetValues(typeof(PickableTypes));
			return (PickableTypes)random.GetValue(new System.Random()
				.Next(random.Length));
		}

	}
}
