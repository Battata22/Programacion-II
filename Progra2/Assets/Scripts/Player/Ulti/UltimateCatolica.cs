using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateCatolica : SpecialObject
{
    //Idea
    //"giramos toda la casa"

    //Pasos
    //Detectar todo los ragdolleables y hacerlos ragdoll
    //Detectar todas las cosas que tengan rigidbody, sacale las cosnrains y activar gravedad
    //Girar la casa
    //Resar que salga bien

    [SerializeField] GameObject _houseCenter;
    [SerializeField] LayerMask _objectMask;
    [SerializeField] float _ultRad;
    [SerializeField] float _rotSpeed;

    [SerializeField] string[] _textosSatanicosos;

    [SerializeField] ChildScript _childScript;

    bool createTrap = false;
    Vector3 _rotPivot = Vector3.zero;

    DelegateType.VoidDelegate DoRot = delegate { };

    private void Update()
    {
        if (createTrap && GameManager.Instance.terrorBar.value >= GameManager.Instance.terrorBar.maxValue)
        {
            createTrap = false;
            CreateTrap();
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            createTrap = false;
            CreateTrap();
        }
    }

    private void FixedUpdate()
    {
        DoRot();
    }

    public void Activar()
    {
        createTrap = true;
    }

    public override void CreateTrap()
    {
        base.CreateTrap();
        _rotPivot = _houseCenter.transform.position + new Vector3(0,2f,0);

        GameManager.Instance.ChangeObjectiveText(_textosSatanicosos[0]);
    }

    protected override void ObjectAbility(Transform origin)
    {
        var npcs = Physics.OverlapSphere(transform.position, _ultRad, GameManager.Instance.NpcLayers);
        var objetos = Physics.OverlapSphere(transform.position, _ultRad , _objectMask);

        foreach(var obj in objetos)
        {
            if(obj.transform.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.useGravity = true;
            }
        }

        foreach(var npc in npcs)
        {
            if(npc.transform.TryGetComponent<IRagdoll>(out var ragdoll))
            {
                ragdoll.CallRagdollOn();
            }
        }

        _childScript.gameObject.SetActive(false);

        DoRot = RotameEsta;
        GameManager.Instance.ChangeObjectiveText(_textosSatanicosos[1]);

    }

    void RotameEsta()
    {
        _houseCenter.transform.RotateAround(_rotPivot, transform.forward, 90 * _rotSpeed * Time.fixedDeltaTime);
        
        if(_houseCenter.transform.rotation.z % 170 >= 1)
        {
            DoRot = delegate { };

            _houseCenter.transform.rotation = Quaternion.Euler(0, 0, 180);

            Invoke("CallWin", 5f);
        }
    }

    void CallWin()
    {
        GameManager.Instance.CompleteLevel();

    }
}


