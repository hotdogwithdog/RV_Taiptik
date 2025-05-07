

using AudioSystem;
using UnityEditor;
using UnityEngine;

namespace UI.Menus.States
{
    internal class Pause : AMenuState
    {
        private MapSelector _mapSelector;
        private AudioMapController _mapController;
        public Pause(MapSelector mapSelector) : base("Menus/Pause")
        {
            _mapSelector = mapSelector;
        }

        public override void Enter()
        {
            base.Enter();
            _mapController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioMapController>();
            _mapController.Stop();
            Time.timeScale = 0;
        }

        protected override void OnMenuNavigation(MenuButtons option)
        {
            switch (option)
            {
                case MenuButtons.Resume:
                    Resume();
                    break;
                case MenuButtons.Options:
                    MenuManager.Instance.SetState(new Options(_mapSelector));
                    break;
                case MenuButtons.Restart:
                    // TODO: Restart level
                    _mapController.Restart();
                    Resume();
                    break;
                case MenuButtons.Exit:
                    Time.timeScale = 1;
                    MenuManager.Instance.SetState(_mapSelector);
                    break;
                default:
                    Debug.LogError($"ERROR_UNKOWN_OPTION: {option}");
                    return;
            }
        }

        private void Resume()
        {
            _mapController.Resume();
            Time.timeScale = 1;
            MenuManager.Instance.SetState(new Gameplay(_mapSelector));
        }

        public override void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Resume();
            }
        }
    }
}
