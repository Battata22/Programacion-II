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

    [SerializeField] bool musicOutput;

    Transform _farthestNode, _farthestNode2, _farthestNode3, _finalNode;


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
        
        GetFarthestNode();

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
                    GetBetterNode(_npcInRange, GameManager.Instance.Player);
                    _npcInRange.GetScared(scareAmount, _finalNode);
                    //if (_objScript)
                    //{
                    //    //Debug.Log("<color=green> LLamado a nerfeo </color>");
                        
                    //    //Nerf si asusta
                    //    //_objScript.NerfObj();
                    //}
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
        var nodes = GameManager.Instance.AiNodes;
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
        Debug.Log($"NPC detectado {target.name}");
        Debug.Log($"Player detectado {player.name}");

        var playerVector = player.transform.position - target.transform.position;
        var node1vector = _farthestNode.position - target.transform.position;
        var node2Vector = _farthestNode2.position - target.transform.position;
        var node3Vector = _farthestNode3.position - target.transform.position;

        var angle1 = Vector3.Angle(playerVector, node1vector);
        var angle2 = Vector3.Angle(playerVector, node2Vector);
        var angle3 = Vector3.Angle(playerVector, node3Vector);

        _finalNode = _farthestNode;

        if (angle1 > angle2 && angle1 > angle3)
            _finalNode = _farthestNode;
        if(angle2 > angle1 && angle2 > angle3)
            _finalNode = _farthestNode2;
        if(angle3 > angle1 && angle2 > angle3)
            _finalNode = _farthestNode3;

        _farthestNode = null;
        _farthestNode2 = null;
        _farthestNode3 = null;
    }

}
