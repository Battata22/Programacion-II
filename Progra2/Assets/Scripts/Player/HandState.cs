using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class HandState : MonoBehaviour
{
    [SerializeField] public bool pointing = false, relax = false, holding = false; //esto se sigue usando para la logica no para las poses
    //[SerializeField] Mesh _relaxMesh, _pointingMesh, _holdingMesh, _tensMesh;
    [SerializeField, Tooltip("<color=green> Poner las poses en el mismo orden que la lista de currenPose </color>")]
    GameObject[] _handPose;

    [SerializeField] public HandPose currentPose = HandPose.relax;

    MeshFilter _filter;
    Vector3 _up;

    private void Start()
    {
        GameManager.Instance.HandState = this;
        _filter = gameObject.GetComponent<MeshFilter>();

        _up = transform.up;

        ChangeState(HandState.HandPose.relax);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Nivel5")
        {
            ChangeState(HandPose.gun);
        }
    }

    public void ChangeState(HandPose newPose)
    {
        #region comment
        //if (holding && !relax)
        //{
        //    //  transform.up = _up;

        //    _filter.mesh = _holdingMesh;
        //   //Debug.Log("<color=cyan> Mano agarrando </color>");
        //}
        //if (pointing && !holding && !relax)
        //{

        //    _filter.mesh = _pointingMesh;
        //    //Debug.Log("<color=magenta> Mano apuntando </color>");
        //}
        //if (!pointing && !holding && relax)
        //{
        //    //transform.up = _up;

        //    _filter.mesh = _relaxMesh;
        //    //Debug.Log("<color=green> Mano relajada </color>");
        //}
        //if(!pointing && !holding && !relax)
        //{
        //    //transform.up = _up;

        //    _filter.mesh= _relaxMesh;
        //    //Debug.Log("<color=green> Mano relajada </color>");
        //} 
        #endregion

        currentPose = newPose;

        
        if (holding) return;//si no esta esto las poses cambian mientras sostenes algo

        for (int i = 0; i < _handPose.Length; i++)
        {
            if (i == (int)currentPose)
                _handPose[i].SetActive(true);
            else
                _handPose[i].SetActive(false);
        }
    }

    public enum HandPose
    {
        relax,
        point,
        hold,
        gun
    }

}
