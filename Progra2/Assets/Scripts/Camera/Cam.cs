using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cam : MonoBehaviour
{
    [SerializeField] bool _followPlayer = true;
    [SerializeField] public Vector3 point;
    [SerializeField] public Vector3 surfaceNormal;
    [SerializeField] Transform _target;
    [SerializeField] Transform _camCenter;
    [SerializeField] float _offset;
    [SerializeField] float _maxDist;
    [SerializeField] float _stopChangeDist;
    [SerializeField] float _camSpeed;

    public bool followPlayer { get { return _followPlayer; } }

    public bool usePoint=false;

    private void Start()
    {
        GameManager.Instance.Camera = this;
        _maxDist = _target.localPosition.sqrMagnitude;
    }

    private void LateUpdate()
    {
        if (_followPlayer) 
            UpdatePosition();  
    }

    private void UpdatePosition()
    {
        //el punto se lo da el CamDistance
        if (usePoint && Vector3.SqrMagnitude(point-_camCenter.position) <= _maxDist)
        {
            var dis = Vector3.Angle(surfaceNormal, transform.forward);
            
            transform.position = point + surfaceNormal * (dis * _offset);
            //transform.position = point;
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

    public void ChangeFloor()
    {
        _followPlayer = false;

        StartCoroutine(FloorMovement());
    }

    IEnumerator FloorMovement()
    {
        yield return new WaitForSeconds(0.02f);

        while (!_followPlayer)
        {
            var dir = (_target.position - transform.position).normalized;

            transform.position += dir * _camSpeed * Time.deltaTime;
            transform.forward = _target.forward;

            if (Vector3.SqrMagnitude(_target.position - transform.position) <= (_stopChangeDist * _stopChangeDist))
                _followPlayer = true;

            yield return new WaitForEndOfFrame();
        }
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
