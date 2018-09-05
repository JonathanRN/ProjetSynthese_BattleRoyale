using UnityEngine;

namespace Playmode.Environment
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] public float ZoneChangeDelay =30f;
		[SerializeField] private float zoneMinSize = 6f;
		[SerializeField] private float shrinkingSpeed = 0.01f;
		
		public float CameraHalfHeight { get; set; }
		public float CameraHalfWidth { get; set; }
		
		private Camera camera;
		private ZoneShrinkingTimer zoneShrinkingTimer;
		
		public bool ZoneIsShrinking;

		private void Awake()
		{
			camera = Camera.main;
			InitializeComponents();
		}

		private void InitializeComponents()
		{
			zoneShrinkingTimer = GetComponent<ZoneShrinkingTimer>();
		}

		private void OnEnable()
		{
			zoneShrinkingTimer.OnZoneChanged += NotifyZoneChanged;
		}

		private void OnDisable()
		{
			zoneShrinkingTimer.OnZoneChanged -= NotifyZoneChanged;
		}

		private void Update ()
		{
			if (ZoneIsShrinking && camera.orthographicSize > zoneMinSize)
			{
				camera.orthographicSize -= shrinkingSpeed;
			}
			FixCameraBounds();
		}

		private void NotifyZoneChanged()
		{
			ZoneIsShrinking = !ZoneIsShrinking;
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
