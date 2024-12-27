using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CatalizadorVideos : MonoBehaviour
{
    [SerializeField] VideoClip[] videoClipsSerialize;
    public static VideoClip[] videoClips;

    private void Awake()
    {
        videoClips = videoClipsSerialize;
    }

}
