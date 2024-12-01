using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {           
            //Destroy(this);
            Destroy(this.gameObject);
        }

        //_npc = null;
        terrorBar = null;
    }
    #endregion
    
    

    private Player _player;
    public Player Player 
    {
        get { return _player; } 
        set { _player = value; }
    }

    [SerializeField] private List<NPC> _npc;
    public List<NPC> Npc
    {
        get { return _npc; }
        set { _npc = value; }        
    }

    public List<Ghostbuster> Gb;

    public List<Luces> _lights = new ();

    public List<Transform> AiNodes;
    public List<Transform> activeNodes;
    public AINodeManager AINodeManager;

    [SerializeField] private Cam _camera;
    public Cam Camera 
    {
        get { return _camera; } 
        set {  _camera = value; } 
    }

    private Transform _itemHolde;
    public Transform ItemHolde
    {
        get { return _itemHolde;}
        set { _itemHolde = value;}
    }

    private HandState _handState;
    public HandState HandState 
    { 
        get { return _handState; } 
        set { _handState = value; }
    }

    private MasterNivel1 _master1;
    public MasterNivel1 Master1
    {
        get { return _master1; }
        set { _master1 = value; }
    }

    public MasterNivel2 _master2;

    private Animator _animPuerta;
    public Animator AnimPuerta
    {
        get { return _animPuerta; }
        set { _animPuerta = value; }
    }

    private CamaraCanvas _camGBCanvas;
    public CamaraCanvas CamGBCanvas
    {
        get { return _camGBCanvas; }
        set { _camGBCanvas = value; }
    }


    public LayerMask DropLayers;

    public LayerMask NpcLayers;

    public GameObject TrailGen;

    public GameObject ParticleObj;
    //public VolumeManager VolumeManager;

    public TutorialManager Tutorial;

    public AudioMixerGroup AudioGroupMusic, AudioGroupSfx;

    public AudioClip choque;

    public Shadow ShadowPrefab;

    public LvlPhase currentLvlPhase = LvlPhase.trap;
    public enum LvlPhase
    {
        trap,
        gameplay
    }

    public List<PlayerTrap> PlayerTraps;

    public AudioClip bonk;

    public AudioClip ghostSong;

    public Slider barraGanar;

    public bool canActiveTuto = true;

    public CreatePlayerTrap createPlayerTrap;

    //public List<GameObject> visualesLineas1 = new List<GameObject>();

    public GameObject firstVisualLinea;

    public GameObject imagenBlanco, imagenRojo;

    public AudioSource Drop;

    [SerializeField] private Slider _terrorBar;
    public Slider terrorBar
    {
        get{ return _terrorBar; }
        set 
        {
            //Debug.Log($"<color=green> Barra de terror Seteada {value}</color>");
            _terrorBar = value; 
            //DeactivateBar();
        }
    }

    public Rociadores Rociadores;

    public AudioClip jumpscareBoo;


    //De momento GameManager se va a encargar de Ganar, porque si, porque puedo y lo valgo
    private bool _activateWinCondition = false;
    public event DelegateType.VoidDelegate ActivateWinCondition = delegate { };

    //private IEnumerator Start()
    //{
    //    yield return new WaitForEndOfFrame();

    //    Debug.Log($"<color=yellow>terrorBar {terrorBar.name} condicion ya activada {_activateWinCondition}</color>");
    //}

    private void Update()
    {
        //primera parte del if momentaneamente

        if (terrorBar != null && !_activateWinCondition && terrorBar.value >= terrorBar.maxValue)
        {
            ActivateWinCondition();
            _activateWinCondition = true;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            var coso = GetComponent<PhaseManager>();
            coso.CallTrapPhase();

            SceneManager.LoadScene("Nivel2");
        }
        if(Input.GetKeyDown(KeyCode.K)) 
        {
            var coso = GetComponent<PhaseManager>();
            coso.CallTrapPhase();

            SceneManager.LoadScene("Nivel1");
        }
    }

    public void CompleteLevel()
    {

        var coso = GetComponent<PhaseManager>();
        coso.CallTrapPhase();

        if (SceneManager.GetActiveScene().name == "Nivel1")
        {
            //SceneManager.LoadScene("Nivel2");          

            SceneManager.LoadScene("Niveles");
        }
        else
        {
            SceneManager.LoadScene("Victoria");
        }
    }

    public void ActivateTerrorBar()
    {
        _terrorBar.gameObject.SetActive(true);
        //a

        _terrorBar.value = 0f;
    }

    private void OnDestroy()
    {
        
    }

    public int pasoActual;

}


