using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour, IInteractable
{
    //You know
    // que chota hay que hacer?
    //puerta arranca bloqueada
    // completas sala se desbloquea
    //cuando un npc entra en el trigger hacer animacion
    // la animacion tiene que cambiar reproducice para el lado que camine el bot, clarito?

    [SerializeField] Animator _myAnim;
    [Range(0, 360)]
    [SerializeField] float _angle;

    [SerializeField] LayerMask _normalLayer;
    [SerializeField] LayerMask _lockedLayer;
    [SerializeField] bool _locked;
    [SerializeField] Collider _myColider;
    bool _isOpen = false;

    public event DelegateType.VoidDelegate OnDoorOpen = delegate { };
    public event DelegateType.VoidDelegate OnDoorClose = delegate { };

    void Awake()
    {

        //_normalLayer.value = gameObject.layer;

        //Debug.Log($"{gameObject.layer}, {_normalLayer.value}, {_lockedLayer.value}");

    }

    private void Start()
    {
        _myAnim = GetComponentInChildren<Animator>();
    }

    public void LockDoor()
    {
        gameObject.layer = LayerMask.NameToLayer("Wall");

        _locked = true;
        transform.GetComponent<NavMeshObstacle>().enabled = true;
        //Debug.Log("Puerta Cerrada");
    }

    public void UnlockDoor()
    {
        gameObject.layer = LayerMask.NameToLayer("NoTras");

        //Desactivo porque soy la verga compadre
        _locked = false;

        transform.GetComponent<NavMeshObstacle>().enabled = false;
        //gameObject.SetActive(false); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_locked) return;
        if(other.transform.TryGetComponent<NPC>(out var npc))
        {
            PlayAnim(npc.transform);
        }
        if(other.transform.TryGetComponent<Player>(out var player))
        {
            PlayAnim(player.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (_locked) return;
        if (other.transform.GetComponent<NPC>() || other.transform.GetComponent<Player>())
            CloseAnim();
    }

    void PlayAnim(Transform opener)
    {
        if (_myAnim == null) return;
        if (_isOpen) return;
        var dir = (new Vector3(opener.transform.position.x,transform.position.y,opener.transform.position.z) - transform.position).normalized;

        if(Vector3.Angle(dir, transform.forward) > 90)
        {
            _myAnim.SetBool("OpenF", true);
            //Debug.Log("<color=blue>Abro para adelante</color>");
        }
        else
        {
            _myAnim.SetBool("OpenB", true);
            //Debug.Log("<color=blue>Abro para atras</color>");
        }
        _isOpen = true;
        SetColider(!_isOpen);

        OnDoorOpen();
    }

    void CloseAnim()
    {
        if (_myAnim == null) return;
        if(!_isOpen) return;

        _myAnim.SetBool("Close", true);
        SetColider(!_isOpen);
        _isOpen = false;

        OnDoorClose();
    }

    public void SetColider(bool newState)
    {
        _myColider.enabled = newState;
    }

    public void Interact()
    {
        Debug.Log("<color=green>Gus intenta abrir puerta</color>");

        PlayAnim(GameManager.Instance.Player.transform);
    }
}
