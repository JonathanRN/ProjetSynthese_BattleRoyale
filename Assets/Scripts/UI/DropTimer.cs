using System.Collections;
using Playmode.Pickables;
using Playmode.Util.Values;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	//BEN_REVIEW : Le dossier "UI" devrait être dans le dossier "Playmode".
	
	public class DropTimer : MonoBehaviour {

		private float countDownTimer = 0f;

		private Text text;
		private PickableSpawner pickableSpawner;

		private void Awake()
		{
			InitializeComponents();

			ResetTimer();
		}

		private void InitializeComponents()
		{
			pickableSpawner = GameObject.FindWithTag(Tags.PickableSpawner).GetComponent<PickableSpawner>();
			text = GetComponent<Text>();
		}

		private void OnEnable()
		{
			//BEN_REVIEW : Au lieu d'avoir une coroutine qui met à jour l'interface à intervales, il aurait été mieux
			//			   de créer un événement dans "PickableSpawner" et de s'y abbonner.
			//
			//			   L'autre option, vu que c'est un Timer, est de mettre à jour l'interface à toutes les frames.
			//			   Puisque c'est un timer, je trouve cela acceptable.
			StartCoroutine(TimerDropCountdown());
		}

		private IEnumerator TimerDropCountdown()
		{
			while (true)
			{
				yield return new WaitForSeconds(1f);
				countDownTimer--;
			}
		}

		private void ResetTimer()
		{
			countDownTimer = pickableSpawner.SpawnTimeDelay;
		}

		private void Update()
		{
			//BEN_CORRECTION : String devrait être un SerializeField.
			text.text = "Next drop in: " + countDownTimer;
			if (countDownTimer <= 0)
			{
				ResetTimer();
			}
		}
	}
}
