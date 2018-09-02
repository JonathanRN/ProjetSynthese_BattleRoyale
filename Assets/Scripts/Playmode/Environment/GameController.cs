using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Environment
{
	public class GameController : MonoBehaviour
	{
		private CameraController cameraController;

		private void Awake()
		{
			cameraController = GameObject.FindWithTag(Tags.MainCamera).GetComponent<CameraController>();
		}

		public bool IsObjectOutOfMap(GameObject gameObject)
		{
			if (gameObject.transform.position.y >= cameraController.CameraHalfHeight)
				return true;
			if (gameObject.transform.position.y <= -cameraController.CameraHalfHeight)
				return true;
			if (gameObject.transform.position.x >= cameraController.CameraHalfWidth)
				return true;
			return gameObject.transform.position.x <= -cameraController.CameraHalfWidth;
		}
	}
}