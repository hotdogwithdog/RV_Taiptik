using UnityEngine;
using System.Collections.Generic;
using AudioSystem;
using System.IO;
using UI.Menus;
using UI.Menus.States;
using Unity.VisualScripting;
using TMPro;


namespace UI.Gameplay
{
    public class MapsGroup : MonoBehaviour
    {
        private List<Map> _maps;
        private List<GameObject> _mapPanels;
        private int _mapIndex;
        private string _mapsDirectory;
        private GameObject _mapPanelPrefab;
        private UnityEngine.UI.Image _backGround;
        private AudioMapController _controller;

        private void Start()
        {
            _mapsDirectory = "Assets/Resources/Maps";
            _mapPanelPrefab = Resources.Load<GameObject>("Prefabs/MapPanel");
            _maps = new List<Map>();
            _mapPanels = new List<GameObject>();
            _controller = GameObject.FindWithTag("AudioController").GetComponent<AudioMapController>();
            Debug.Log(_controller);
            
            foreach (string mapFolder in Directory.GetDirectories(_mapsDirectory))
            {
                _maps.Add(new Map(mapFolder));
                _mapPanels.Add(GameObject.Instantiate(_mapPanelPrefab, this.transform));

                _mapPanels[_mapPanels.Count - 1].GetComponentInChildren<TextMeshProUGUI>().text = _maps[_mapPanels.Count - 1].MapName;

                _mapPanels[_mapPanels.Count - 1].GetComponent<MapPanel>().SetIndex(_mapPanels.Count - 1);
                _mapPanels[_mapPanels.Count - 1].GetComponent<MapPanel>().onClick += OnMapClicked;
            }

            _mapIndex = Random.Range(0, _maps.Count);
            _backGround = _maps[_mapIndex].GetBackground();
            Debug.Log($"Added {_maps.Count} maps; now the index is in {_mapIndex}");
        }

        private void OnMapClicked(int index)
        {
            if (index < _maps.Count) _mapIndex = index;
        }

        private void Update()
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                // Move the list up and down
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // Move up
                _mapIndex = (_mapIndex + 1) % _maps.Count;
                _backGround = _maps[_mapIndex]?.GetBackground();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // Move Down
                _mapIndex = (_mapIndex - 1) % _maps.Count;
                _backGround = _maps[_mapIndex]?.GetBackground();
            }
            else if (Input.GetKeyDown(KeyCode.Return))  // The Enter key xd
            {
                // Play the selected song
                Debug.Log($"Playing the song of index: {_mapIndex}");
                Play();
            }

        }

        private void Play()
        {
            if (_controller.LoadMap(_maps[_mapIndex]))
            {
                Debug.Log("StartPlaying");
                _controller.Play();
                MenuManager.Instance.SetState(new Menus.States.Gameplay((Menus.States.MapSelector)MenuManager.Instance.GetState()));
            }
        }

        private void OnDestroy()
        {
            foreach (GameObject panel in _mapPanels)
            {
                panel.GetComponent<MapPanel>().onClick -= OnMapClicked;
            }
        }
    }
}