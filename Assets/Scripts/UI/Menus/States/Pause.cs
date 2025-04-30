

using UnityEditor;
using UnityEngine;

namespace UI.Menus.States
{
    internal class Pause : AMenuState
    {
        public Pause() : base("Menus/Pause") { }

        protected override void OnMenuNavigation(MenuButtons option)
        {
            switch (option)
            {
                case MenuButtons.Resume:
                    MenuManager.Instance.SetState(new Gameplay());
                    break;
                case MenuButtons.Options:
                    MenuManager.Instance.SetState(new Options(false));
                    break;
                case MenuButtons.Restart:
                    // TODO: Restart level
                    Debug.Log("Restart pressed");
                    break;
                case MenuButtons.Exit:
                    MenuManager.Instance.SetState(new MapSelector());
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
                MenuManager.Instance.SetState(new Gameplay());
            }
        }
    }
}
