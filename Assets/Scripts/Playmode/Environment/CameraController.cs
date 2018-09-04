using UnityEngine;

namespace Playmode.Environment
{
	public class CameraController : MonoBehaviour
	{

		// Use this for initialization
		private Camera camera;
		private ZoneShrinkingTimer zoneShrinkingTimer;
		public bool zoneIsShrinking;
		[SerializeField] public float zoneChangeDelay =30f;
		[SerializeField] private float zoneMinSize = 6f;
		[SerializeField] private float shrinkingSpeed = 0.01f;
	
		public float CameraHalfHeight { get; set; }
		public float CameraHalfWidth { get; set; }

		void Awake()
		{
			camera = Camera.main;
			zoneShrinkingTimer = GetComponent<ZoneShrinkingTimer>();
		}

		private void OnEnable()
		{
			zoneShrinkingTimer.OnZoneChanged += OnZoneChanged;
		}

		// Update is called once per frame
		void Update ()
		{
			if (zoneIsShrinking && camera.orthographicSize > zoneMinSize)
			{
				camera.orthographicSize -= shrinkingSpeed;
			}
			FixCameraBounds();
		}

		private void OnZoneChanged()
		{
			zoneIsShrinking = !zoneIsShrinking;
		}

		private void FixCameraBounds()
		{
			CameraHalfHeight = camera.orthographicSize - 1.5f;
			CameraHalfWidth = CameraHalfHeight * camera.aspect + 1.5f;
		}
		
		public bool IsObjectOutOfMap(GameObject gameObject)
		{
			if (gameObject.transform.position.y >= CameraHalfHeight)
				return true;
			if (gameObject.transform.position.y <= -CameraHalfHeight)
				return true;
			if (gameObject.transform.position.x >= CameraHalfWidth)
				return true;
			return gameObject.transform.position.x <= -CameraHalfWidth;
		}
	}
}
