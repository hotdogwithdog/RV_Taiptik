using HapticInteraction;
using System;
using UnityEngine;
using AudioSystem;

[RequireComponent(typeof(ForceToOnePoint))]
public class DrumObject : MonoBehaviour
{
    public Action<Drum> OnTap;
    [SerializeField]
    private Drum _drum;
    private ForceToOnePoint _actor;

    void Start()
    {
        _actor = GetComponent<ForceToOnePoint>();
        _actor.OnTouch += Tapped;
    }

    public Drum GetDrum() { return _drum; }

    private void Tapped()
    {
        OnTap?.Invoke(_drum);
    }

    private void OnDestroy()
    {
        _actor.OnTouch -= Tapped;
    }
}
