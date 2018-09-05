using System;
using Playmode.Entity.Destruction;
using Playmode.Entity.Movement;
using Playmode.Entity.Senses;
using UnityEngine;

namespace Playmode.Bullet
{
	public class BulletController : MonoBehaviour
	{
		[Header("Behaviour")] [SerializeField] private float lifeSpanInSeconds = 5f;

		private Mover mover;
		private Destroyer destroyer;
		private float timeSinceSpawnedInSeconds;

		private HitStimulus hitStimulus;

		private bool IsAlive => timeSinceSpawnedInSeconds < lifeSpanInSeconds;

		private void Awake()
		{
			ValidateSerialisedFields();
			InitialzeComponent();

			mover.MoveSpeed = 40;
		}

		private void OnEnable()
		{
			hitStimulus.OnHit += OnHit;
		}

		private void OnDisable()
		{
			hitStimulus.OnHit -= OnHit;
		}

		private void OnHit()
		{
			destroyer.Destroy();
		}

		private void ValidateSerialisedFields()
		{
			if (lifeSpanInSeconds < 0)
				throw new ArgumentException("LifeSpan can't be lower than 0.");
		}

		private void InitialzeComponent()
		{
			mover = GetComponent<RootMover>();
			destroyer = GetComponent<RootDestroyer>();
			hitStimulus = transform.root.GetComponentInChildren<HitStimulus>();

			timeSinceSpawnedInSeconds = 0;
		}

		private void Update()
		{
			UpdateLifeSpan();

			Act();
		}

		private void UpdateLifeSpan()
		{
			timeSinceSpawnedInSeconds += Time.deltaTime;
		}

		private void Act()
		{
			if (IsAlive)
				mover.Move(Mover.Forward);
			else
				destroyer.Destroy();
		}
	}
}