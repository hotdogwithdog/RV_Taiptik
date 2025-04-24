using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;


namespace AudioSystem
{
    public class AudioMapController : MonoBehaviour
    {
        private BeatMap _beatMap;
        [SerializeField]
        private string _path;
        private AudioSource _source;
        [SerializeField]
        private AudioClip _clip;
        private Beat _actualBeat;

        [SerializeField]
        private float _maxOffset;

        private void Start()
        {
            _source = GetComponent<AudioSource>();
            _beatMap = new BeatMap(_path);

            foreach (DrumObject drum in GetComponentsInChildren<DrumObject>())
            {
                drum.OnTap += Tapped;
            }
        }

        private void Tapped(Drum drum)
        {
            Debug.Log("Reached tapped method in AudioMapController");
            if (!_source.isPlaying) return;

            if (_actualBeat.drum == drum)
            {
                if (_source.time <= _actualBeat.time + _maxOffset && _source.time >= _actualBeat.time - _maxOffset)
                {
                    // Generate the points
                    Debug.Log("Tapped correctly");
                    // Change the current Beat
                    _actualBeat = _beatMap.GetNext();
                }
            }
        }

        private void Update()
        {
            if (_source.isPlaying)
            {
                //Debug.Log($"Time of track: {_source.time}");
                if (_source.time > _actualBeat.time + _maxOffset)   // enter here is that the player don't hit the beat at time
                {
                    // Generate fail 
                    Debug.Log($"Beat do not hitted by the player: {_actualBeat.ToString()}; {_source.time}");
                    // Change the current Beat
                    _actualBeat = _beatMap.GetNext();
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Play();
            }
        }

        public void Play()
        {
            if (_beatMap.LoadFile())
            {
                _actualBeat = _beatMap.GetNext();
                _source.clip = _clip;
                _source.Play();
            }
        }

        public void ChangePath(string newPath)
        {
            _path = newPath;
        }
    }
}

