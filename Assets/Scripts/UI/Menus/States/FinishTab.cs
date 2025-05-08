

using AudioSystem;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace UI.Menus.States
{
    internal class FinishTab : AMenuState
    {
        private string _runPerformance;
        private AudioMapController _mapController;

        public FinishTab(string runPerformance) : base("Menus/FinishTab")
        { 
            _runPerformance = runPerformance;
        }

        public override void Enter()
        {
            base.Enter();

            _mapController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioMapController>();

            foreach (TextMeshProUGUI text in _menu.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (text.gameObject.tag == "FinishTabText") text.text = _runPerformance;
            }
        }

        protected override void OnMenuNavigation(MenuButtons option)
        {
            switch (option)
            {
                case MenuButtons.Back:
                    Back();
                    break;
                case MenuButtons.Restart:
                    _mapController.Restart();
                    MenuManager.Instance.SetState(new Gameplay(new MapSelector()));
                    break;
                default:
                    Debug.LogError($"ERROR_UNKOWN_OPTION: {option}");
                    return;
            }
        }

        private void Back()
        {
            MenuManager.Instance.SetState(new MapSelector());
        }
        public override void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Back();
            }
        }
    }
}
