using UnityEngine;

namespace Playmode.Environment
{
	public class WallController : MonoBehaviour
	{

		private EdgeCollider2D wall;
		public Vector2[] newVerticies;
		private CameraController cameraController;
		
		[SerializeField]private float wallAdjustX = 3f;
		[SerializeField] private float wallAdjustY = 3f;

		private void Awake ()
		{
			newVerticies  = new Vector2[5];
			wall = GetComponent<EdgeCollider2D>();
			cameraController = GetComponentInParent<CameraController>();
			AdjustPointsToCamera();
		}

		private void AdjustPointsToCamera()
		{
			newVerticies[0] = new Vector2(-cameraController.CameraHalfWidth - wallAdjustX, -cameraController.CameraHalfHeight - wallAdjustY);
			newVerticies[1] = new Vector2(cameraController.CameraHalfWidth + wallAdjustX, -cameraController.CameraHalfHeight - wallAdjustY);
			newVerticies[2] = new Vector2(cameraController.CameraHalfWidth + wallAdjustX, cameraController.CameraHalfHeight + wallAdjustY);
			newVerticies[3] = new Vector2(-cameraController.CameraHalfWidth - wallAdjustX, cameraController.CameraHalfHeight + wallAdjustY);
			newVerticies[4] = new Vector2(-cameraController.CameraHalfWidth - wallAdjustX, -cameraController.CameraHalfHeight - wallAdjustY);
			
			wall.points = newVerticies;
		}

		private void Update () 
		{
			AdjustPointsToCamera();
		}
	}
}
