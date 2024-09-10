using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Particulas : MonoBehaviour
{
    [SerializeField] Sprite exclamation, question;
    [SerializeField] Transform _player;
    NPC _npcScript;
    SpriteRenderer _spriteRenderer;
    Vector3 dir;
    public float wait;

    private void Start()
    {
       _spriteRenderer = GetComponent<SpriteRenderer>();
       _npcScript = GetComponentInParent<NPC>();
       _spriteRenderer.sprite = null;
    }
    void Update()
    {
        wait += Time.deltaTime;

        transform.LookAt(_player.position);

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
