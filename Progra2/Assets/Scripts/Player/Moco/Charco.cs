using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Charco : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip caminarNpc, reboteItem;
    [SerializeField] bool _doSlide;
    [SerializeField] float _lifeTime;//4.65f era el default antes
    [SerializeField] float _internalCD;
    float lastSlide;
    [SerializeField] int _uses;

    private void Awake()
    {
        lastSlide = -_internalCD;
        source = GetComponent<AudioSource>();
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_doSlide && other.gameObject.TryGetComponent<Asustable>(out var asusScript))
        {
            asusScript.StartSlow();
            source.clip = caminarNpc;
            //source.loop = true;
            source.Play();
        }

        if (_doSlide && other.gameObject.TryGetComponent<ICanSlide>(out var _target) && Time.time - lastSlide > _internalCD)
        {
            _target.StartSlide(other.transform.forward, 12f);
            lastSlide = Time.time;
            _uses--;
            if (_uses <= 0)
                Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (!_doSlide && other.gameObject.TryGetComponent<Asustable>(out Asustable asusScript))
        {
            asusScript.StopSlow();
            //source.loop = false;
            source.Stop();
        }

        //if(_doSlide && Time.time - lastSlide < _internalCD)
        //{
        //    _uses--;
        //    if (_uses <= 0)
        //        Destroy(gameObject);
        //}
    }
}
