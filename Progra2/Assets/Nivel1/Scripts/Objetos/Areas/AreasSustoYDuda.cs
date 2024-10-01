using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreasSustoYDuda : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] AudioClip chocar1;
    float wait;
    Vector3 muymuylejano = new Vector3(1000f, 1000f, 1000f);
    protected bool dudando = false, asustado = false;

    protected virtual void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        _audioSource.clip = chocar1;
        _audioSource.Play();
    }

    void Update()
    {
        wait += Time.deltaTime;

        if (wait >= 0.5f)
        {
            //transform.position = muymuylejano;
            Destroy(gameObject);
        }
    }
        
}
