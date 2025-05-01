using System;
using UnityEngine;

namespace UI.Menus.Navigation
{
    internal class MenuOptionGroup : MonoBehaviour
    {
        public Action<MenuButtons> onMenuNavigation;

        private void Start()
        {
            foreach (MenuOption option in GetComponentsInChildren<MenuOption>())
            {
                option.onOptionClick += OnOptionClick;
            }
        }

        private void OnOptionClick(MenuButtons option)
        {
            onMenuNavigation?.Invoke(option);
        }
    }
}
