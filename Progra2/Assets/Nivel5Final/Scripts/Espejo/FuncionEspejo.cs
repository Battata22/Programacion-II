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
    [SerializeField] AudioSource source, rotohijo;
    [SerializeField] AudioClip rebote;

    void Start()
    {
        target = GameManager.Instance.Player.gameObject;
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
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

            if (Time.timeScale != 0)
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
            if (Time.timeScale != 0)
            {
                Rotar(1);
            }
        }

    }

    void Rotar(float speedUp)
    {
        transform.eulerAngles += new Vector3(0f, velRot * speedUp, 0f);
    }

    Player playerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<Player>() != null && other.gameObject.GetComponentInParent<Player>().hasMirror == false && agarrado == false)
        {
            agarrado = true;
            playerScript = other.gameObject.GetComponentInParent<Player>();
            other.gameObject.GetComponentInParent<Player>().hasMirror = true;
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

    public void RomperEspejo()
    {
        //hacer que salgan volando las piezas del espejo

        source.clip = rebote;
        source.Play();
        //rotohijo.Play();

        MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();
        mesh[0].enabled = false;
        mesh[1].enabled = false;
        playerScript.hasMirror = false;

        Destroy(gameObject, 0.3f);
        //Invoke("Desactivar", 1f);


        //hacer que hagan respawn
    }

    void Desactivar()
    {
        gameObject.SetActive(false);
    }
}
