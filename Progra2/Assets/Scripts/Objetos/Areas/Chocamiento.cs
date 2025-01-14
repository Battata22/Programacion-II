using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

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

    [SerializeField] bool musicOutput;

    Transform _farthestNode, _farthestNode2, _farthestNode3, _finalNode;

    public event DelegateType.VoidDelegate OnChocoActive = delegate { };


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

        _audioSource.clip = GameManager.Instance.choque;
        _audioSource.Play();
 
        //GetFarthestNode();

        Collider[] colliders;
        colliders = Physics.OverlapSphere(pos, _doubtRange, _layer);
        foreach (var collider in colliders)
        {
            //Debug.Log("<color=blue> Buscando Npc en Susto</color>");
            if (collider.gameObject.TryGetComponent<NPC>(out _npcInRange))
            {
                //Debug.Log("<color=pink> NPC en area Susto </color>");

                //Debug.Log($"<color=green> Room de Objeto {gameObject.GetComponent<Pickable>().actualRoom} Room de asustable {_npcInRange.actualRoom}</color>");

                if (Vector3.Distance(pos, _npcInRange.transform.position) <= _scareRange && gameObject.GetComponent<Pickable>().actualRoom == _npcInRange.actualRoom)
                //if (Vector3.SqrMagnitude(pos - _npcInRange.transform.position) <= (_scareRange * _scareRange) && scareAmount > 0)
                {
                    //GetBetterNode(_npcInRange, GameManager.Instance.Player);

                    if(_npcInRange.TryGetComponent<Exorcista>(out var exorcista))
                    {
                        exorcista.GetScared(scareAmount, transform);
                    }
                    else
                    _npcInRange.GetScared(scareAmount, _finalNode);

                    #region Comment
                    //if (_objScript)
                    //{
                    //    //Debug.Log("<color=green> LLamado a nerfeo </color>");

                    //    //Nerf si asusta
                    //    //_objScript.NerfObj();
                    //} 
                    #endregion

                }
                else
                {
                    _npcInRange.GetDoubt(pos);
                }
            }
        }

        OnChocoActive();

        //ResetFarthestNode();

    }

    public void ChocoNPC(Vector3 pos, Asustable asustableScript)
    {
        //Instantiate(areas, pos, Quaternion.identity);
        //Debug.Log("<color=yellow> CHocamiento </color>");

        GetFarthestNode();
        GetBetterNode(asustableScript, GameManager.Instance.Player);

        //print("antes del nodo");
        //GetBetterNode(_npcInRange, GameManager.Instance.Player);
        //print("despues del nodo");
        asustableScript.GetScared(scareAmount, _finalNode);
        //print("despues del get scared");

    }

    public void ChocoSonoro(Vector3 pos)
    {
        #region Comment
        //Instantiate(areasSonoro, pos, Quaternion.identity);
        //Debug.Log("<color=yellow> Sonoro </color>");

        //_audioSource.clip = GameManager.Instance.choque;
        //_audioSource.Play(); 
        #endregion

        Collider[] colliders;
        colliders = Physics.OverlapSphere(pos, _doubtRange, _layer);
        foreach (var collider in colliders)
        {
            //Debug.Log("<color=blue> Buscando Npc en Duda</color>");
            if (collider.TryGetComponent<NPC>(out _npcInRange))
            {
                _npcInRange.GetDoubt(pos);

                #region Comment
                //Debug.Log("<color=pink> NPC en area Duda </color>");
                //if (Vector3.SqrMagnitude(pos - _npcInRange.transform.position) <= (_scareRange * _scareRange))
                //{
                //    _npcInRange.GetScared(scareAmount);
                //}
                //else
                //{
                //    _npcInRange.GetDoubt(pos);
                //} 
                #endregion
            }
        }
    }

    void GetFarthestNode()
    {
        _farthestNode = null;
        var farthestDis = -1f;
        var nodes = GameManager.Instance.activeNodes;
        foreach(var node in nodes)
        {
            var Dis = (node.position - transform.position).sqrMagnitude;
            if (Dis > farthestDis)
            {
                farthestDis = Dis;
                _farthestNode3 = _farthestNode2;
                _farthestNode2 = _farthestNode;
                _farthestNode = node;
            }
        }
        //print($"<color=magenta> Nodo mas lejano {_farthestNode.name} </color>");
    }

    void GetBetterNode(NPC target, Player player)
    {
        //Debug.Log($"NPC detectado {target.name}");
        //Debug.Log($"Player detectado {player.name}");

        var playerVector = player.transform.position - target.transform.position;
        if (_farthestNode == null) Debug.Log("Nodo mas lejano = Null");
        if (target == null) Debug.Log("Como carajo falta el nps?");
        var node1vector = _farthestNode.position - target.transform.position;
        var angle1 = Vector3.Angle(playerVector, node1vector);

        Single angle2 = angle1;
        Single angle3 = angle1;
        if (_farthestNode2 != null)
        {
            Vector3 node2Vector = _farthestNode2.position - target.transform.position;
            angle2 = Vector3.Angle(playerVector, node2Vector);
        }

        if(_farthestNode3 != null)
        {
            var node3Vector = _farthestNode3.position - target.transform.position;
            angle3 = Vector3.Angle(playerVector, node3Vector);
        }


        _finalNode = _farthestNode;

        if (angle1 > angle2 && angle1 > angle3)
            _finalNode = _farthestNode;
        if(angle2 > angle1 && angle2 > angle3)
            _finalNode = _farthestNode2;
        if(angle3 > angle1 && angle2 > angle3)
            _finalNode = _farthestNode3;

        //_farthestNode = null;
        //_farthestNode2 = null;
        //_farthestNode3 = null;
    }

    void ResetFarthestNode()
    {
        _farthestNode = null;
        _farthestNode2 = null;
        _farthestNode3 = null;
    }


}
