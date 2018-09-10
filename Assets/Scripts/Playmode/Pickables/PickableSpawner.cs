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
		//BEN_CORRECTION : Aurait du être private. Si vous en avez besoin à l'extérieur, créer une propriété.
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
			//BEN_REVIEW : Hein ? Pourquoi ?
			//
			//			   ...
			//
			//			   Ah, je viens de comprendre. C'est pour ne pas spawner deux Pickables au même endroit.
			//
			//			   Une méthode d'extension aurait pu vous aider. Me voir.
			if (childTransform.childCount <= 0)
			{
				Instantiate(pickables.GetRandom(), childTransform.position, Quaternion.identity, childTransform);
			}
		}

		//BEN_CORRECTION : L'objet devrait se détruire lui même lorsqu'il est hors de la caméra. Intrusion dans les responsabilités
		//				   d'une autre classe.
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
