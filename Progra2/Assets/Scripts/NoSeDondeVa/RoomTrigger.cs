using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RoomTrigger : MonoBehaviour
{
    [SerializeField] int _thisRoom;
    public int roomIndex;
    [SerializeField] Transform[] _roomNodes;
    [SerializeField] AINodeManager _nodeManager;

    [SerializeField] bool _activeOnStart = false;


    private IEnumerator Start()
    {
        _nodeManager = GetComponentInParent<AINodeManager>();
        List<Transform> finalNodes = new();

        _roomNodes = GetComponentsInChildren<Transform>();

        foreach(var node in _roomNodes)
        {
            if (node.position != Vector3.zero)
                finalNodes.Add(node);
            //else
                //Debug.Log($"<color=green> {transform.name} elimino nodo {node.name}</color>");
        }

        //_nodeManager.roomsNodes.Add(finalNodes);
        _nodeManager.SetRoomNodesList(finalNodes, out roomIndex);

        //Debug.Log($"<color=yellow> {roomIndex} </color>");
        yield return new WaitForEndOfFrame();

        if(_activeOnStart)
            _nodeManager.SetActiveNodes(roomIndex);
    }

    [SerializeField] ParticleSystem[] _sahumerioParticles;

    void ActivarParticulasSahumerio()
    {
        foreach(var  particle in _sahumerioParticles)
        {
            particle.Play();
        }
    }

    void DesactivarParticulasSahumerio()
    {
        foreach (var particle in _sahumerioParticles)
        {
            particle.Stop();
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IRoomDetectable>(out var coso))
        {
            coso.SetRoom(roomIndex);
        }
        if(other.TryGetComponent<Sahumerio>(out var sahumerio) && sahumerio.prendido)
        {
            ActivarParticulasSahumerio();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<Sahumerio>(out var sahumerio))
        {
            DesactivarParticulasSahumerio();
        }
    }

}
