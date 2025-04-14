using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haptic_Wrapper : MonoBehaviour
{
    private HapticTool tool;

    void Start()
    {
        tool = GetComponent<HapticTool>();
        tool.OnFailInitialize += TryInitHaptic;
    }

    private void TryInitHaptic()
    {
        Debug.Log("Enter try init");
        if (!tool.IsInitialize)
        {
            tool.ScanDevices();
            tool.Init();
        }
        if (tool.IsInitialize) tool.OnFailInitialize -= TryInitHaptic;
        Debug.Log($"Exit try init the value of init is: {tool.IsInitialize}");
    }
}
