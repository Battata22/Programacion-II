using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]

public class Chocamiento : MonoBehaviour
{
    [SerializeField] AreasSustoYDuda areas;
    [SerializeField] AreasSustoYDudaSonoros areasSonoro;

    [SerializeField]Pickable _objScript;
    [SerializeField] NPC _npcInRange;
    [SerializeField] float _scareRange = 16f, _doubtRange = 40f;
    public float scareAmount;

    LayerMask _layer;
    AudioSource _audioSource;
    AudioClip _audioClip;

    [SerializeField] bool musicOutput;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _objScript = gameObject.GetComponent<Pickable>();  

    }
    private void Start()
    {
        if (musicOutput) _audioSource.outputAudioMixerGroup = GameManager.Instance.AudioGroupMusic;
        else _audioSource.outputAudioMixerGroup = GameManager.Instance.AudioGroupSfx;
        _layer = GameManager.Instance.NpcLayers;
        //Debug.Log(_layer);
    }

    public void Choco(Vector3 pos)
    {
        //Instantiate(areas, pos, Quaternion.identity);
        //Debug.Log("<color=yellow> CHocamiento </color>");

        _audioSource.clip = _audioClip;
        _audioSource.Play();

        Collider[] colliders;
        colliders = Physics.OverlapSphere(pos, _doubtRange, _layer);
        foreach (var collider in colliders)
        {
            //Debug.Log("<color=blue> Buscando Npc en Susto</color>");
            if (collider.TryGetComponent<NPC>(out _npcInRange))
            {
                //Debug.Log("<color=pink> NPC en area Susto </color>");
                if (Vector3.SqrMagnitude(pos - _npcInRange.transform.position) <= (_scareRange * _scareRange) && scareAmount > 0)
                {
                    _npcInRange.GetScared(scareAmount);
                    if (_objScript)
                    {
                        //Debug.Log("<color=green> LLamado a nerfeo </color>");
                        _objScript.NerfObj();
                    }
                }
                else
                {
                    _npcInRange.GetDoubt(pos);
                }
            }
        }
    }

    public void ChocoSonoro(Vector3 pos)
    {
        //Instantiate(areasSonoro, pos, Quaternion.identity);
        //Debug.Log("<color=yellow> Sonoro </color>");

        _audioSource.clip = _audioClip;
        _audioSource.Play();

        Collider[] colliders;
        colliders = Physics.OverlapSphere(pos, _doubtRange, _layer);
        foreach (var collider in colliders)
        {
            //Debug.Log("<color=blue> Buscando Npc en Duda</color>");
            if (collider.TryGetComponent<NPC>(out _npcInRange))
            {
                _npcInRange.GetDoubt(pos);

                //Debug.Log("<color=pink> NPC en area Duda </color>");
                //if (Vector3.SqrMagnitude(pos - _npcInRange.transform.position) <= (_scareRange * _scareRange))
                //{
                //    _npcInRange.GetScared(scareAmount);
                //}
                //else
                //{
                //    _npcInRange.GetDoubt(pos);
                //}
            }
        }
    }

}
