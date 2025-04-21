using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HapticInteraction
{
    public class ForceToOnePoint : MonoBehaviour
    {
        private HapticTool tool;

        private Vector3 _forceDirection;
        private Vector3 _force;

        [Header("Properties")]
        [SerializeField]
        private Transform _center;
        [SerializeField]
        private float _forceMagnitude = 7.5f;


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

        private Vector3 ComputeForce(Vector3 actualPosition, Vector3 actualVelocity)
        {
            return _force;
        }



        private void Init()
        {
            tool.OnInitialize -= Init;
            _forceDirection = _center.position - transform.position;
            _forceDirection.Normalize();
            _force = _forceDirection * _forceMagnitude;
            Debug.Log($"Object -> ({this.name}) is Initialize for contact forces");
        }
    }
}

