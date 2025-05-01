

using UnityEditor;
using UnityEngine;

namespace UI.Menus.States
{
    internal class Main : AMenuState
    {
        public Main() : base("Menus/MainMenu") { }

        protected override void OnMenuNavigation(MenuButtons option)
        {
            switch (option)
            {
                case MenuButtons.MapSelector:
                    MenuManager.Instance.SetState(new MapSelector());
                    break;
                case MenuButtons.Options:
                    MenuManager.Instance.SetState(new Options());
                    break;
                case MenuButtons.Credits:
                    MenuManager.Instance.SetState(new Credits());
                    break;
                case MenuButtons.Exit:
#if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                    break;
                default:
                    Debug.LogError($"ERROR_UNKOWN_OPTION: {option}");
                    return;
            }
        }
        public override void Update(float deltaTime) { }
    }
}
