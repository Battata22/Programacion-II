using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutWall : MonoBehaviour
{
    [SerializeField] Transform _targetObject;

    [SerializeField] LayerMask _wallLayer;

    [SerializeField]Transform origin;

    RaycastHit[] _hitObjects;
    //RaycastHit[] _lastHitObjs;

    Camera _cam;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        #region comment
        //Vector2 cutoutPos=_cam.WorldToViewportPoint(transform.position);
        //cutoutPos.y /= (Screen.width / Screen.height);

        //Vector3 offset=_targetObject.position - transform.position;
        //Debug.DrawLine(transform.position, transform.position + offset, Color.red);
        ////RaycastHit[] hitObjects = Physics.RaycastAll(_targetObject.transform.position, _targetObject.transform.forward, 1f, _wallLayer);
        //RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, _wallLayer);

        //for (int i = 0; i < hitObjects.Length; i++)
        //{
        //    Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

        //    for (int m = 0; m < materials.Length; m++)
        //    {
        //        materials[m].SetVector("_cutoutPosition", cutoutPos);
        //        materials[m].SetFloat("_cutoutSize", 0.1f);
        //        materials[m].SetFloat("_falloffSize", 0.05f);
        //    }          
        //}        
        #endregion

        //Magic();
        Magic2();
    }

    void Magic()
    {
        Vector2 cutoutPos = _cam.WorldToViewportPoint(transform.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Vector3 offset = _targetObject.position - transform.position;
        Debug.DrawLine(transform.position, transform.position + offset, Color.red);
        //RaycastHit[] hitObjects = Physics.RaycastAll(_targetObject.transform.position, _targetObject.transform.forward, 1f, _wallLayer);
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, _wallLayer);

        for (int i = 0; i < hitObjects.Length; i++)
        {
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            for (int m = 0; m < materials.Length; m++)
            {
                materials[m].SetVector("_cutoutPosition", cutoutPos);
                materials[m].SetFloat("_cutoutSize", 0.1f);
                materials[m].SetFloat("_falloffSize", 0.05f);
            }
        }
    }

    void Magic2()
    {
        RaycastHit[] _lastHitObjs = _hitObjects;

        Vector2 cutoutPos = _cam.WorldToViewportPoint(origin.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Vector3 offset = _targetObject.position - origin.position;
        Debug.DrawLine(origin.position, origin.position + offset, Color.red);
        //RaycastHit[] hitObjects = Physics.RaycastAll(_targetObject.transform.position, _targetObject.transform.forward, 1f, _wallLayer);
        _hitObjects = null;
        _hitObjects = Physics.RaycastAll(origin.position, offset, offset.magnitude, _wallLayer);

        if( _hitObjects != null )
        StartCoroutine(ResetLastObj(cutoutPos));

        for (int i = 0; i < _hitObjects.Length; i++)
        {

            //Debug.Log($"{_hitObjects[i].transform.name}");

            Material[] materials = _hitObjects[i].transform.GetComponent<Renderer>().materials;

            for (int m = 0; m < materials.Length; m++)
            {
                materials[m].SetVector("_cutoutPosition", cutoutPos);
                materials[m].SetFloat("_cutoutSize", 0.2f);
                materials[m].SetFloat("_falloffSize", 0.1f);
            }
        }

        //if (_lastHitObjs != null && _hitObjects == null)

        //ResetLastObj(_lastHitObjs, cutoutPos);
    }

    void ResetLastObj(RaycastHit[] lastHitObjs, Vector2 cutoutPos)
    {
        if (lastHitObjs == null)
        {
            Debug.Log($"<color=green>Last = Null</color>");
            return;
        }

        Debug.Log("Entre al reset");
        for (int i = 0; i < lastHitObjs.Length; i++)
        {
            Material[] materials = lastHitObjs[i].transform.GetComponent<Renderer>().materials;

            for (int m = 0; m < materials.Length; m++)
            {
                materials[m].SetVector("_cutoutPosition", cutoutPos);
                materials[m].SetFloat("_cutoutSize", 0.0f);
                materials[m].SetFloat("_falloffSize", 0.0f);
            }
        }
    }

    IEnumerator ResetLastObj(Vector2 cutoutPos)
    {
        yield return new WaitForEndOfFrame();

        if (_hitObjects == null)
        {
            Debug.Log($"<color=green>Last = Null</color>");
        }

        //Debug.Log("Entre al reset");
        for (int i = 0; i < _hitObjects.Length; i++)
        {
            Debug.Log($"<color=grey>Pared Actualizada</color>");

            Material[] materials = _hitObjects[i].transform.GetComponent<Renderer>().materials;

            for (int m = 0; m < materials.Length; m++)
            {
                materials[m].SetVector("_cutoutPosition", cutoutPos);
                materials[m].SetFloat("_cutoutSize", 0.0f);
                materials[m].SetFloat("_falloffSize", 0.0f);
            }
        }
    }
}
