using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] Slider _masterSlider, _sFXSlider, _nPCSlider, _musicSFXSlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("MasterVolume") || PlayerPrefs.HasKey("SFXVolume") || PlayerPrefs.HasKey("NPCsVolume") || PlayerPrefs.HasKey("MusicSFXVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volumeMaster = _masterSlider.value;
        float volumeSFX = _sFXSlider.value;
        float volumeNPC = _nPCSlider.value;
        float volumeMusicSFX = _musicSFXSlider.value;

        _audioMixer.SetFloat("Master", Mathf.Log10(volumeMaster) * 20);
        _audioMixer.SetFloat("SFX", Mathf.Log10(volumeSFX) * 20);
        _audioMixer.SetFloat("NPCs", Mathf.Log10(volumeNPC) * 20);
        _audioMixer.SetFloat("MusicSFX", Mathf.Log10(volumeMusicSFX) * 20);

        PlayerPrefs.SetFloat("MasterVolume", volumeMaster);
        PlayerPrefs.SetFloat("SFXVolume", volumeSFX);
        PlayerPrefs.SetFloat("NPCsVolume", volumeNPC);
        PlayerPrefs.SetFloat("MusicSFXVolume", volumeMusicSFX);
    }

    void LoadVolume()
    {
        _masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        _sFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        _nPCSlider.value = PlayerPrefs.GetFloat("NPCsVolume");
        _musicSFXSlider.value = PlayerPrefs.GetFloat("MusicSFXVolume");

        SetMusicVolume();
    }
}
