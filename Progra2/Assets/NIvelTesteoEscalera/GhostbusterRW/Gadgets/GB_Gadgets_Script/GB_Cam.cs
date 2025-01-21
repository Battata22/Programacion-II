using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GB_Cam : GB_Gadget
{
    [SerializeField] Animator _camAnimator;

    [SerializeField] int _maxHp;
    int _hp;

    GameObject _detectionCone;

    //[SerializeField] float _speed;
    //[SerializeField,Range(0, 180), Tooltip("Aplica a ambos lados")] float _maxAngle;
    //[SerializeField] float rot;

    //[SerializeField] Animator _animator;


    //float rotDir = -1;

    private void Awake()
    {
        _hp = _maxHp;
    }

    private void Start()
    {
        _detectionCone = GetComponentInChildren<GB_CamCone>().gameObject;
        _camAnimator = GetComponentInChildren<Animator>();
    }

    public void DetectGhost()
    {
        //Pitido
        //Activar flash bang del GB

        //Debug.Log($"<color=green> Gus Detectado </color>");

        if (_myOwner != null)
            _myOwner.GetDoubt(transform.position,-1);
    }

    public override void GetDamage(int dmgAmount = 1)
    {
        _hp -= dmgAmount;

        if (_hp <= 0)
            Break();

        //Debug.Log($"<color=red> AHHHHHHHHH </color>");

    }

    public override void Break()
    {
        // efectos de rotura
        // desactivar deteccion
        // si no se cae hacer que se caiga, para efectos dramaticos

        if(_isBroken) return;

        _detectionCone.SetActive(false);
        _isBroken = true;
        _camAnimator.SetBool("Broken", true);

        _myOwner.AddToRepairList(this);
        OnBreak();
    }

    public override void Repair()
    {
        // efectos de reparacion
        // reactivar mierda

        if (!_isBroken) return;

        _hp = _maxHp;

        _camAnimator.SetBool("Repair", true);
        _detectionCone.SetActive(true);
        _isBroken = false;


        OnRepair();
    }

    //Developer

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                Repair();
            }       
        }
    }
}
