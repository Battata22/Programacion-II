using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CopyAlpha : MonoBehaviour
{
    RawImage _target;
    [SerializeField] TextMeshProUGUI _color;
    [SerializeField] RawImage _image;
    
    private void Awake()
    {
        _target = GetComponent<RawImage>();
    }
    private void LateUpdate()
    {
        if(_image.color != _target.color)
            _image.color = _target.color;
        
        if (_color.color != _target.color)
            _color.color = _target.color;
        
    }
}
