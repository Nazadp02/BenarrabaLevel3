using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceListenerBS : MonoBehaviour
{
    public SlicerBS slicer;
    private void OnTriggerEnter(Collider other)
    {
        slicer.isTouched = true;
    }
}
