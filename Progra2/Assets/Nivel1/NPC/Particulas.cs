using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Particulas : MonoBehaviour
{
    [SerializeField] Sprite exclamation, question;
    [SerializeField] Transform _lookingAt;
    NPC _npcScript;
    SpriteRenderer _spriteRenderer;

    public float wait;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _npcScript = GetComponentInParent<NPC>();
        _spriteRenderer.sprite = null;
        //_lookingAt = GameManager.Instance.Camera.transform;
    }
    void Update()
    {
        if(_lookingAt == null) _lookingAt = GameManager.Instance.Camera.transform;
        
        wait += Time.deltaTime;

        transform.LookAt(_lookingAt.position);

        if(_npcScript._scared == true)
        {
            wait = 0;
            _spriteRenderer.sprite = exclamation;
        }

        if (_npcScript._doubt == true)
        {
            wait = 0;
            _spriteRenderer.sprite = question;
        }

        if(wait >= 0.2f)
        {
            _spriteRenderer.sprite = null;
        }
    }
}
