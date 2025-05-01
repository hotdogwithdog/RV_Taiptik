using UI.Menus.Navigation;
using UnityEngine;

namespace UI.Menus.States
{
    internal class MapSelector : AMenuState
    {
        public MapSelector() : base("Menus/MapSelector") { }


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

        public override void Enter()
        {
            if (_menu == null)
            {
                base.Enter();
            }
            else
            {
                _menu.SetActive(true);
            }
        }

        public override void Exit()
        {
            _menu.GetComponentInChildren<MenuOptionGroup>().onMenuNavigation -= OnMenuNavigation;
            _menu.SetActive(false);
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
