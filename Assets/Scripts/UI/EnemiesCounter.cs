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

		//BEN_CORRECTION : Cela n'a pas d'affaire dans le UI. C'est de la logique de jeu, donc couche modèle.
		private float CountEnemiesLeft()
		{
			return GameObject.FindGameObjectsWithTag(Tags.Enemy).Length;
		}

		//BEN_CORRECTION : Cela n'a pas d'affaire dans le UI. C'est de la logique de jeu, donc couche modèle.
		public bool IsThereAWinner()
		{
			return CountEnemiesLeft() < 2;
		}

		private void Update()
		{
			//BEN_CORRECTION : String devrait être un SerializeField.
			//				   Aussi, utilisez "string.format".
			text.text = "Enemies left: " + CountEnemiesLeft();
		}
	}
}
