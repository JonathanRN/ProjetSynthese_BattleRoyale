using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	// Use this for initialization
	private Camera camera;
	private ZoneShrinkingTimer zoneShrinkingTimer;
	private bool zoneIsShrinking;
	[SerializeField] private float zoneMinSize = 6f;
	[SerializeField] private float shrinkingSpeed = 0.01f;

void Awake() {
		camera = Camera.main;
		zoneShrinkingTimer = GetComponent<ZoneShrinkingTimer>();
	}

	private void OnEnable()
	{
		zoneShrinkingTimer.OnZoneChanged += OnZoneChanged;
	}

	// Update is called once per frame
	void Update ()
	{
		if (zoneIsShrinking && camera.orthographicSize > zoneMinSize)
		{
			camera.orthographicSize -= shrinkingSpeed;
		}
	}

	private void OnZoneChanged()
	{
		zoneIsShrinking = !zoneIsShrinking;
	}
}
