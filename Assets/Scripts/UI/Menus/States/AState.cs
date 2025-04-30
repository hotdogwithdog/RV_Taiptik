
using UI.Menus.Navigation;
using UnityEngine;

namespace UI.Menus.States
{
    internal abstract class AState : IState
    {
        private string _menuName;
        private GameObject _menu;
        private Canvas _canvas;

        public AState(string menuName)
        {
            _menuName = menuName;
        }

        public void Enter()
        {
            _canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
            GameObject menuPrefab = (GameObject)Resources.Load(_menuName);
            _menu = GameObject.Instantiate(menuPrefab, _canvas.transform);

            _menu.GetComponentInChildren<MenuOptionGroup>().onMenuNavigation += OnMenuNavigation;
        }

        protected abstract void OnMenuNavigation(MenuButtons option);

        public void Exit()
        {
            _menu.GetComponentInChildren<MenuOptionGroup>().onMenuNavigation -= OnMenuNavigation;
            GameObject.Destroy(_menu);
        }

        public abstract void Update(float deltaTime);
    }
}
