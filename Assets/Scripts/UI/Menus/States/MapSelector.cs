using UI.Menus.Navigation;
using UnityEngine;

namespace UI.Menus.States
{
    internal class MapSelector : AMenuState
    {
        public MapSelector() : base("Menus/MapSelector") { _isMain = false; }

        private bool _isMain;

        protected override void OnMenuNavigation(MenuButtons option)
        {
            switch (option)
            {
                case MenuButtons.Back:
                    GoMain();
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
                _menu.GetComponentInChildren<MenuOptionGroup>().onMenuNavigation += OnMenuNavigation;
            }
        }

        public override void Exit()
        {
            if (_isMain)
            {
                base.Exit();
            }
            else
            {
                _menu.GetComponentInChildren<MenuOptionGroup>().onMenuNavigation -= OnMenuNavigation;
                _menu.SetActive(false);
            }
        }

        private void GoMain()
        { 
            _isMain = true;
            MenuManager.Instance.SetState(new Main());
        }

        public override void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GoMain();
            }
        }
    }
}
