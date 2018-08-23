using Playmode.Entity.Senses;
using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableController : MonoBehaviour {

	[Header("Type Images")] [SerializeField] private Sprite medicalKitSprite;
	[SerializeField] private Sprite shotgunSprite;
	[SerializeField] private Sprite uziSprite;

	[Header("Objects")]
	[SerializeField] private GameObject visual;

	private PickableSensor pickableSensor;

	private void Awake()
	{
		pickableSensor = GetComponent<PickableSensor>();
	}

	private void OnEnable()
	{
		pickableSensor.OnPickUp += OnPickUp;
	}

	private void OnDisable()
	{
		pickableSensor.OnPickUp -= OnPickUp;
	}

	private void OnPickUp(GameObject pickable)
	{
		Debug.Log("An item got picked up!");
	}

	public void ConfigureSprite(PickableTypes pickableType)
	{
		switch (pickableType)
		{
			case PickableTypes.MedicalKit:
				visual.GetComponent<SpriteRenderer>().sprite = medicalKitSprite;
				break;
			case PickableTypes.Shotgun:
				visual.GetComponent<SpriteRenderer>().sprite = shotgunSprite;
				break;
			case PickableTypes.Uzi:
				visual.GetComponent<SpriteRenderer>().sprite = uziSprite;
				break;
			default:
				visual.GetComponent<SpriteRenderer>().sprite = medicalKitSprite;
				break;
		}
	}

}
