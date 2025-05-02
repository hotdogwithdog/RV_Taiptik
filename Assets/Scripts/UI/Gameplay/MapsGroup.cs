using UnityEngine;
using System.Collections.Generic;
using AudioSystem;
using System.IO;
using UI.Menus;
using UI.Menus.States;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UI;


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

        [SerializeField]
        private RectTransform _minTransform;
        [SerializeField]
        private RectTransform _maxTransform;

        [SerializeField]
        private Color _unselectedColor;
        [SerializeField]
        private Color _selectedColor;

        private float _minHeight;
        private float _maxHeight;
        private RectTransform _mapsTransform;

        private void Start()
        {
            _mapsTransform = GetComponent<RectTransform>();
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
                _mapPanels[_mapPanels.Count - 1].GetComponent<RawImage>().color = _unselectedColor;

                _mapPanels[_mapPanels.Count - 1].GetComponent<MapPanel>().SetIndex(_mapPanels.Count - 1);
                _mapPanels[_mapPanels.Count - 1].GetComponent<MapPanel>().onClick += OnMapClicked;
            }
            _minHeight = _minTransform.position.y;
            _maxHeight = _maxTransform.position.y + (_maps.Count - 1) * (GetComponent<VerticalLayoutGroup>().spacing + _mapPanelPrefab.GetComponent<RectTransform>().rect.height);
            Debug.Log($"Min height: {_minHeight} : {_maxHeight}");

            _mapIndex = Random.Range(0, _maps.Count);
            _mapPanels[_mapIndex].GetComponent<RawImage>().color = _selectedColor;
            _mapsTransform.position = new Vector3(_mapsTransform.position.x, Mathf.Clamp(_minHeight * _mapIndex, _minHeight, _maxHeight), _mapsTransform.position.z);
            _backGround = _maps[_mapIndex].GetBackground();
            Debug.Log($"Added {_maps.Count} maps; now the index is in {_mapIndex}");
        }

        private void OnMapClicked(int index)
        {
            if (index < _maps.Count)
            {
                _mapPanels[_mapIndex].GetComponent<RawImage>().color = _unselectedColor;
                _mapIndex = index;
                _mapPanels[_mapIndex].GetComponent<RawImage>().color = _selectedColor;
            }
        }

        private void Update()
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                // Move the list up and down
                MovePanels(Input.mouseScrollDelta.y * 32);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                // Move up
                MovePanels(-32);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                // Move Down
                MovePanels(32);
            }
            else if (Input.GetKeyDown(KeyCode.Return))  // The Enter key xd
            {
                // Play the selected song
                Debug.Log($"Playing the song of index: {_mapIndex}");
                Play();
            }

        }

        private void MovePanels(float movement)
        {
            Debug.Log($"position: {_mapsTransform.position}");
            float posToMove = _mapsTransform.position.y + movement;
            if (_minHeight <= posToMove && _maxHeight >= posToMove)
            {
                _mapsTransform.position = new Vector3(_mapsTransform.position.x, posToMove, _mapsTransform.position.z);
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