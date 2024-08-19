using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoTest : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] VideoClip videoOG, videoApple;
    bool OG = true;
    void Start()
    {
        
    }


    void Update()
    {
        if (OG)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                videoPlayer.Stop();
                videoPlayer.clip = videoApple;
                videoPlayer.Play();
                OG = false;
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                videoPlayer.Stop();
                videoPlayer.clip = videoOG;
                videoPlayer.Play();
                OG = true;
            }
        }

    }
}
