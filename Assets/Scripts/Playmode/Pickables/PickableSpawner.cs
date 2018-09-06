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
		[Header("Timer")]
		[SerializeField] public float SpawnTimeDelay = 5f;
		
		[Header("Pickup prefabs")]
		[SerializeField] private GameObject[] pickables;

		private CameraController cameraController;

		private void Awake()
		{
			InitializeComponents();
		}

		private void InitializeComponents()
		{
			cameraController = GameObject.FindWithTag(Tags.MainCamera).GetComponent<CameraController>();
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
				yield return new WaitForSeconds(SpawnTimeDelay);

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
