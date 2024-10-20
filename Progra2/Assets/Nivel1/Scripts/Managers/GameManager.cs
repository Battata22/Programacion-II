using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

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
            Destroy(this);
        }
  
        //_npc = null;
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

}


