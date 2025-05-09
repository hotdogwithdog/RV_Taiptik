using AudioSystem;
using System;
using System.Collections;
using UnityEngine;


namespace HapticInteraction
{
    public class ChangeColor : MonoBehaviour
    {
        private DrumObject _drumObject;
        private Material _material;
        [SerializeField]
        private Color _tappedColor;
        private Color _edgeColor;

        [SerializeField]
        private float _tapTime = 0.5f;

        private string _edgeColorName = "EdgeColor";

        private void Start()
        {
            _material = GetComponent<MeshRenderer>().material;
            _edgeColor = _material.GetColor(_edgeColorName);
            _drumObject = GetComponent<DrumObject>();
            _drumObject.OnTap += Tapped;
        }

        private void Tapped(Drum drum)
        {
            StartCoroutine(InterpolateColor());
        }

        private IEnumerator InterpolateColor()
        {
            float elapsedTime = 0;

            while (elapsedTime < _tapTime)
            {
                _material.SetColor(_edgeColorName, Color.Lerp(_tappedColor, _edgeColor, elapsedTime / _tapTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _material.SetColor(_edgeColorName, _edgeColor);
        }

        private void OnDestroy()
        {
            _drumObject.OnTap -= Tapped;
        }
    }
}

