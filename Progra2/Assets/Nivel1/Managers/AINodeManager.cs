using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINodeManager : MonoBehaviour
{
    [SerializeField] private Transform[] _nodes;

    private void Start()
    {
        _nodes = GetComponentsInChildren<Transform>();

        foreach(NPC npc in GameManager.Instance.NPC)
        {
            npc.NavMeshNodes.AddRange(_nodes);
            npc.Initialized();
        }
    }

}
