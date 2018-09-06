using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Pickables
{
	public abstract class Pickable : MonoBehaviour
	{
		public abstract PickableTypes Type { get;}
		public abstract void Use(GameObject enemy);

		public bool IsWeapon()
		{
			return Type != PickableTypes.MedicalKit;
		}

		public bool IsMedicalKit()
		{
			return Type == PickableTypes.MedicalKit;
		}
	}
}
