using AudioSystem;

namespace UI.Audio
{
    public class SliderGroup : UnityEngine.MonoBehaviour
    {
        public void Start()
        {
            foreach(Slider slider in this.gameObject.GetComponentsInChildren<Slider>())
            {
                slider.onSliderEvent += OnSliderEvent;
            }
        }

        private void OnSliderEvent(AudioChannel channel, float value)
        {
            AudioManager.Instance.SetVolume(channel, value);
        }
    }
}
