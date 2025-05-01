using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Gameplay
{
    internal class MapPanel : MonoBehaviour, IPointerClickHandler
    {
        public Action<int> onClick;
        private int _index;

        public void SetIndex(int index) { _index = index; }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke(_index);
        }
    }
}
