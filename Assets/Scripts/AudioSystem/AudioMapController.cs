using System;
using System.Collections;
using System.Collections.Generic;
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

        public Action<BeatMap> OnPlay;

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
            if (!_source.isPlaying) return;

            if (_actualBeat.drum == drum)
            {
                if (_source.time <= _actualBeat.time + _maxOffset && _source.time >= _actualBeat.time - _maxOffset)
                {
                    Debug.Log($"Tapped correctly: {_actualBeat.ToString()} at {_source.time}");
                    // TODO: Generate the points
                    _actualBeat = _beatMap.GetNextLogicBeat();
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
                    // TODO: Generate fail 
                    Debug.Log($"Beat do not hitted by the player: {_actualBeat.ToString()}; {_source.time}");
                    _actualBeat = _beatMap.GetNextLogicBeat();
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Play();
            }
        }

        public float GetTime()
        {
            return _source.time;
        }

        public void Play()
        {
            if (_beatMap.LoadFile())
            {
                OnPlay?.Invoke(_beatMap);
                _actualBeat = _beatMap.GetNextLogicBeat();
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

