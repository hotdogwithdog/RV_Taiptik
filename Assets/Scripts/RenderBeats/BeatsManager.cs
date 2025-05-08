using AudioSystem;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace RenderBeats
{
    public class BeatsManager : MonoBehaviour
    {
        [SerializeField]
        private float _timeToReach = 2f;

        private const int N_DRUMS = 8;
        private string _prefabPath = "Prefabs/Beat";
        private bool _isPlaying;
        private AudioMapController _audioMapper;
        private BeatMap _beatMap;
        private Vector3[] _drumPositions;
        private AudioSystem.Beat _actualBeat;

        private void Start()
        {
            _audioMapper = GetComponent<AudioMapController>();
            InitializeDrumsPosition();

            _audioMapper.onPlay += Play;
        }

        private void InitializeDrumsPosition()
        {
            _drumPositions = new Vector3[N_DRUMS];

            foreach (DrumObject drum in GetComponentsInChildren<DrumObject>())
            {   
                _drumPositions[(int)drum.GetDrum()] = drum.posToGo;
            }
        }

        private void Update()
        {
            if (_isPlaying)
            {
                if (_audioMapper.GetTime() >= _actualBeat.time - _timeToReach)   // enter here is that the player don't hit the beat at time
                {
                    GenerateBeat(_actualBeat);
                    _actualBeat = _beatMap.GetNextVisualBeat();
                    if (_actualBeat.drum == Drum.None)
                    {
                        _isPlaying = false;
                    }
                }
            }
        }

        private void Play(BeatMap beatMap)
        {
            _beatMap = beatMap;
            _isPlaying = true;
            _actualBeat = _beatMap.GetNextVisualBeat();
        }

        private void GenerateBeat(AudioSystem.Beat beat)
        {
            GameObject visualBeat = (GameObject)Resources.Load(_prefabPath);
            GameObject.Instantiate(visualBeat).GetComponent<RenderBeats.Beat>().GoTarget(transform.position,
                GetDrumPosition(beat.drum), beat.time - _audioMapper.GetTime());
        }

        private Vector3 GetDrumPosition(Drum drum)
        {
            return _drumPositions[(int)drum];
        }

        private void OnDestroy()
        {
            _audioMapper.onPlay -= Play;
        }
    }

}
