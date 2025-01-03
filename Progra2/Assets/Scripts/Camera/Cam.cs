using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cam : MonoBehaviour
{
    [SerializeField] bool _followPlayer = true;
    [SerializeField] public Vector3 point;
    [SerializeField] Transform _target;

    public bool usePoint=false;

    private void Start()
    {
        GameManager.Instance.Camera = this;
    }

    private void LateUpdate()
    {
        if (_followPlayer) 
            UpdatePosition();  
    }

    private void UpdatePosition()
    {
        //el punto se lo da el CamDistance
        if (usePoint)
        {
            transform.position = point;
        }
        else
        {
            transform.position = _target.position;
        }

        transform.forward = _target.forward;
    }

    public void FollowPlayer(bool doFollow)
    {
        _followPlayer = doFollow;
    }

    //QUIZA ESTO ESTABA PARA EL MUSEO

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        SceneManager.LoadScene("Menu");
    //    }
    //}
}
