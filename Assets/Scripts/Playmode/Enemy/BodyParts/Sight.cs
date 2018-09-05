using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Ennemy.BodyParts
{
	public class Sight : MonoBehaviour
	{
		public void LookTowardsTarget(GameObject target)
		{
			var vectorBetweenEnemy = new Vector3(transform.position.x - target.transform.position.x, transform.position.y - target.transform.position.y);
			if (Vector3.Dot(vectorBetweenEnemy, transform.right) < -0.5)
			{
				transform.Rotate(Vector3.forward,-1);
			}
			else if (Vector3.Dot(vectorBetweenEnemy, transform.right) > 0.5)
			{
				transform.Rotate(Vector3.forward,1);
			}
		}
	}
}
