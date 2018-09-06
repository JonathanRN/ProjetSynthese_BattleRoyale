using Playmode.Util.Values;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class EnemiesCounter : MonoBehaviour {
	
		private Text text;

		private void Awake()
		{
			InitializeComponents();
		}

		private void InitializeComponents()
		{
			text = GetComponent<Text>();
		}

		private float CountEnemiesLeft()
		{
			return GameObject.FindGameObjectsWithTag(Tags.Enemy).Length;
		}

		public bool IsThereAWinner()
		{
			return CountEnemiesLeft() < 2;
		}

		private void Update()
		{
			text.text = "Enemies left: " + CountEnemiesLeft();
		}
	}
}
