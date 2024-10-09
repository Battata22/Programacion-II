using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconoSonido : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    [SerializeField] Sprite _soundSprite;
    [SerializeField] Transform _lookingAt;
    [SerializeField] AudioSource _audioSource;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponentInParent<AudioSource>();
        _spriteRenderer.sprite = null;
    }
    void Update()
    {

        if (_lookingAt == null)
        {
            _lookingAt = GameManager.Instance.Camera.transform;
        }

        transform.LookAt(_lookingAt.position);

        if (_audioSource.isPlaying == true)
        {
            _spriteRenderer.sprite = _soundSprite;
        }
        else
        {
            _spriteRenderer.sprite = null;
        }

    }
}
