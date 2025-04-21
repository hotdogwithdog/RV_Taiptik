using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HapticInteraction
{
    [RequireComponent(typeof(HapticTool))]
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
            if (tool.IsInitialize)
            {
                tool.OnFailInitialize -= TryInitHaptic;
                Debug.Log($"Haptic Initialization completed -> {tool.IsInitialize}");
            }
        }
    }
}

