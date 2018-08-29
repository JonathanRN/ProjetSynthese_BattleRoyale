using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableType : MonoBehaviour {

	[SerializeField] private PickableTypes type;

	public new PickableTypes GetType()
	{
		return type;
	}

    public bool IsWeapon()
    {
        return type != PickableTypes.MedicalKit;
    }

    public bool IsMedKit()
    {
        return type == PickableTypes.MedicalKit;
    }

}
