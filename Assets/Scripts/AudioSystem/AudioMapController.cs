using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AudioSystem
{
    public class AudioMapController : MonoBehaviour
    {
        private BeatMap _beatMap;
        private AudioClip _clip;
        private AudioSource _source;
        private Beat _actualBeat;

        private bool _isPlaying = false;

        private Map _map;

        private float _maxOffset;

        public Action<BeatMap> onPlay;
        public Action onPause;
        public Action onUnPause;
        public Action onFinish;
        public Action<float, float> onBeatTap;
        public Action onBeatFail;

        private void Start()
        {
            _source = GetComponent<AudioSource>();

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
                    onBeatTap?.Invoke(_actualBeat.time, _source.time);
                    _actualBeat = _beatMap.GetNextLogicBeat();
                }
            }
        }

        private void Update()
        {
            if (!_isPlaying) return;
            if (_source.isPlaying)
            {
                //Debug.Log($"Time of track: {_source.time}");
                if (_source.time > _actualBeat.time + _maxOffset)   // enter here is that the player don't hit the beat at time
                {
                    if (_actualBeat.drum != Drum.None) onBeatFail?.Invoke();
                    _actualBeat = _beatMap.GetNextLogicBeat();
                }
            }
            else if(_source.time == 0)
            {
                _isPlaying = false;
                onFinish?.Invoke();
            }
        }

        public float GetTime()
        {
            return _source.time;
        }

        public bool LoadMap(Map map)
        {
            _map = map;
            _beatMap = _map.GetBeatMap();
            _clip = _map.GetAudioClip();
            return _map.LoadMap();
        }

        public void Play()
        {
            _maxOffset = _beatMap.GetOptions().maxOffset;
            onPlay?.Invoke(_beatMap);
            _actualBeat = _beatMap.GetNextLogicBeat();
            _source.clip = _clip;
            _source.Play();
            _isPlaying = true;
        }

        public void Stop()
        {
            _source.Pause();
            onPause?.Invoke();
        }

        public void Restart()
        {
            _source.Stop();
            _beatMap.Restart();
            // Search all of the notes that are actually going to the drums and destroy its 
            // Sinceramente no se porque este foreach no explota por infraccion de acceso probe de broma y funciono sin esperarse xd
            foreach(GameObject note in GameObject.FindGameObjectsWithTag("Beat"))
            {
                GameObject.Destroy(note);
            }
            this.Play();
        }

        public void Resume()
        {
            _source.UnPause();
            onUnPause?.Invoke();
        }
    }
}

