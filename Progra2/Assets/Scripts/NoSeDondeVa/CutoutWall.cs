using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutWall : MonoBehaviour
{
    [SerializeField] Transform _targetObject;

    [SerializeField] LayerMask _wallLayer;

    Camera _cam;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        Vector2 cutoutPos=_cam.WorldToViewportPoint(transform.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Vector3 offset=_targetObject.position - transform.position;
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
}
