using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void RotationChangedEventHandler();

public class TimedRotation : MonoBehaviour {

    public event RotationChangedEventHandler OnRotationChanged;

    [SerializeField]
    private float maxTimeBeforeChangingRotation = 1.2f;
    [SerializeField]
    private float minTimeBeforeChangingRotation = 0.8f;


    private void OnEnable()
    {
        StartCoroutine(ChangeRotationRoutine());
    }

    private IEnumerator ChangeRotationRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBeforeChangingRotation, maxTimeBeforeChangingRotation));
            RotationChange();
        }
    }

    public void RotationChange()
    {
        if (OnRotationChanged != null) OnRotationChanged();
    }
    

    
}
