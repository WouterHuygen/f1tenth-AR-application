/// Taken from the following tutorial:
/// https://unity3d.com/learn/tutorials/modules/intermediate/live-training-archive/modal-window

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringToFront : MonoBehaviour
{
    void OnEnable()
    {
        transform.SetAsLastSibling();
    }
}
