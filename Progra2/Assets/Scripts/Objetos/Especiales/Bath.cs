using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bath : SpecialObject
{
    [SerializeField] GameObject _jumpScarePrefab;
    [SerializeField] Asustable _target;
    [SerializeField] Transform cortina;

    Vector3 _cortinaPos;

    bool inPos = false, trapActive = false;

    public event DelegateType.VoidDelegate ActionActive = delegate { };


    protected override void Awake()
    {
        //CreateTrap();
        _cortinaPos = cortina.position;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject == _target.gameObject && trapActive)
    //    {
    //        inPos = true;
    //    }
    //}

    private void Update()
    {
        if(trapActive && !inPos && Vector3.SqrMagnitude(_target.transform.position - transform.position) < (2f*2f))
        {
            inPos = true;
        }

        if (inPos && cortina != null)
        {
            cortina.position += new Vector3(0,0,1) * 5 * Time.deltaTime; 
        }
    }

    protected override void ObjectAbility(Transform origin)
    {
        _target.GetDoubt(transform.position, -1);

        trapActive = true;
        if (!alreadyActive)
            StartCoroutine(WaitToScare());
    }

    bool alreadyActive = false;

    IEnumerator WaitToScare()
    {
        alreadyActive = true;
        while(inPos == false)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);



        var spookyJumpscare = Instantiate(_jumpScarePrefab, transform.position + new Vector3(0,1,0), transform.rotation);
        //Debug.Log($"<color=#ff00ff> Sonido de calaca tocando trompeta </color>");

        yield return new WaitForSeconds(0.2f);

        ActionActive();

        var dir = (_target.transform.position - transform.position + new Vector3 (0,1,0)).normalized;

        _target.CallRagdollOn(dir);

        _target.CallRagdollOff(1f, true);

        Destroy(cortina.gameObject);

        yield return new WaitForSeconds(0.15f);
        GameManager.Instance.pasoActual = 3;

        Destroy(spookyJumpscare);
        trapActive = false;
        Destroy(_trap);


    }

    public override void CreateTrap()
    {
        base.CreateTrap();

    }
}
