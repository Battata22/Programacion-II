using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AINodeManager : MonoBehaviour
{
    private Transform[] _nodes;    

    private void Start()
    {
        _nodes = GetComponentsInChildren<Transform>();

        //GameManager.Instance.AiNodes.AddRange(_nodes);

        foreach(NPC npc in GameManager.Instance.Npc)
        {
            npc.NavMeshNodes.AddRange(_nodes);
            npc.Initialize();
        }
    }  

}
