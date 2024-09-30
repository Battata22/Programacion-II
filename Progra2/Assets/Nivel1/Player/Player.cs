using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    int random1;

    [Header("Cosas necesarias")]
    [SerializeField] int _hp;
    Rigidbody _rb;
    Vector3 Spawn = new Vector3(0f, 1f, 0f);
    [SerializeField] private Transform _itemHolder;
    public int _nivel = 1;
    AudioSource _audioSource;
    [SerializeField] AudioClip clipLevelUp;

    [SerializeField] Sprite _vidaFull, _vidaMedia, _vidaBaja;
    [SerializeField] Image _vidaUI;
    [SerializeField] GameObject _marcoLvl1, _marcoLvl2, _marcoLvl3;


    [Header("Movement")]
    [SerializeField] float _speed, _frezzeCD, _slowCD;
    float salto;
    float _xAxis, _zAxis;
    Vector3 _dir = new();

    //Attack logic
    public bool underAttack, _traped = false, _canFrezze2 = true;
    public Ghostbuster attacker;

    [Header("Prefabs")]
    [SerializeField] Shadow _shadowPrefab;

    [SerializeField] int _maxShadows;
    public int currentShadows;
    float waitFrezze, waitSlow;
    [SerializeField] float opacidadMarco;




    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GameManager.Instance.Player = this;
        GameManager.Instance.ItemHolde = _itemHolder;
        Marco1();
    }

    private void Update()
    {
        if (_traped == true)
        {
            waitFrezze += Time.deltaTime;
            waitSlow += Time.deltaTime;
            LockedTrampa();
        }

        _xAxis = Input.GetAxisRaw("Horizontal");
        _zAxis = Input.GetAxisRaw("Vertical");

        LifeSaver(transform.position.y);
        if (Input.GetKeyDown(KeyCode.LeftShift)) CreateShadow();
        if (Input.GetKeyDown(KeyCode.F)) MakeNoise();//posible cambio a E si no hay nada con lo que interactuar

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_nivel > 1)
            {
                _nivel--;
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (_nivel < 3)
            {
                LevelUp();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _speed = _speed * 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _speed = _speed * 2f;
        }

        SpriteVidaUpdate();

    }

    private void FixedUpdate()
    {
        if (_xAxis != 0 || _zAxis != 0)
        {
            Movement(_xAxis, _zAxis);
        }
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, -transform.up, out hit, 0.2f, LayerMask.GetMask("NoTras"))) _rb.AddForce(-transform.up * _speed, ForceMode.Force);
    }

    void Movement(float xAxis, float zAxis)
    {
        if (underAttack)
        {
            LockedMovement();
        }
        else
        {
            _dir = (transform.right * xAxis + transform.forward * zAxis).normalized;

            //transform.position += _dir * _speed * Time.fixedDeltaTime;
            _rb.position += _dir * _speed *Time.fixedDeltaTime;
            //_rb.AddForce(_dir * _speed * Time.fixedDeltaTime, ForceMode.Force);
        }
    }

    void SpriteVidaUpdate()
    {
        if (_hp == 3)
        {
            _vidaUI.sprite = _vidaFull;
        }
        else if (_hp == 2)
        {
            _vidaUI.sprite = _vidaMedia;
        }
        else if (_hp == 1)
        {
            _vidaUI.sprite = _vidaBaja;
        }
    }


    public void Marco1()
    {
        _marcoLvl1.SetActive(true);
        _marcoLvl2.SetActive(false);
        _marcoLvl3.SetActive(false);
        print("marco1");
    }
    public void Marco2()
    {
        _marcoLvl1.SetActive(true);
        _marcoLvl2.SetActive(true);
        _marcoLvl3.SetActive(false);
        print("marco2");
    }
    public void Marco3()
    {
        _marcoLvl1.SetActive(true);
        _marcoLvl2.SetActive(true);
        _marcoLvl3.SetActive(true);
        print("marco3");
    }

    void LockedMovement()
    {
        transform.RotateAround(attacker.transform.position, Vector3.up, _xAxis * 100 * Time.fixedDeltaTime);
    }

    void LockedTrampa()
    {
        if (_canFrezze2 == true)
        {
            _speed = 0;
            if (waitFrezze >= _frezzeCD)
            {
                _speed = 2;
                waitSlow = 0;
                _canFrezze2 = false;
            }
        }
        else
        {
            if (waitSlow >= _slowCD)
            {
                _speed = 5;
                _traped = false;
                _canFrezze2 = true;
                waitFrezze = 0;
                waitSlow = 0;
            }

        }

    }

    public void ApplyForce(Vector3 direction, float forceMult)
    {
        if (forceMult == 0) _rb.velocity = Vector3.zero;
        _rb.AddForce(direction * forceMult * Time.fixedDeltaTime, ForceMode.Force);
    }

    void Jump()
    {
        _rb.AddForce(transform.up * salto, ForceMode.Impulse);
    }

    void LifeSaver(float _dis)
    {
        if(_dis <= -15)
        {
            transform.position = Spawn;
        }
    }

    void CreateShadow()
    {
        if (currentShadows >= _maxShadows) return;
        Shadow newShadow = Instantiate(_shadowPrefab, transform.position, Quaternion.identity);
        newShadow.Initialize(this);
        currentShadows++;
    }

    void MakeNoise()
    {
        Debug.Log("BOO");
    }

    public void LevelUp()
    {
        _nivel++;
        _audioSource.clip = clipLevelUp;
        _audioSource.Play();
        if (_nivel == 2)
        {
            Marco2();
        }
        else if (_nivel == 3)
        {
            Marco3();
        }
    }

    public void GetDamage()
    {
        Debug.Log("<color=#6916c1> Auch </color>");
        _hp--;
        if (_hp <=0)
        {
            //Debug.Log("<color=#6916c1> IM Dead ... /n wait a minute </color> ");
            SceneManager.LoadScene("Derrota");
        }
    }
    
}
