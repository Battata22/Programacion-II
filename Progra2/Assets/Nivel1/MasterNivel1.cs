using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MasterNivel1 : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    void Start()
    {
        string json = File.ReadAllText(Application.dataPath + "/VolumenDataFile.json");
        VolumenData data = JsonUtility.FromJson<VolumenData>(json);
        audioMixer.SetFloat("Master", Mathf.Log10(data.Master) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(data.SFX) * 20);
        audioMixer.SetFloat("NPCs", Mathf.Log10(data.NPC) * 20);
        audioMixer.SetFloat("MusicSFX", Mathf.Log10(data.MusicSFX) * 20);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("Nivel1");
        }
    }
}
