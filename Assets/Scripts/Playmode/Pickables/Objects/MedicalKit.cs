using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Pickables.Objects
{
	public class MedicalKit : Pickable
	{
		public override PickableTypes Type => PickableTypes.MedicalKit;

		public override void Use(GameObject enemy)
		{
			enemy.GetComponent<Enemy.Enemy>().Health.Hit(-50);
			enemy.transform.parent.GetComponentInChildren<HealthBar>().UpdateHealthBar();
		}
	}
}