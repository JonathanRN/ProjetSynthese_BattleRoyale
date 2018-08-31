using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{

	private EdgeCollider2D wall;
	public Vector2[] newVerticies;
	private CameraController cameraController;
	[SerializeField]private float wallAdjustX = 3f;
	[SerializeField] private float wallAdjustY = 3f;

	// Use this for initialization
	void Awake ()
	{
		newVerticies  = new Vector2[5];
		wall = GetComponent<EdgeCollider2D>();
		cameraController = GetComponentInParent<CameraController>();
		AdjustPointsToCamera();
	}
	
	void getPointsToDebug() {
		foreach (Vector2 piste in wall.points) {
			Debug.Log(piste);
		}
	}

	void AdjustPointsToCamera()
	{
		newVerticies[0] = new Vector2(-cameraController.CameraHalfWidth - wallAdjustX, -cameraController.CameraHalfHeight - wallAdjustY);
		newVerticies[1] = new Vector2(cameraController.CameraHalfWidth + wallAdjustX, -cameraController.CameraHalfHeight - wallAdjustY);
		newVerticies[2] = new Vector2(cameraController.CameraHalfWidth + wallAdjustX, cameraController.CameraHalfHeight + wallAdjustY);
		newVerticies[3] = new Vector2(-cameraController.CameraHalfWidth - wallAdjustX, cameraController.CameraHalfHeight + wallAdjustY);
		newVerticies[4] = new Vector2(-cameraController.CameraHalfWidth - wallAdjustX, -cameraController.CameraHalfHeight - wallAdjustY);
	}
	
	void setPoints()
	{
		wall.points = newVerticies;
	}
	
	// Update is called once per frame
	void Update () {
		AdjustPointsToCamera();
		getPointsToDebug();
		setPoints();
	}
}
