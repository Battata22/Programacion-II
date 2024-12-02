using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UltimateLiving : SpecialObject
{
    //[SerializeField] PhysicMaterial phyParedes, phyPiso;
    [SerializeField] float radio, fuerzaTorque, tiempoInAir;
    [SerializeField] LayerMask maskUlti, maskNPC;
    [SerializeField] Image ultState;

    Player _player;

    bool active = false, used = false;
    float countDown, waitScared;

    bool trapCreated = false;

    private void Start()
    {
        GameManager.Instance.ActivateWinCondition += CreateTrap;
    }

    private void Update()
    {
        if(!trapCreated && GameManager.Instance.terrorBar.value >= GameManager.Instance.terrorBar.maxValue) 
        {
            CreateTrap();
        }
    }

    public override void CreateTrap()
    {
        base.CreateTrap();
        trapCrated = true;
        GameManager.Instance.ActivateWinCondition -= CreateTrap;
    }

    protected override void ObjectAbility(Transform origin)
    {
        //Ultimate copiada del player
        try
        {
            StartCoroutine(DoUltimate(tiempoInAir));
        }
        catch
        {
            Invoke("CallWin", 1f);
        }
    }

    IEnumerator DoUltimate(float newWait)
    {

        var wait = new WaitForSeconds(newWait);

        Levitar();

        yield return wait;

        //Caida();
    }

    void Levitar()
    {
        Invoke("CallWin", tiempoInAir + 1);
        Invoke("Caida", tiempoInAir);

        active = true;

        used = true;

        countDown = 0;

        Collider[] collidersNPC = Physics.OverlapSphere(transform.position, radio, maskNPC);
        foreach (var collider in collidersNPC)
        {

            if (collider.GetComponent<Asustable>() != null)
            {
                Asustable asustableScript = collider.GetComponent<Asustable>();
                asustableScript.GetDoubt(collider.transform.position);
            }

        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, radio, maskUlti);

        print(colliders.Length);
        foreach (var collider in colliders)
        {

            if (collider.GetComponent<Rigidbody>() != null)
            {
                Pickable pickScript = collider.GetComponent<Pickable>();
                if (pickScript._pickedUp == false)
                {
                    Rigidbody rb = collider.GetComponent<Rigidbody>();
                    rb.useGravity = false;
                    rb.constraints = RigidbodyConstraints.None;
                    rb.AddForce(transform.up * fuerzaTorque);
                    rb.AddTorque(transform.right * fuerzaTorque);
                }

                if (collider.GetComponent<NavMeshObstacle>() != null)
                {
                    NavMeshObstacle obst = collider.GetComponent<NavMeshObstacle>();
                    obst.enabled = false;
                }

            }

        }

    }

    void Caida()
    {
        //ultState.color = Color.red;
        waitScared = 0;

        Collider[] collidersNPC = Physics.OverlapSphere(transform.position, radio, maskNPC);



        Collider[] colliders = Physics.OverlapSphere(transform.position, radio, maskUlti);

        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Rigidbody>() != null)
            {
                Pickable pickScript = collider.GetComponent<Pickable>();
                if (pickScript._pickedUp == false)
                {
                    Rigidbody rb = collider.GetComponent<Rigidbody>();
                    rb.useGravity = true;
                    rb.AddForce(transform.up * -fuerzaTorque);
                }


                if (collider.GetComponent<NavMeshObstacle>() != null)
                {
                    NavMeshObstacle obst = collider.GetComponent<NavMeshObstacle>();
                    obst.enabled = true;
                }

            }

        }

        foreach (var collider in collidersNPC)
        {
            if (collider.GetComponent<Asustable>() != null)
            {
                if (collider.GetComponent<Asustable>() != null)
                {
                    Asustable asustableScript = collider.GetComponent<Asustable>();
                    asustableScript.GetScared(1f);

                }
            }

        }

        active = false;

        Invoke("CallWin", 1f);
    }

    void CallWin()
    {
        GameManager.Instance.CompleteLevel();
    }

    private void OnDestroy()
    {
        GameManager.Instance.ActivateWinCondition -= CreateTrap;

    }
}
