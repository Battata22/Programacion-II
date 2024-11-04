using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeManager : MonoBehaviour
{

    [SerializeField] Volume _volume;
    [SerializeField] public Vignette _vignette;
    [SerializeField] public ShadowsMidtonesHighlights _shadMidHigh;
    [SerializeField] public LiftGammaGain _liftGammaGain;

    Vignette _OGvignette;
    ShadowsMidtonesHighlights _OGshadMidHigh;
    LiftGammaGain _OGliftGammaGain;



    void Start()
    {
        _volume = GetComponent<Volume>();
        _vignette = GetComponent<Vignette>();
        _shadMidHigh = GetComponent<ShadowsMidtonesHighlights>();
        _liftGammaGain = GetComponent<LiftGammaGain>();

        _OGliftGammaGain = _liftGammaGain;
        _OGshadMidHigh = _shadMidHigh;
        _OGvignette = _vignette;

        //GameManager.Instance.VolumeManager = this;             
    }    

    public void ChangeVignette(string hex, ClampedFloatParameter i)
    {
        Color color;
        ColorUtility.TryParseHtmlString(hex, out color);
        _vignette.color.value = color;
        _vignette.intensity = i;
    }
}
