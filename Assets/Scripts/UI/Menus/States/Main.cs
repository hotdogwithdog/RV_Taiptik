

using UnityEditor;
using UnityEngine;

namespace UI.Menus.States
{
    internal class Main : AState
    {
        public Main() : base("Menus/MainMenu") { }

        protected override void OnMenuNavigation(MenuButtons option)
        {
            switch (option)
            {
                case MenuButtons.MapSelector:
                    Debug.Log("Pulsado el Map selector");
                    break;
                case MenuButtons.Options:
                    Debug.Log("Pulsado el options");
                    break;
                case MenuButtons.Credits:
                    Debug.Log("Pulsado el credits");
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
