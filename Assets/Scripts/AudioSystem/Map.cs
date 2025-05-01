using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace AudioSystem
{
    public class Map
    {
        private string _folderPath;
        private BeatMap _beatMap;
        private AudioClip _audioClip;
        private UnityEngine.UI.Image _background;
        
        public string MapName { get; private set; }

        public Map(string foldePath)
        {
            _folderPath = foldePath;
            string[] paths = Directory.GetFiles(_folderPath);

            foreach (string path in paths)
            {
                if (path.EndsWith(".maprv"))
                {
                    _beatMap = new BeatMap(path);
                    int start = path.LastIndexOf('\\') + 1;
                    int end = path.LastIndexOf('.');
                    MapName = path.Substring(start, end - start);

                    Debug.Log($"Loading the {path} file : {MapName}");
                }
                else if (path.EndsWith(".wav") || path.EndsWith(".mp3") || path.EndsWith(".ogg") || path.EndsWith(".aif"))
                {
                    _audioClip = Resources.Load<AudioClip>(GetResourceString(path));

                    Debug.Log($"Loading the {path} file : {_audioClip}");
                }
                else if (path.EndsWith(".png") || path.EndsWith(".jpg"))
                {
                    _background = Resources.Load<UnityEngine.UI.Image>(GetResourceString(path));

                    Debug.Log($"Loading the {path} file : {_background}");
                }
            }
        }
        private string GetResourceString(string absolutePath)
        {
            int start = absolutePath.LastIndexOf('/') + 1;
            int end = absolutePath.LastIndexOf('.');
            return absolutePath.Substring(start, end - start);
        }

        public bool LoadMap()
        {
            return (_audioClip.LoadAudioData() && _beatMap.LoadFile());
        }

        public BeatMap GetBeatMap() {  return _beatMap; }
        public UnityEngine.UI.Image GetBackground() { return _background; }
        public AudioClip GetAudioClip() { return _audioClip; }
    }
}
