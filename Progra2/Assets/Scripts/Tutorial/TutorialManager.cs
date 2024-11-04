using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("<color=red> Necesario </color>")]
    [SerializeField] Image _pickUpImg;
    [SerializeField] Image _throwImg, _dropImg, _interactImg;

    [SerializeField] PickUp pickUpScript;

    //[SerializeField] TextMeshPro _pickUpTxt, _throwTxt, _dropTxt, _interactTxt, _catTxt, _grannyTxt, _gbTxt;
    public bool pickUpTuto = false, throwTuto = false, dropTuto = false, interactTuto = false, catTuto = false, grannyTuto = false, gbTuto = false, wallingTuto = false;
    [SerializeField] float _speedMult;

    TutoImage[] _tutoPick;

    float timer;
    int axisSelector;
    bool canMove, moveInAxis, startTimer = false;
    Image _actualImage;
    Vector3 _finPos;

    private void Awake()
    {
        _tutoPick = GetComponentsInChildren<TutoImage>();
        pickUpScript.OnPickUp += EndPickUp;
        pickUpScript.OnInteract += EndInteract;
    }
    private void Start()
    {
        GameManager.Instance.Tutorial = this;
        //PickUp();
    }

    

    public void StartPickUp()
    { 
        pickUpTuto=true;
        _tutoPick[0].animator.SetBool("In", true);
    }
    public void EndPickUp() 
    {       
        pickUpTuto = false;       
        _tutoPick[0].animator.SetBool("Out", true);
        StartDrop();
        pickUpScript.OnPickUp -= EndPickUp;
    }

    public void StartThrow()
    {
        throwTuto = true;
        _tutoPick[1].animator.SetBool("In", true);
    }
    public void EndThrow() 
    {
        throwTuto = false;
        _tutoPick[1].animator.SetBool("Out", true);
        StartInteract();
    }

    public void StartDrop()
    {
        dropTuto = true;
        _tutoPick[2].animator.SetBool("In", true);
        StartThrow();
    }
    public void EndDrop() 
    {
        dropTuto = false;
        _tutoPick[2].animator.SetBool("Out", true);
        StartWalling();
    }

    public void StartInteract()
    {
        interactTuto = true;
        _tutoPick[3].animator.SetBool("In", true);
    }
    public void EndInteract() 
    {
        interactTuto = false;
        _tutoPick[3].animator.SetBool("Out", true);
        pickUpScript.OnInteract -= EndInteract;
    }

    public void StartWalling()
    {
        wallingTuto = true;
        _tutoPick[4].animator.SetBool("In", true);
    }

    public void EndWalling()
    {
        wallingTuto = false;
        _tutoPick[4].animator.SetBool("Out", true);
    }

    #region Comment
    //void MoveImage()
    //{
    //    if (!canMove) return;
    //    var dir = (_final1.transform.position - _actualImage.transform.position).normalized;
    //    _actualImage.transform.position += dir * Time.deltaTime * _speedMult;
    //}

    //void MoveInAxis(int num)
    //{

    //    switch (num)
    //    {
    //        case 0:
    //            _actualImage.transform.position += _actualImage.transform.up * Time.deltaTime * _speedMult;
    //            break;
    //        case 1:
    //            _actualImage.transform.position += _actualImage.transform.right * Time.deltaTime * _speedMult;
    //            break;
    //        case 2:
    //            _actualImage.transform.position += _actualImage.transform.up * Time.deltaTime * -_speedMult;
    //            break;
    //        case 4:
    //            _actualImage.transform.position += _actualImage.transform.right * Time.deltaTime * -_speedMult;
    //            break;           
    //    }
    //} 
    #endregion

    private void OnDestroy()
    {
        GameManager.Instance.Tutorial = null;
    }
}
