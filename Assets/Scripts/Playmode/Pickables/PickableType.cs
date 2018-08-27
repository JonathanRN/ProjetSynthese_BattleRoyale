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
        if(type != PickableTypes.MedicalKit)
            return true;
        else
            return false;
    }

    public bool IsMedKit()
    {
        if (type == PickableTypes.MedicalKit)
            return true;
        else
            return false;
    }

}
