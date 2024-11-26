using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AINodeManager : MonoBehaviour
{
    private Transform[] _nodes;
    int roomCount = 0;

    [SerializeField] List<List<Transform>> roomsNodes = new();
    //NPC _npc;

    private IEnumerator Start()
    {
        GameManager.Instance.AINodeManager = this;
        List<Transform> finalNodes = new();

        _nodes = GetComponentsInChildren<Transform>();

        foreach(var node in _nodes)
        {
            if (node.position != Vector3.zero)
                finalNodes.Add(node);
            //else
                //Debug.Log($"<color=red>Nodo descartado: {node.name}</color>");
        }

        GameManager.Instance.AiNodes.AddRange(finalNodes);

        yield return new WaitForEndOfFrame();

    }

    //List<Transform> activeNodes = new();
    public void SetActiveNodes(int index, bool callClear = false)
    {
        Debug.Log($"AHHHHHHHHHHHHHHHHHHHHHH {index}");
        if(callClear)
            ClearActiveNodes();

        GameManager.Instance.activeNodes.AddRange(roomsNodes[index]);
    }

    public void SetRoomNodesList(List<Transform> newList, out int index)
    {
        roomsNodes.Add(newList);

        index = roomCount;

        roomCount++;
    }

    public void ClearActiveNodes()
    {
        GameManager.Instance.activeNodes.Clear();
    }


    private void OnDestroy()
    {
        GameManager.Instance.AiNodes.Clear();
        ClearActiveNodes();
    }

}
