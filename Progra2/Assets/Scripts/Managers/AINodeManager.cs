using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AINodeManager : MonoBehaviour
{
    private Transform[] _nodes;
    //NPC _npc;

    private IEnumerator Start()
    {
        _nodes = GetComponentsInChildren<Transform>();

        GameManager.Instance.AiNodes.AddRange(_nodes);

        yield return new WaitForEndOfFrame();

        //foreach (NPC npc in GameManager.Instance.Npc)
        //{
        //    npc._testNodes.AddRange(_nodes);
        //    //npc.Initialize();
        //    //npc.gameObject.SetActive(true);
        //}
    }

    private void Update()
    {
        //foreach (NPC npc in GameManager.Instance.Npc)
        //{
        //    if (npc.NavMeshNodes != null) return;
        //    npc.NavMeshNodes.AddRange(_nodes);
        //    npc.Initialize();
        //    //npc.gameObject.SetActive(true);
        //}
    }

    private void OnDestroy()
    {
        GameManager.Instance.AiNodes.Clear();
    }

}
