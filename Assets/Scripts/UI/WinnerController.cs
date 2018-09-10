using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class WinnerController : MonoBehaviour
	{
		[SerializeField] private GameObject[] objectsToDisable;
		
		private Text text;
		private EnemiesCounter enemiesCounter;

		private void Awake()
		{
			InitializeComponents();
			text.text = "";
		}

		private void InitializeComponents()
		{
			text = GetComponent<Text>();
			enemiesCounter = transform.parent.GetComponentInChildren<EnemiesCounter>();
		}

		private void Update()
		{
			//BEN_CORRECTION : Rider n'a pas toujours raison sur tout. Préférer un "if/else" pour la lisibilité.
			if (!enemiesCounter.IsThereAWinner()) return;
			//BEN_CORRECTION : String devrait être un SerializeField.
			text.text = "Winner Winner Chicken Dinner";
			DisableObjects();
		}

		private void DisableObjects()
		{
			foreach (var obj in objectsToDisable)
			{
				obj.SetActive(false);
			}
		}
	}
}
