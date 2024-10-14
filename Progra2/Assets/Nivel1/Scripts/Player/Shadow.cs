using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    Player player;
    [SerializeField] float _lTShadow,_speed;
    [SerializeField] List<Transform> _nodes = new();
    Transform _actualNode;
    public float _changeNodeDist = 0.5f;
    public bool _canMove = false , _col = false;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        _nodes = GameManager.Instance.AiNodes;
        _actualNode = GetNewNode();
    }

    private void Update()
    {
        if(!_canMove && player.nivel > 2)
        {
            _canMove = true;
            //_lTShadow += _lTShadow * 0.5f;
        }
        if (!_canMove) return;      
        if (_col) return;
        if (_nodes != null && Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)) _actualNode = GetNewNode(_actualNode);
        Movement();

    }

    void Movement()
    {
        var dir = (_actualNode.position - transform.position).normalized;
        transform.position += dir * Time.deltaTime * _speed;
        transform.LookAt(_actualNode.position);
    }

    protected virtual Transform GetNewNode(Transform lastNode = null)
    {
        Transform newNode = _nodes[Random.Range(1, _nodes.Count)];

        while (lastNode == newNode)
        {
            newNode = _nodes[Random.Range(1, _nodes.Count)];
        }

        return newNode;
    }

    public void Initialize(Player newPlayer)
    {
        //pedir a manager lista de npc para girar a verlos o algo
        player = newPlayer;

        if (player.nivel > 2)
            _lTShadow *= 2.5f;
        Destroy(gameObject, _lTShadow);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Chocaste con {other.name}");
        if(other.GetComponent<Ghostbuster>()) 
        {
            other.GetComponent<Ghostbuster>().AttackShadow(this.gameObject);
            _col = true;
        }
        else if (other.gameObject.GetComponent<Asustable>())
        {
            other.GetComponent<Asustable>().GetScared(0.5f);
            _col = true;
        }
    }

    private void OnDestroy()
    {
        player.currentShadows--;
    }
}
