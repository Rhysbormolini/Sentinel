using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCappa : MonoBehaviour
{
    [SerializeField] int target = 30;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
    }
}
