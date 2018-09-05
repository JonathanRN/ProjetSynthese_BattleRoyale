using UnityEngine;

namespace Playmode.Environment
{
	public class WallController : MonoBehaviour
	{
		[SerializeField]private float wallAdjustX = 3f;
		[SerializeField] private float wallAdjustY = 3f;
		
		private EdgeCollider2D wall;
		public Vector2[] NewVerticies;
		private CameraController cameraController;

		private void Awake ()
		{
			InitializeComponents();

			NewVerticies  = new Vector2[5];
			AdjustPointsToCamera();
		}

		private void InitializeComponents()
		{
			wall = GetComponent<EdgeCollider2D>();
			cameraController = GetComponentInParent<CameraController>();
		}
		
		private void Update () 
		{
			AdjustPointsToCamera();
		}

		private void AdjustPointsToCamera()
		{
			NewVerticies[0] = new Vector2(-cameraController.CameraHalfWidth - wallAdjustX,
				-cameraController.CameraHalfHeight - wallAdjustY);
			NewVerticies[1] = new Vector2(cameraController.CameraHalfWidth + wallAdjustX,
				-cameraController.CameraHalfHeight - wallAdjustY);
			NewVerticies[2] = new Vector2(cameraController.CameraHalfWidth + wallAdjustX,
				cameraController.CameraHalfHeight + wallAdjustY);
			NewVerticies[3] = new Vector2(-cameraController.CameraHalfWidth - wallAdjustX,
				cameraController.CameraHalfHeight + wallAdjustY);
			NewVerticies[4] = new Vector2(-cameraController.CameraHalfWidth - wallAdjustX,
				-cameraController.CameraHalfHeight - wallAdjustY);

			wall.points = NewVerticies;
		}
	}
}
