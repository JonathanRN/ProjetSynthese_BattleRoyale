using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Pickables.Objects
{
	public class Shotgun : Pickable
	{
		public override PickableTypes Type => PickableTypes.Shotgun;

		public override void Use(GameObject enemy)
		{
			//BEN_CORRECTION : Ça fait rien ? Erreur de design.
		}
	}
}