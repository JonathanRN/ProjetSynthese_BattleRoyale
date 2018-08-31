using System.Collections;
using System.Collections.Generic;
using Playmode.Entity.Destruction;
using UnityEngine;

public class BloodSplashController : MonoBehaviour
{
	private ParticleSystem particleSystem;

	private void Awake()
	{
		particleSystem = GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		Act();
	}

	private void Act()
	{
		if (!particleSystem.IsAlive())
			Destroy(transform.gameObject);
	}
}
