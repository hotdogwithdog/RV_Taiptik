

using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace UI.Menus.States
{
    internal class Options : AMenuState
    {
        private MapSelector _mapSelector;

        public Options(MapSelector mapSelector = null) : base("Menus/Options") 
        {
            _mapSelector = mapSelector;
        }

        protected override void OnMenuNavigation(MenuButtons option)
        {
            switch (option)
            {
                case MenuButtons.Back:
                    Debug.Log("Back pressed");
                    Back();
                    break;
                default:
                    Debug.LogError($"ERROR_UNKOWN_OPTION: {option}");
                    return;
            }
        }

        private void Back()
        {
            if (_mapSelector == null)
            {
                MenuManager.Instance.SetState(new Main());
            }
            else
            {
                MenuManager.Instance.SetState(new Pause(_mapSelector));
            }
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
