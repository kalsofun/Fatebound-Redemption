using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public enum VolumeType { Master, BGM, SFX }

    [SerializeField] private VolumeType volumeType;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        if (slider != null && AudioManager.Instance != null)
        {
            // Set initial slider value based on current volume
            switch (volumeType)
            {
                case VolumeType.Master:
                    slider.value = AudioManager.Instance.MasterVol;
                    break;
                case VolumeType.BGM:
                    slider.value = AudioManager.Instance.BGMVol;
                    break;
                case VolumeType.SFX:
                    slider.value = AudioManager.Instance.SFXVol;
                    break;
            }

            // Add listener for value changes
            slider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }

    private void OnVolumeChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            switch (volumeType)
            {
                case VolumeType.Master:
                    AudioManager.Instance.SetMasterVolume(value);
                    break;
                case VolumeType.BGM:
                    AudioManager.Instance.SetBGMVolume(value);
                    break;
                case VolumeType.SFX:
                    AudioManager.Instance.SetSFXVolume(value);
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        if (slider != null)
        {
            slider.onValueChanged.RemoveListener(OnVolumeChanged);
        }
    }
}
