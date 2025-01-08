using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GB_Flashbang : GB_Gadget
{
    [SerializeField] LayerMask obstructions;
    [SerializeField] float _scaleDuration;
    [SerializeField] float _scaleSpeed;
    [SerializeField] float _maxScale;

    float _currentTime;
    bool _doDoubt = false;

    public override void Initialize(Ghostbuster newOwner, bool doDoubt = false)
    {
        base.Initialize(newOwner, doDoubt);
        _doDoubt = doDoubt;
    }

    public override void GetDamage(int noSeUsaXDD)
    {
        //No se puede romper
        //o si
        //no se jaja
    }
    public override void Break()
    {

    }
    public override void Repair()
    {
        //No se puede reparar
        //porque no se rompe
        //a no ser que se rompa, entonces si se puede reparar
        //a menos que no sea irreparable XDD
    }

    private void Awake()
    {
        _currentTime = 0;
        Destroy(gameObject, _scaleDuration);
        dev_ogScale = transform.localScale;
    }

    private void Update()
    {
        if (_currentTime < _scaleDuration)
        {
            _currentTime += Time.deltaTime;
            ScaleObject();
        }

        Dev_DrawRay();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Choca con Player
        //Chequea que tenga LOS
        //si LOS true se enoja GB

        if(other.transform.TryGetComponent<Player>(out Player player))
        {
            if (CheckLOS(player.transform, transform))
            {
                // Player detectado, llamar GB
                //Debug.Log($"<color=green> Gus Detectado </color>");

                if (_myOwner != null)
                    if (!_doDoubt)
                        _myOwner.GetAngry();
                    else
                        _myOwner.GetDoubt(player.transform.position);
            }
        }
    }

    void ScaleObject()
    {
        if (transform.localScale.x >= _maxScale) return;
        //{
        //    _currentTime = 0f;
        //    transform.localScale = dev_ogScale;
        //}

        if(transform.localScale.x <= 1.4)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
        //Escala la esfera
        transform.localScale += transform.localScale * Time.deltaTime * _scaleSpeed;
    }
    

    bool CheckLOS(Transform player, Transform owner)
    {
        var dir = player.position - owner.position;
        //var dist = dir.magnitude;

        dev_dir = dir;
        dev_origin = owner;

        RaycastHit hit;
        if (Physics.Raycast(owner.position, dir, out hit, dir.magnitude,obstructions))
        {      
            // Devuelve false cuando el rayo es cortado por paredes
            //Debug.Log($"<color=green> Rayo cortado por {hit.transform.name} </color>");
            return false;
        }
        else
        {
            //Debug.Log($"<color=red> No se corto el rayo </color>");
            return true;
        }
    }

    // Developer Test

    Vector3 dev_ogScale;
    Transform dev_origin;
    Vector3 dev_dir;
    void Dev_DrawRay()
    {
        if (dev_origin == null || dev_dir == Vector3.zero) return;

        Debug.DrawRay(dev_origin.position, dev_dir);
    }
}
