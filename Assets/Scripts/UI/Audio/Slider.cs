using AudioSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Audio
{
    public class Slider : MonoBehaviour, IDragHandler
    {
        public delegate void OnSliderEvent(AudioChannel channel, float value);
        public OnSliderEvent onSliderEvent;

        private UnityEngine.UI.Slider _slider;
        public float SldValue { get { return _slider.value; } set { _slider.value = value; } }

        [SerializeField] private AudioChannel _channel;

        public void Awake()
        {
            _slider = gameObject.GetComponent<UnityEngine.UI.Slider>();
            _slider.value = AudioManager.Instance.GetVolumeByChannel( _channel );
        }

        public void OnDrag(PointerEventData eventData)
        {
            onSliderEvent?.Invoke(_channel, _slider.value);
        }

        public AudioChannel GetChannel()
        {
            return _channel;
        }
    }
}
