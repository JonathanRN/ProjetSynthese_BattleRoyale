using UnityEngine;

namespace Playmode.Enemy.Display
{
	public class BloodParticles : MonoBehaviour
	{
		private new ParticleSystem particleSystem;

		private void Awake()
		{
			particleSystem = GetComponent<ParticleSystem>();
		}

		private void Update()
		{
			if (!particleSystem.IsAlive())
				Destroy(transform.gameObject);
		}
	}
}
