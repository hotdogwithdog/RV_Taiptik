

using UnityEditor;
using UnityEngine;

namespace UI.Menus.States
{
    internal class Credits : AMenuState
    {
        public Credits() : base("Menus/Credits") { }

        protected override void OnMenuNavigation(MenuButtons option)
        {
            switch (option)
            {
                case MenuButtons.Back:
                    MenuManager.Instance.SetState(new Main());
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
                MenuManager.Instance.SetState(new Main());
            }
        }
    }
}
