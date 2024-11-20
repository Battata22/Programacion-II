using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWallCol : MonoBehaviour
{
    [SerializeField] LayerMask _ogMask;
    [SerializeField] LayerMask _noTras;

    private void Awake()
    {
        //_ogMask = gameObject.layer;
        GameManager.Instance.Player.OnCrazyScape += ChangeToNoTras;
    }

    void ChangeToNoTras()
    {
        int layerNum = (int)Mathf.Log(_noTras.value, 2);
        gameObject.layer = layerNum;
        GameManager.Instance.Player.OnCrazyScape -= ChangeToNoTras;
        GameManager.Instance.Player.OnStopBounce += ChangeToOG;
    }

    void ChangeToOG()
    {
        //Debug.Log($" {_ogMask.value}");
        //int layerNum = (int)Mathf.Log(_ogMask.value, 2);
        gameObject.layer = 31;// HardCoded because yes
        GameManager.Instance.Player.OnCrazyScape += ChangeToNoTras;
        GameManager.Instance.Player.OnStopBounce -= ChangeToOG;

    }

    private void OnDestroy()
    {
        GameManager.Instance.Player.OnStopBounce -= ChangeToOG;
        GameManager.Instance.Player.OnCrazyScape -= ChangeToNoTras;
    }
}
