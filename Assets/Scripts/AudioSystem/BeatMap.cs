using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AudioSystem
{
    public enum Drum
    {
        None = -1,
        UL = 0,
        U = 1,
        UR = 2,
        L = 3,
        R = 4,
        DL = 5,
        D = 6,
        DR = 7
    }

    internal struct Beat
    {
        internal Drum drum;
        internal float time;

        internal Beat(Drum drum, float time)
        {
            this.drum = drum;
            this.time = time;
        }
        public override string ToString()
        {
            return $"t--{drum}--{time}";
        }
    }

    public struct MapOptions
    {
        public float maxOffset;
        public float okeyOffset;
        public float perfectOffset;
        public float timeToReach;

        public MapOptions(float max, float okey = 0.5f, float perfect = 0.25f, float timeToReach = 2.0f)
        {
            this.maxOffset = max;
            this.okeyOffset = okey;
            this.perfectOffset = perfect;
            this.timeToReach = timeToReach;
        }

        public MapOptions(string max, string okey, string perfect, string timeToReach)
        {
            this.maxOffset = float.Parse(max);
            this.okeyOffset = float.Parse(okey);
            this.perfectOffset = float.Parse(perfect);
            this.timeToReach = float.Parse(timeToReach);
        }

        public override string ToString()
        {
            return $"Max: {maxOffset}; Okey: {okeyOffset}; Perfect: {perfectOffset}";
        }
    }


    public class BeatMap
    {
        private string _path;
        private List<Beat> _beats;
        private int _iteratorLogic;
        private int _iteratorVisual;
        private MapOptions _options;

        internal BeatMap(string path)
        {
            _path = path;
            _iteratorLogic = 0;
            _iteratorVisual = 0;
            _beats = new List<Beat>();
        }

        internal bool LoadFile()
        {
            StreamReader reader = new StreamReader(_path);
            _beats.Clear();
            _iteratorLogic = 0;

            if (reader.Peek() == 'o')
            {
                string[] options = reader.ReadLine().Split("--");
                _options = new MapOptions(options[1], options[2], options[3], options[4]);
                UnityEngine.Debug.Log($"Custom settings: {_options.ToString()}");
            }
            else
            {
                _options = new MapOptions(1.0f);
                UnityEngine.Debug.Log($"Default settings: {_options.ToString()}");
            }

            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split("--");

                if (line.Length > 0)
                {
                    if (line[0] == "t")
                    {
                        _beats.Add(new Beat(ParseToDrum(line[1]), float.Parse(line[2])));
                    }
                }
            }
            return true;
        }

        internal Beat GetNextLogicBeat()
        {
            if (_iteratorLogic < _beats.Count)
            {
                return _beats[_iteratorLogic++];
            }
            else
            {
                return new Beat(Drum.None, -1f);
            }
        }

        internal Beat GetNextVisualBeat()
        {
            if (_iteratorVisual < _beats.Count)
            {
                return _beats[_iteratorVisual++];
            }
            else
            {
                return new Beat(Drum.None, -1f);
            }
        }

        public MapOptions GetOptions() { return _options; }

        internal void Restart()
        {
            _iteratorLogic = 0;
            _iteratorVisual = 0;
        }

        internal void WriteFile(string path)
        {
            using (StreamWriter writer = File.CreateText(path + ".maprv"))
            {
                // Start in one for avoid headers on the csv table file
                for (int i = 1; i < _beats.Count; ++i)
                {
                    writer.WriteLine(_beats[i].ToString());
                }
                writer.Close();
            }
        }

        private Drum ParseToDrum(string data)
        {
            return (Drum)Enum.Parse(typeof(Drum), data);
        }
    }
}
