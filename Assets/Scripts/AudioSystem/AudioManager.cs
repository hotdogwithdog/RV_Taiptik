using UnityEngine;
using UnityEngine.Audio;
using UI.Audio;

namespace AudioSystem
{
    public class AudioManager : Utilities.Singleton<AudioManager>
    {
        public AudioMixer audioControl;

        private float _masterVol = 0.5f;
        private float _musicVol = 0.5f;
        private float _SFXVol = 0.5f;

        public float MasterVol { get { return _masterVol; } set { _masterVol = value; } }
        public float MusicVol { get { return _musicVol; } set { _musicVol = value; } }
        public float SFXVol { get { return _SFXVol; } set { _SFXVol = value; } }

        private void Start()
        {
            audioControl.SetFloat("Master", ConvertSliderdValue(_masterVol));
            audioControl.SetFloat("Music", ConvertSliderdValue(_musicVol));
            audioControl.SetFloat("SFX", ConvertSliderdValue(_SFXVol));
        }

        public void SetVolume(AudioChannel channel, float value)
        {
            switch(channel)
            {
                case AudioChannel.Master:
                    audioControl.SetFloat("Master", ConvertSliderdValue(value));
                    _masterVol = value;
                    break;
                case AudioChannel.Music:
                    audioControl.SetFloat("Music", ConvertSliderdValue(value));
                    _musicVol = value;
                    break;
                case AudioChannel.SFX:
                    audioControl.SetFloat("SFX", ConvertSliderdValue(value));
                    _SFXVol = value;
                    break;
                default:
                    Debug.Log("ERROR IN SET VOLUME UNKOWN AUDIO CHANNEL: " + channel);
                    return;
            }
        }

        public float GetVolumeByChannel(AudioChannel channel)
        {
            switch(channel)
            {
                case AudioChannel.Master:
                    return MasterVol;
                case AudioChannel.Music:
                    return MusicVol;
                case AudioChannel.SFX:
                    return SFXVol;
                default:
                    Debug.LogError($"ERROR_UNKNOWN_CHANNEL: {channel}");
                    return 0;
            }
        }


        private float ConvertSliderdValue(float value)
        {
            return Mathf.Log10(value) * 20;
        }

    }
}
