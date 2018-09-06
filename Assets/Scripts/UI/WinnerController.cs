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
			if (!enemiesCounter.IsThereAWinner()) return;
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
