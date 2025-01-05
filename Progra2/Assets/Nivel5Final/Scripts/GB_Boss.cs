using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GB_Boss : MonoBehaviour
{
    public float maxHp, actualHp;
    [SerializeField] GameObject parentDefenceWall, player;
    [SerializeField] bool accion = false, defenceWallState = false, animIdle = true;
    [SerializeField] Collider col;
    [SerializeField] Animator _anim;
    ParticleSystem[] _parGens;
    [SerializeField] ParticleSystem _tornadoGen;
    [SerializeField] float rotY;
    [SerializeField] float speedRot;

    void Start()
    {
        actualHp = maxHp;
        col = GetComponent<Collider>();
        _anim = GetComponentInChildren<Animator>();
        _parGens = GetComponentsInChildren<ParticleSystem>();
        _tornadoGen = _parGens[1];
        player = GameManager.Instance.Player.gameObject;
    }


    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            accion = !accion;
        }

        if (accion == true)
        {
            //DefenceWall();
            Aspirado();
        }
    }

    void DefenceWall()
    {
        if (defenceWallState ==  false)
        {
            parentDefenceWall.SetActive(true);
            col.enabled = false;
        }
        else
        {
            parentDefenceWall.SetActive(false);
            col.enabled = true;
        }

        defenceWallState = !defenceWallState;

    }

    void Aspirado()
    {
        if (animIdle == true)
        {
            _anim.SetBool("Idle", false);
            _anim.SetBool("Attacking", true);
            _tornadoGen.Play();

            animIdle = false;
        }

        Vector3 dir = transform.position - GameManager.Instance.Player.transform.position;

        transform.LookAt(new Vector3(GameManager.Instance.Player.transform.position.x, rotY, GameManager.Instance.Player.transform.position.z));
        //transform.LookAt(new Vector3(0f, 0f, GameManager.Instance.Player.transform.position.z));
        //transform.LookAt(new Vector3(GameManager.Instance.Player.transform.position.x, 0f, 0f));
        //transform.LookAt(new Vector3(0f, GameManager.Instance.Player.transform.position.y, 0f));

        //transform.Rotate(dir.x, dir.y, dir.z);


        //transform.localEulerAngles = dir;

        //_anim.SetBool("Idle", true);
        //_anim.SetBool("Attacking", false);
        //animIdle = true;
    }

    public void GetDamage(float damage)
    {
        if (actualHp > 0)
        {
            actualHp -= damage;
        }
        else
        {
            print("lo mataste");
            gameObject.SetActive(false);
        }
    }
}
