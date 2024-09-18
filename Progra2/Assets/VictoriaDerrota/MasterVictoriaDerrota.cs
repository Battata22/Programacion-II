using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MasterVictoriaDerrota : MonoBehaviour
{
    [SerializeField] VideoClip _clip1, _clip2;
    [SerializeField] VideoPlayer _player;
    [SerializeField] bool derrota;

    private void Start()
    {
        if (derrota)
        {
            int random;
            random = Random.Range(1, 100 + 1);
            if (random <= 99)
            {
                _player.clip = _clip2;
                _player.Play();
            }
            else
            {
                _player.clip = _clip1;
                _player.Play();
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
