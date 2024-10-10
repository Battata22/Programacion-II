    using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MasterNivel1 : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject canvas;
    [SerializeField] VideoPlayer videoPlayer;
    public GameObject gb;
    float waitVideo;

    private void Awake()
    {
        GameManager.Instance.Master1 = this;
    }

    void Start()
    {

        Time.timeScale = 1;

        CargarVolumen();
    }


    void Update()
    {

    }

    public void ActivarGB()
    {
        Invoke("GB", 1.46f);
        GameManager.Instance.AnimPuerta.SetTrigger("GB_Arrives");
        GameManager.Instance.CamGBCanvas.PrendidoRAW();
    }

    void GB()
    {
        gb.SetActive(true);
    }

    void CargarVolumen()
    {
        string json = File.ReadAllText(Application.dataPath + "/VolumenDataFile.json");
        VolumenData data = JsonUtility.FromJson<VolumenData>(json);
        audioMixer.SetFloat("Master", Mathf.Log10(data.Master) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(data.SFX) * 20);
        audioMixer.SetFloat("NPCs", Mathf.Log10(data.NPC) * 20);
        audioMixer.SetFloat("MusicSFX", Mathf.Log10(data.MusicSFX) * 20);
    }
}
