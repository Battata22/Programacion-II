using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoHabilidad : MonoBehaviour
{
    [SerializeField] Texture2D _tornado, _shadow, _enchant, _moco, _wire;
    RawImage _myImage;

    int lastHab;

    private void Awake()
    {
        _myImage = GetComponent<RawImage>();
        //lastHab = SelectorUI.habAct;
        UpdateIcon();
    }

    private void Update()
    {
        if(SelectorUI.habAct != lastHab)
        {
            UpdateIcon();
        }
    }

    void UpdateIcon()
    {
        switch (SelectorUI.habAct)
        {
            case 1:
                _myImage.texture = _tornado;
                _myImage.color = Color.white;
                //_myImage.color += new Color(0, 0, 0, 1);
                break;
            case 2:
                _myImage.texture = _shadow;
                _myImage.color = Color.white;
                //_myImage.color += new Color(0, 0, 0, 1);

                break;
            case 3:
                _myImage.texture = _enchant;
                _myImage.color = Color.white;
                //_myImage.color += new Color(0, 0, 0, 1);
                break;
            case 4:
                _myImage.texture = _moco;
                _myImage.color = Color.white;
                //_myImage.color += new Color(0, 0, 0, 1);
                break;
            case 5:
                _myImage.texture = _wire;
                _myImage.color = Color.white;
                //_myImage.color += new Color(0, 0, 0, 1);
                break;
            default:
                _myImage.texture = null;
                _myImage.color = new Color(0, 0, 0, 0);
                break;
        }
        lastHab = SelectorUI.habAct;
    }
}
