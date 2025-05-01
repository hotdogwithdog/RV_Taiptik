

using UnityEditor;
using UnityEngine;

namespace UI.Menus.States
{
    internal class Pause : AMenuState
    {
        private MapSelector _mapSelector;
        public Pause(MapSelector mapSelector) : base("Menus/Pause")
        {
            _mapSelector = mapSelector;
        }

        protected override void OnMenuNavigation(MenuButtons option)
        {
            switch (option)
            {
                case MenuButtons.Resume:
                    MenuManager.Instance.SetState(new Gameplay(_mapSelector));
                    break;
                case MenuButtons.Options:
                    MenuManager.Instance.SetState(new Options(_mapSelector));
                    break;
                case MenuButtons.Restart:
                    // TODO: Restart level
                    Debug.Log("Restart pressed");
                    break;
                case MenuButtons.Exit:
                    MenuManager.Instance.SetState(_mapSelector);
                    break;
                default:
                    Debug.LogError($"ERROR_UNKOWN_OPTION: {option}");
                    return;
            }
        }
        public override void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuManager.Instance.SetState(new Gameplay(_mapSelector));
            }
        }
    }
}
