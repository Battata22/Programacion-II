using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GB_Boss : MonoBehaviour
{
    public float maxHp, actualHp;
    [SerializeField] GameObject parentDefenceWall;
    [SerializeField] Player player;
    [SerializeField] bool Atacado = false, defenceWallState = false, animIdle = true, resetWaitAspirado = false;
    [SerializeField] Collider col;
    [SerializeField] Animator _anim;
    ParticleSystem[] _parGens;
    [SerializeField] ParticleSystem _tornadoGen;
    [SerializeField] float rotY, speedRot, _suctionForce, limiteAceSuccion, duracionAspirado, cdApirado, velRotParedesAspirado;
    [SerializeField] GameObject padreParedesPlayer, padreAtaquesIsaac;
    [SerializeField] GameObject[] espejos;
    [SerializeField] GameObject torretasPadre;
    public delegate void EventBossAccion();
    public event EventBossAccion Accion;
    public int fase = 0;

    public static bool isInSafeZone = false;
    float aceSuccion;
    float waitAspirado, waitCdAspirado;

    void Start()
    {
        GameManager.Instance.GB_BossScript = this;
        actualHp = maxHp;
        col = GetComponent<Collider>();
        _anim = GetComponentInChildren<Animator>();
        _parGens = GetComponentsInChildren<ParticleSystem>();
        _tornadoGen = _parGens[1];
        player = GameManager.Instance.Player;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Atacado = !Atacado;
        }
        if(actualHp < maxHp)
        {
            Atacado = true;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            TorretaPadre.laserOn = true;
        }
    }

    void FixedUpdate()
    {
        if (Atacado == true)
        {
            if(actualHp >= (maxHp / 3) * 2)
            {
                print("primer tercio");
                Fase1();
            }
            else if (actualHp >= maxHp / 3)
            {
                print("segundo tercio");
                Fase2();
            }
            else
            {
                print("tercer tercio");
                Fase3();
            }
            //DefenceWall();
            //Aspirado(duracionAspirado);
            //AtaqueIsaac();
            //AtaqueDrones();
        }
        else
        {
            if (animIdle == false)
            {
                _anim.SetBool("Idle", true);
                _anim.SetBool("Attacking", false);
                _tornadoGen.Stop();

                animIdle = true;
            }

            ApagadoIsaac();
        }
    }

    void Fase1()
    {
        fase = 1;
        Aspirado(duracionAspirado);
        //AtaqueIsaac();
    }

    void Fase2()
    {
        fase = 2;
        #region EndFase1
        if (padreParedesPlayer.transform.position.y > -10)
        {
            padreParedesPlayer.transform.position -= new Vector3(0f, 20 * Time.deltaTime, 0f);
        }
        if (animIdle == false)
        {
            _anim.SetBool("Idle", true);
            _anim.SetBool("Attacking", false);
            _tornadoGen.Stop();

            animIdle = true;
        }
        #endregion
        SpawnEspejos();
        AtaqueDrones();
        DefenceWall();
    }
    void Fase3()
    {
        fase = 3;
        #region EndFase2
        parentDefenceWall.SetActive(false);
        col.enabled = true; 
        #endregion
        AtaqueIsaac();
        RevivirTorretas();

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

        //defenceWallState = !defenceWallState;



    }

    void Aspirado(float dur)
    {
        if (resetWaitAspirado == false)
        {
            waitAspirado = 0;
            resetWaitAspirado = true;
        }

        waitAspirado += Time.deltaTime;

        if (waitAspirado <= dur)
        {

            if (animIdle == true)
            {
                _anim.SetBool("Idle", false);
                _anim.SetBool("Attacking", true);
                _tornadoGen.Play();

                animIdle = false;
            }

            if (padreParedesPlayer.transform.position.y < 0)
            {
                padreParedesPlayer.transform.position += new Vector3(0f, 10 * Time.deltaTime, 0f);
            }


            transform.LookAt(new Vector3(player.transform.position.x, rotY, player.transform.position.z));

            Vector3 direction = player.transform.position - transform.position;

            padreParedesPlayer.transform.localEulerAngles += new Vector3(0f, velRotParedesAspirado * Time.deltaTime, 0f);

            //succion
            if (isInSafeZone == false)
            {
                player.ApplyForce(-direction, _suctionForce * Time.fixedDeltaTime * aceSuccion);

                if (aceSuccion < limiteAceSuccion)
                {
                    aceSuccion += Time.deltaTime * 0.5f;
                }
            }
            else
            {
                print("safezone");
            }



            #region Comment
            //Vector3 dir = transform.position - GameManager.Instance.Player.transform.position;
            //transform.LookAt(new Vector3(player.transform.position.x, -player.transform.position.y, player.transform.position.z));

            //transform.LookAt(new Vector3(0f, 0f, GameManager.Instance.Player.transform.position.z));
            //transform.LookAt(new Vector3(GameManager.Instance.Player.transform.position.x, 0f, 0f));
            //transform.LookAt(new Vector3(0f, GameManager.Instance.Player.transform.position.y, 0f));

            //transform.Rotate(dir.x, dir.y, dir.z);


            //transform.localEulerAngles = dir;

            //_anim.SetBool("Idle", true);
            //_anim.SetBool("Attacking", false);
            //animIdle = true; 
            #endregion
        }
        else
        {
            if (padreParedesPlayer.transform.position.y > -10)
            {
                padreParedesPlayer.transform.position -= new Vector3(0f, 20 * Time.deltaTime, 0f);
            }
            else
            {
                waitCdAspirado += Time.deltaTime;
                if (animIdle == false)
                {
                    _anim.SetBool("Idle", true);
                    _anim.SetBool("Attacking", false);
                    _tornadoGen.Stop();

                    animIdle = true;
                }
                if (waitCdAspirado >= cdApirado)
                {
                    waitAspirado = 0;
                    waitCdAspirado = 0;
                    resetWaitAspirado = false;
                }

                //Atacado = false;
            }

            //StartCoroutine(BajarParedes());
            //waitAspirado = 0;
            //resetWaitAspirado = false;
            //accion = false;
        }




    }

    void SpawnEspejos()
    {
        for (int i = 0; i < espejos.Length; i++)
        {
            print(i);
            espejos[i].SetActive(true);
        }
    }

    IEnumerator BajarParedes()
    {
        float segPasados = 0;
        if (padreParedesPlayer.transform.position.y > -10 || segPasados >= 10)
        {
            print("test1");
            padreParedesPlayer.transform.position -= new Vector3(0f, 2 * Time.deltaTime, 0f);
            segPasados += Time.deltaTime;
        }
        else
        {
            print("test2");
            yield return null;
        }


    }

    bool isaac = false;
    void AtaqueIsaac()
    {
        if (isaac == false)
        {
            padreAtaquesIsaac.SetActive(true);
            isaac = true;
        }

    }

    void ApagadoIsaac()
    {
        if (isaac == true)
        {
            padreAtaquesIsaac.SetActive(false);
            isaac = false;
        }
    }

    [SerializeField] GameObject drone;
    public static bool isDroneAlive = false;
    void AtaqueDrones()
    {
        //invocar drones, que los drones vayan hacia player, una vez encima empiecen a tirar cosas, que puedas romperlos, (plus, que se acomoden segun la cantidad).
        //Instantiate(drone, new Vector3(transform.position.x, transform.position.y + 20, transform.position.z), Quaternion.identity);
        if (isDroneAlive == false)
        {
            Instantiate(drone, transform.position, Quaternion.identity);
        }

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

    void RevivirTorretas()
    {
        TorretaPadre.laserOn = true;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

}
