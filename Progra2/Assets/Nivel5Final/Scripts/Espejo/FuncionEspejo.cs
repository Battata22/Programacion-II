using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionEspejo : MonoBehaviour
{
    [SerializeField] bool arreglos = false, pausa = false;
    [SerializeField] GameObject target;
    [SerializeField] float velRot,alturaSobreGus, rotAmpliada;
    [SerializeField] Animator animator;
    public bool agarrado = false;

    void Start()
    {
        target = GameManager.Instance.Player.gameObject;
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (GameManager.Instance.GB_BossScript.fase != 2)
        {
            Destroy(gameObject);
        }

        if (agarrado == true)
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y + alturaSobreGus, target.transform.position.z);

            if (pausa == false)
            {
                Rotar(rotAmpliada);
            }

            #region Aggare
            if (arreglos == false)
            {
                Agarrado();
            } 
            #endregion
        }
        else
        {
            if (pausa == false)
            {
                Rotar(1);
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pausa = !pausa;
        }
    }

    void Rotar(float speedUp)
    {
        transform.eulerAngles += new Vector3(0f, velRot * speedUp, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<Player>() != null && agarrado == false)
        {
            agarrado = true;
        }

        if(other.gameObject.GetComponent<CargaBehaviour>() != null && agarrado == true)
        {
            //devolver bomba
        } 

        if(other.gameObject.GetComponent<BallaBehaivour>() != null)
        {
            Destroy(other.gameObject);
        }

    }

    void Agarrado()
    {
        animator.enabled = false;
        transform.eulerAngles = new Vector3(0f, 0f, 0f);

        //hacerlo semi transparente

        arreglos = true;
    }
}
