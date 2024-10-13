using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
//using Unity.Burst.CompilerServices;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    bool inmortal = false;

    [Header("Cosas necesarias")]
    [SerializeField] int _hp;
    Rigidbody _rb;
    Vector3 Spawn = new Vector3(0f, 1f, 0f);
    [SerializeField] private Transform _itemHolder;
    public int nivel = 1;
    AudioSource _audioSource;
    [SerializeField] AudioClip clipLevelUp;

    [SerializeField] Sprite _vidaFull, _vidaMedia, _vidaBaja;
    [SerializeField] Image _vidaUI;
    [SerializeField] GameObject _marcoLvl1, _marcoLvl2, _marcoLvl3;
    [SerializeField] Image[] _marcoColor;
    Color _OGmarcoColor1, _OGmarcoColor2, _OGmarcoColor3 , _actCol1, _actCol2, _actCol3;


    [Header("Movement")]
    [SerializeField] float _speed, _frezzeCD, _slowCD;
    float _salto;
    float _xAxis, _zAxis;
    Vector3 _dir = new();


    public bool underAttack, _traped = false, _canFrezze2 = true;
    public Ghostbuster attacker;


    [Header("Prefabs")]
    [SerializeField] Shadow _shadowPrefab;

    [SerializeField] int _maxShadows;
    public int currentShadows;

    
    float waitFrezze, waitSlow;
    [SerializeField] float opacidadMarco;


    public int scapeSpam;
    [SerializeField]int randomAxis = 0;




    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GameManager.Instance.Player = this;
        GameManager.Instance.ItemHolde = _itemHolder;

        UpdateTerrorFrame();

        _OGmarcoColor1 = _marcoColor[0].color;
        _OGmarcoColor2 = _marcoColor[1].color;
        _OGmarcoColor3 = _marcoColor[2].color;
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CreateShadow();
        }

        #region ControlesF
        if (Input.GetKeyDown(KeyCode.F1))
        {
            RestartScene();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            _audioSource.clip = clipLevelUp;
            _audioSource.Play();
            inmortal = !inmortal;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            _speed = _speed * 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            _speed = _speed * 2f;
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            if (nivel > 1)
            {
                nivel--;
                UpdateTerrorFrame();
            }
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            if (nivel < 3)
            {
                LevelUp();
            }
        } 
        #endregion

        SpriteVidaUpdate();

        if (underAttack)
        {
            if (!_suctionAntiSpam)
                StartCoroutine(SuctionMult());
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                scapeSpam++;
                _suctionMul = 0.5f;

                _marcoColor[0].color = Color.Lerp(_actCol1, _OGmarcoColor1, scapeSpam * 0.1f / 3);
                _marcoColor[1].color = Color.Lerp(_actCol2, _OGmarcoColor2, scapeSpam * 0.1f / 3);
                _marcoColor[2].color = Color.Lerp(_actCol3, _OGmarcoColor3, scapeSpam * 0.1f / 3);

                if (scapeSpam % 2 == 0 || scapeSpam == 0)
                {
                    if (Random.Range(0, 2) == 1)
                        randomAxis = -1;
                    else
                        randomAxis = 1;
                }
            }
        }
        
        else if (!underAttack && scapeSpam != 0)
        {
            randomAxis = 0;
            scapeSpam = 0;
        }
    }
    

    private void FixedUpdate()
    {
        if (_xAxis != 0 || _zAxis != 0)
        {
            Movement(_xAxis, _zAxis);
        }
        if (underAttack) LockedMovement();

        RaycastHit hit;
        if (!Physics.Raycast(transform.position, -transform.up, out hit, 0.2f, LayerMask.GetMask("NoTras"))) _rb.AddForce(-transform.up * _speed, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (underAttack && collision.gameObject.layer == 8) randomAxis *=-1; //8 NoTras
    }

    void Movement(float xAxis, float zAxis)
    {       
        if (underAttack) return;

        #region Comment
        //{
        //    //LockedMovement();
        //    //transform.RotateAround(attacker.transform.position, Vector3.up, lastAxis * 100 * Time.fixedDeltaTime);
        //}
        //else
        //{
        //    _dir = (transform.right * xAxis + transform.forward * zAxis).normalized;

        //    //transform.position += _dir * _speed * Time.fixedDeltaTime;
        //    _rb.position += _dir * _speed *Time.fixedDeltaTime;
        //    //_rb.AddForce(_dir * _speed * Time.fixedDeltaTime, ForceMode.Force);
        //} 
        #endregion

        RaycastHit hitR, hitL, hitF, hitB;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y+1f, transform.position.z);

        if (Physics.SphereCast(pos,0.25f, transform.right, out hitR, 0.52f, LayerMask.GetMask("NoTras")) && xAxis > 0)
        {
            //Debug.Log("<color=ellow> Wall Detected R </color>");
            return;
        }
        if (Physics.SphereCast(pos,0.25f, -transform.right, out hitL, 0.52f, LayerMask.GetMask("NoTras")) && xAxis < 0)
        {
            //Debug.Log("<color=ellow> Wall Detected L </color>");
            return;
        }
        if (Physics.SphereCast(pos,0.25f, transform.forward,out hitF, 0.52f, LayerMask.GetMask("NoTras")) && zAxis > 0)
        {
            //Debug.Log("<color=ellow> Wall Detected F </color>");
            return;
        }
        if (Physics.SphereCast(pos,0.25f, -transform.forward,out hitB, 0.52f, LayerMask.GetMask("NoTras")) && zAxis < 0)
        {
            //Debug.Log("<color=ellow> Wall Detected B </color>");
            return;
        }

        _dir = (transform.right * xAxis + transform.forward * zAxis).normalized;
        _rb.position += _dir * _speed * Time.fixedDeltaTime;
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


    public void UpdateTerrorFrame()
    {
        switch (nivel)
        {
            case 3:
                _marcoLvl1.SetActive(true);
                _marcoLvl2.SetActive(true);
                _marcoLvl3.SetActive(true);
                break;
            case 2:
                _marcoLvl1.SetActive(true);
                _marcoLvl2.SetActive(true);
                _marcoLvl3.SetActive(false);
                break;
            default:
                _marcoLvl1.SetActive(true);
                _marcoLvl2.SetActive(false);
                _marcoLvl3.SetActive(false);
                break;
        }   
    }

    public void ChangeFrameColor(string hexColor, float a = -1)
    {
        Color color;
        ColorUtility.TryParseHtmlString(hexColor, out color);
        if (a < 0) 
            color.a = _OGmarcoColor1.a;
        else
            color.a = a;

        _actCol1 = color;
        _actCol2 = color;
        _actCol3 = color;

        _marcoColor[0].color = _actCol1;
        _marcoColor[1].color = _actCol2;
        _marcoColor[2].color = _actCol3;
    }

    public void ChangeFrameColor(float r, float g, float b, float a)
    {
        Color color = new Color(r, g, b, a);

        _actCol1 = color;
        _actCol2 = color;
        _actCol3 = color;

        _marcoColor[0].color = _actCol1;
        _marcoColor[1].color = _actCol2;
        _marcoColor[2].color = _actCol3;
    }

    public void ResetFrameColor()
    {
        _marcoColor[0].color = _OGmarcoColor1;
        _marcoColor[1].color = _OGmarcoColor2;
        _marcoColor[2].color = _OGmarcoColor3;
    }

    #region Comment
    //public void Marco1()
    //{
    //    _marcoLvl1.SetActive(true);
    //    _marcoLvl2.SetActive(false);
    //    _marcoLvl3.SetActive(false);
    //    //  print("marco1");
    //}
    //public void Marco2()
    //{
    //    _marcoLvl1.SetActive(true);
    //    _marcoLvl2.SetActive(true);
    //    _marcoLvl3.SetActive(false);
    //    //print("marco2");
    //}
    //public void Marco3()
    //{
    //    _marcoLvl1.SetActive(true);
    //    _marcoLvl2.SetActive(true);
    //    _marcoLvl3.SetActive(true);
    //    //print("marco3");
    //}
    #endregion

    void LockedMovement()
    {
        #region Comment
        //RaycastHit hitR, hitL;
        //if (Physics.SphereCast(transform.position, 0.25f, transform.right, out hitR, 0.52f, LayerMask.GetMask("NoTras")))
        //{
        //    //Debug.Log("<color=ellow> Wall Detected R </color>");

        //    return;
        //}
        //if (Physics.SphereCast(transform.position, 0.25f, -transform.right, out hitL, 0.52f, LayerMask.GetMask("NoTras")))
        //{
        //    //Debug.Log("<color=ellow> Wall Detected L </color>");

        //    return;
        //}
        #endregion

        transform.RotateAround(attacker.transform.position, Vector3.up, randomAxis * 200 * Time.fixedDeltaTime);
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

    float _suctionMul = 1;
    bool _suctionAntiSpam = false;

    private IEnumerator SuctionMult()
    {
        _suctionAntiSpam = true;
        while (underAttack)
        {
            var lastSpamnum = scapeSpam;
            yield return new WaitForSeconds(0.5f);
            if (lastSpamnum == scapeSpam) 
                _suctionMul = 10f;
            
        }
        _suctionMul = 0.5f;
        _suctionAntiSpam = false;
    }

    public void ApplyForce(Vector3 direction, float forceMult)
    {
        if (forceMult == 0) _rb.velocity = Vector3.zero;
        _rb.AddForce(direction * forceMult * Time.fixedDeltaTime * _suctionMul, ForceMode.Force);
    }

    void Jump()
    {
        _rb.AddForce(transform.up * _salto, ForceMode.Impulse);
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
        _audioSource.loop = false;
        nivel++;
        _audioSource.clip = clipLevelUp;
        _audioSource.Play();
        UpdateTerrorFrame();

        #region Comment
        //if (_nivel == 2)
        //{
        //    Marco2();
        //}
        //else if (_nivel == 3)
        //{
        //    Marco3();
        //} 
        #endregion
    }

    public void GetDamage()
    {
        if(inmortal) return;
        _hp--;
        if (_hp <=0)
        {
            SceneManager.LoadScene("Derrota");
        }
    }

    #region Comment
    //IEnumerator RythmEscape()
    //{
    //    WaitForSeconds wait = new WaitForSeconds(_rythmTime);
    //    _rythmOn = true;
    //    bool left = false;

    //    while (_rythmOn)
    //    {
    //        yield return wait;
    //        if (left)
    //        {

    //        }
    //        else
    //        {

    //        }

    //    }

    //} 
    #endregion

    void RestartScene()
    {
        SceneManager.LoadScene("Nivel1");
    }

}
