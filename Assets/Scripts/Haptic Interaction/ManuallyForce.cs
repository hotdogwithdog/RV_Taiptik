using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManuallyForce : MonoBehaviour
{
    private HapticTool tool;


    void Start()
    {
        tool = GameObject.FindGameObjectWithTag("HapticDevice").GetComponent<HapticTool>();
        if (tool.IsInitialize) Init();
        else tool.OnInitialize += Init;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tool.IsInitialize)
        {
            if (other.tag == "HapticDevice")
            {
                UnityHaptics.SetHook(ComputeForce);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (tool.IsInitialize)
        {
            if (other.tag == "HapticDevice")
            {
                HapticNativePlugin.remove_hook();
            }
        }
    }

    private Vector3 ComputeForce(Vector3 a, Vector3 b)
    {
        Debug.Log($"A: {a}; B: {b}");



        return -a * 7.5f;
    }



    private void Init()
    {
        Debug.Log("ContactObject initialize");



        tool.OnInitialize -= Init;
    }
}
