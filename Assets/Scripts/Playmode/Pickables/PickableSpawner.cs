using Playmode.Util.Values;
using System;
using System.Collections;
using System.Collections.Generic;
using Playmode.Environment;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Playmode.Pickables
{
	public class PickableSpawner : MonoBehaviour
	{
		[SerializeField] GameObject pickablePrefab;

		[Header("Timer")]
		[SerializeField] public float spawnTimeDelay = 5f;

		private PickableController pickableController;
		private GameObject[] pickables;

		private CameraController cameraController;

		private void Awake()
		{
			pickableController = pickablePrefab.GetComponent<PickableController>();
			cameraController = GameObject.FindWithTag(Tags.MainCamera).GetComponent<CameraController>();
			pickables = pickableController.pickables;
		}

		private void OnEnable()
		{
			StartCoroutine(TimedPickableSpawner());
		}

		private void Update()
		{
			DestroyOutOfMapSpawners();
		}

		private IEnumerator TimedPickableSpawner()
		{
			while (true)
			{
				yield return new WaitForSeconds(spawnTimeDelay);

				for (int i = 0; i < transform.childCount; i++)
				{
					SpawnPickable(transform.GetChild(i));
				}
			}
		}

		private void SpawnPickable(Transform childTransform)
		{
			if (childTransform.childCount <= 0)
			{
				Instantiate(pickables.GetRandom(), childTransform.position, Quaternion.identity, childTransform);
			}
		}

		private void RemoveAllPickables()
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				for (int j = 0; j < transform.GetChild(i).childCount; j++)
				{
					if (transform.GetChild(i).childCount == 1)
					{
						Destroy(transform.GetChild(i).GetChild(j).gameObject);
					}
				}
			}
		}

		private void DestroyOutOfMapSpawners()
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				var child = transform.GetChild(i).gameObject;
				if (cameraController.IsObjectOutOfMap(child))
				{
					Destroy(child);
				}
			}
		}
	}
}
