using Playmode.Ennemy;
using Playmode.Entity.Senses;
using Playmode.Entity.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
	private float health;
	private Quaternion rotation;
	private Vector3 localScale;

	private HitSensor hitSensor;

	private void OnEnable()
	{
		hitSensor.OnHit += OnHit;
	}

	private void OnDisable()
	{
		hitSensor.OnHit -= OnHit;
	}

	private void OnHit(int hitPoints)
	{
		health = transform.root.GetComponentInChildren<EnnemyController>().HealthPoints.HealthPoints;

		localScale.x *= health/100f;
		transform.localScale = localScale;
	}

	private void Awake()
	{
		rotation = transform.rotation;
		localScale = transform.localScale;

		hitSensor = transform.root.GetComponentInChildren<HitSensor>();
	}

	private void LateUpdate()
	{
		transform.position = transform.root.position + transform.up * 1.2f;
		transform.rotation = rotation;
	}
}
