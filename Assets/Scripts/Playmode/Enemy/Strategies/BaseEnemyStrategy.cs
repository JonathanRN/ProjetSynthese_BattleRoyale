using Playmode.Entity.Movement;
using Playmode.Entity.Senses;
using Playmode.Environment;
using Playmode.Pickables;
using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Enemy.Strategies
{
	public abstract class BaseEnemyStrategy : MonoBehaviour
	{
		protected Mover Mover;
		protected Enemy Enemy;
		protected CameraController CameraController;
		protected EnemySensor EnemySensor;
		protected GameObject Target;
		protected PickableSensor PickableSensor;
		protected Pickable Pickable;

		protected float DistanceBetweenEnemy;
		protected float MaxDistanceBetweenEnemy = 5f;

		protected void Awake()
		{
			InitializeComponents();
		}

		protected void OnEnable()
		{
			EnemySensor.OnEnemySeen += OnEnemySeen;
			EnemySensor.OnEnemySightLost += OnEnemySightLost;
			PickableSensor.OnPickableSeen += OnPickableSeen;
		}

		protected void OnDisable()
		{
			EnemySensor.OnEnemySeen -= OnEnemySeen;
			EnemySensor.OnEnemySightLost -= OnEnemySightLost;
			PickableSensor.OnPickableSeen -= OnPickableSeen;
		}

		private void InitializeComponents()
		{
			Mover = GetComponent<RootMover>();
			Enemy = GetComponent<Enemy>();
			CameraController = GameObject.FindWithTag(Tags.MainCamera).GetComponent<CameraController>();
			PickableSensor = transform.root.GetComponentInChildren<PickableSensor>();
			EnemySensor = transform.root.GetComponentInChildren<EnemySensor>();
		}

		private void Update()
		{
			Act();
		}

		protected abstract void Act();

		protected void OnEnemySeen(Enemy enemy)
		{
			if (HasTarget()) return;
			Target = enemy.gameObject;
		}

		protected void OnEnemySightLost(Enemy enemy)
		{
			Target = null;
		}

		protected void OnPickableSeen(Pickable pickable)
		{
			this.Pickable = pickable;
		}

		protected bool IsPickableAWeapon()
		{
			return Pickable != null && Pickable.IsWeapon();
		}

		protected void CalculateDistanceBetweenEnemies()
		{
			if (HasTarget())
			{
				DistanceBetweenEnemy = Vector3.Distance(transform.position, Target.transform.position);
			}
		}

		protected bool HasTarget()
		{
			return Target != null;
		}
	}

	public enum EnemyStrategy
	{
		Normal,
		Careful,
		Cowboy,
		Camper
	}
}