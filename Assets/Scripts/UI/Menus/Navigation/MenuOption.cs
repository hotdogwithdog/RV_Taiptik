using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Menus.Navigation
{
    internal class MenuOption : MonoBehaviour, IPointerClickHandler
    {
        public Action<MenuButtons> onOptionClick;

        [SerializeField]
        private MenuButtons _option;

        public void OnPointerClick(PointerEventData eventData)
        {
            onOptionClick?.Invoke(_option);
        }
    }
}
