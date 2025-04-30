

using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace UI.Menus.States
{
    internal class Options : AMenuState
    {
        private bool _goFromMain;

        public Options(bool goFromMain) : base("Menus/Options") 
        { 
            _goFromMain = goFromMain;
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
            if (_goFromMain)
            {
                MenuManager.Instance.SetState(new Main());
            }
            else
            {
                MenuManager.Instance.SetState(new Pause());
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
