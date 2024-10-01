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
        //videoPlayer.Play();
        ////Time.timeScale = 0;
        //canvas.SetActive(false);

        Time.timeScale = 1;

        string json = File.ReadAllText(Application.dataPath + "/VolumenDataFile.json");
        VolumenData data = JsonUtility.FromJson<VolumenData>(json);
        audioMixer.SetFloat("Master", Mathf.Log10(data.Master) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(data.SFX) * 20);
        audioMixer.SetFloat("NPCs", Mathf.Log10(data.NPC) * 20);
        audioMixer.SetFloat("MusicSFX", Mathf.Log10(data.MusicSFX) * 20);
    }


    void Update()
    {
        //waitVideo += Time.deltaTime;
        //if (waitVideo >= videoPlayer.length * 2)
        //{
        //    videoPlayer.enabled = false;
        //    Time.timeScale = 1;
        //    canvas.SetActive(true);
        //}

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("Nivel1");
        }
    }

    public void ActivarGB()
    {
        gb.SetActive(true);
    }
}
