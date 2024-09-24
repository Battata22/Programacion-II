using System.IO;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SlidersSettings : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] Slider _masterSlider, _sFXSlider, _nPCSlider, _musicSFXSlider, _sensSlider;
    [SerializeField] Text _textMaster, _textSFX, _textNPC, _textMusicSFX, _textSens;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioMixerGroup groupMaster, groupSFX, groupNPC, groupMusic;
    float waitSonido, menuTimer;
    bool sono = true;
    int output = 1; // 1 = master, 2 = SFX, 3 = NPCs, 4 = MusicSFX


    private void Start()
    {
        #region Comment
        //if(PlayerPrefs.HasKey("MasterVolume") || PlayerPrefs.HasKey("SFXVolume") || PlayerPrefs.HasKey("NPCsVolume") || PlayerPrefs.HasKey("MusicSFXVolume"))
        //{
        //    //LoadVolume();
        //    VolCargarJSON();
        //}
        //else
        //{
        //    SetMusicVolume();
        //}
        #endregion

        VolCargarJSON();
        SensCargarJSON();
    }

    private void Update()
    {
        waitSonido += Time.deltaTime;
        menuTimer += Time.deltaTime;

        if (waitSonido >= 0.1f && menuTimer >= 0.2f && sono == false)
        {
            sono = true;
            _audioSource.Play();
        }

        //podria estar mejor? si, pero funciona.
        if (menuTimer <= 0.3)
        {
            _audioSource.volume = 0;
        }
        else
        {
            _audioSource.volume = 1;
        }
    }

    public void MasterOutput()
    {
        _audioSource.outputAudioMixerGroup = groupMaster;
    }

    public void SFXOutput()
    {
        _audioSource.outputAudioMixerGroup = groupSFX;
    }

    public void NPCsOutput()
    {
        _audioSource.outputAudioMixerGroup = groupNPC;
    }

    public void MusicSFXOutput()
    {
        _audioSource.outputAudioMixerGroup = groupMusic;
    }
    public void SensOutput()
    {
        _audioSource.outputAudioMixerGroup = groupMaster;
    }


    public void SetVolume()
    {
        float volumeMaster = _masterSlider.value;
        float volumeSFX = _sFXSlider.value;
        float volumeNPC = _nPCSlider.value;
        float volumeMusicSFX = _musicSFXSlider.value;

        _textMaster.text = ((volumeMaster * 100).ToString("0") + "%");
        _textSFX.text = ((volumeSFX * 100).ToString("0") + "%");
        _textNPC.text = ((volumeNPC * 100).ToString("0") + "%");
        _textMusicSFX.text = ((volumeMusicSFX * 100).ToString("0") + "%");

        _audioMixer.SetFloat("Master", Mathf.Log10(volumeMaster) * 20);
        _audioMixer.SetFloat("SFX", Mathf.Log10(volumeSFX) * 20);
        _audioMixer.SetFloat("NPCs", Mathf.Log10(volumeNPC) * 20);
        _audioMixer.SetFloat("MusicSFX", Mathf.Log10(volumeMusicSFX) * 20);

        waitSonido = 0;
        sono = false;

        #region Comment
        //PlayerPrefs.SetFloat("MasterVolume", volumeMaster);
        //PlayerPrefs.SetFloat("SFXVolume", volumeSFX);
        //PlayerPrefs.SetFloat("NPCsVolume", volumeNPC);
        //PlayerPrefs.SetFloat("MusicSFXVolume", volumeMusicSFX);

        //PlayerPrefs.Save();
        #endregion

        VolGuardarJSON();
    }

    public void SetSens()
    {
        _textSens.text = ((_sensSlider.value).ToString("0") + "%");

        waitSonido = 0;
        sono = false;

        SensGuardarJSON(_sensSlider.value);
    }

    //void LoadVolume()
    //{
    //    _masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
    //    _sFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    //    _nPCSlider.value = PlayerPrefs.GetFloat("NPCsVolume");
    //    _musicSFXSlider.value = PlayerPrefs.GetFloat("MusicSFXVolume");

    //    SetMusicVolume();
    //}

    public void VolGuardarJSON()
    {
        VolumenData data = new VolumenData();
        data.Master = _masterSlider.value;
        data.SFX = _sFXSlider.value;
        data.NPC = _nPCSlider.value;
        data.MusicSFX = _musicSFXSlider.value;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/VolumenDataFile.json", json);
    }

    public void VolCargarJSON()
    {
        string json = File.ReadAllText(Application.dataPath + "/VolumenDataFile.json");
        VolumenData data = JsonUtility.FromJson<VolumenData>(json);

        _masterSlider.value = data.Master;
        _sFXSlider.value = data.SFX;
        _nPCSlider.value = data.NPC;
        _musicSFXSlider.value = data.MusicSFX;
    }

    public void SensGuardarJSON(float d)
    {
        CamData camDataScript = new CamData();
        camDataScript._xSens = d;
        camDataScript._ySens = d;

        string json = JsonUtility.ToJson(camDataScript, true);
        File.WriteAllText(Application.dataPath + "/SensDataFile.json", json);
    }
    public void SensCargarJSON()
    {
        string json = File.ReadAllText(Application.dataPath + "/SensDataFile.json");
        CamData camData = JsonUtility.FromJson<CamData>(json);

        _sensSlider.value = camData._xSens;
    }
}
