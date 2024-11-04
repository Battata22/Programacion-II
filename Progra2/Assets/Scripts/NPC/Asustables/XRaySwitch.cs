using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRaySwitch : MonoBehaviour
{
    [SerializeField] LayerMask _defaultLayer, _xRayLayer;

    bool _active = false;
    float _cd = 10.1f, _lastActivate = -60f, _activeTime = 10f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(2) && Time.time - _lastActivate > _cd)
        {
            _active = true;
            int layerNum = (int)Mathf.Log(_xRayLayer.value, 2);
            gameObject.layer = layerNum;

            if (transform.childCount > 0)
                ChangeLayerInAllChildrens(transform, layerNum);
            _lastActivate = Time.time;
            //Debug.Log("Prendido");
            StartCoroutine(Deactivate());
            //if (_active)
            //{
            //    _active = false;
            //    int layerNum = (int)Mathf.Log(_defaultLayer.value, 2);
            //    gameObject.layer = layerNum;

            //    if (transform.childCount > 0)
            //        ChangeLayerInAllChildrens(transform, layerNum);
            //}
            //else
            //{
            //    _active = true;
            //    int layerNum = (int)Mathf.Log(_xRayLayer.value, 2);
            //    gameObject.layer = layerNum;

            //    if (transform.childCount > 0)
            //        ChangeLayerInAllChildrens(transform, layerNum);
            //}
        }
    }

    void ChangeLayerInAllChildrens(Transform root, int layer)
    {
        //Debug.Log($"Tengo {transform.childCount} hijos");   
        var children = root.GetComponentsInChildren<Transform>(includeInactive: true);

        foreach (Transform child in children)
        {
            child.gameObject.layer = layer;
        }
    }

    private IEnumerator Deactivate()
    {
        //Debug.Log($"Desactivando en {_activeTime}") ;
        yield return new WaitForSeconds(_activeTime);

        _active = false;
        int layerNum = (int)Mathf.Log(_defaultLayer.value, 2);
        gameObject.layer = layerNum;

        if (transform.childCount > 0)
            ChangeLayerInAllChildrens(transform, layerNum);
        //Debug.Log("Apagado");
    }
}
