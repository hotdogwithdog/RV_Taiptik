using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Effects
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private TextMeshProUGUI _text;
        [SerializeField]
        private float _textSizeInHoverMultiplier = 1.5f;
        private float _textSizeInNormal;
        private float _textSizeInHover;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _textSizeInNormal = _text.fontSize;
            _textSizeInHover = _textSizeInNormal * _textSizeInHoverMultiplier;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _text.fontSize = _textSizeInHover;
            _text.fontStyle = FontStyles.Bold;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _text.fontSize = _textSizeInNormal;
            _text.fontStyle = FontStyles.Normal;
        }
    }
}
