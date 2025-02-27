using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ChildScript : NPC
{

    //a

    //que hace el pendejito?
    //sigue al player
    //se asusta si otro se asusta cerca
    //cuando se asusta llora?
    //cuando llora distrae a alguno de sus padres?
    //ragdoll lo hace llorar?

    //si hay un pendejito en area, chocamiento no asusta?
    //

    //pendejito desactiva trampas?
    //

    //gb se da cuenta que el pendejito te sigue y tira escaneres cerca?
    //"Hecho", GbChildDetector hace dudar a alguien si ve al pendejo caminando

    //cosas que poner por inspector
    [Header("<color=green>Pendejito</color>")]
    [SerializeField] Asustable[] _parents;
    [SerializeField] LayerMask _npcMask;
    [SerializeField] float _scareRange;
    [SerializeField] float _cryDuration;

    //bools
    //bool _chasingPlayer = false;
    //bool _crying = false;

    [SerializeField] ChildState _childState;

    [Header("Deteccion de Gus")]
    [SerializeField] ChildGusDetector _triggerPref;
    ChildGusDetector _myGusDetector;
    [SerializeField, Tooltip("Solo aplica para deteccion de npc adultos")] LayerMask obstructions;

    [SerializeField] Transform _detectorOrigin;
    public Transform detectorOrigin
    {
        get { return _detectorOrigin; }
    }

    public bool ChasingPlayer
    {
        get
        {
            //return _chasingPlayer;
            if(_childState == ChildState.ChasingPlayer) return true;
            else return false;
        }
        set { }
    }

    //delegates
    DelegateType.VoidDelegate DoCheckScare = delegate { };
    //event DelegateType.VoidDelegateTrans OnCryEnter = delegate { };

    private void Awake()
    {
        DoCheckScare = CheckScare;
    }

    protected override void Start()
    {
        base.Start();

        _myGusDetector = Instantiate(_triggerPref, Vector3.zero, Quaternion.identity);
        _myGusDetector.Initialize(this);

        gameObject.SetActive(false);

        //SetNewDestination();
    }

    private void OnEnable()
    {
        if (_myGusDetector != null)
            _myGusDetector.gameObject.SetActive(true);

        if (_actualNode == null)
            SetNewDestination();
    }

    private void OnDisable()
    {
        if (_myGusDetector != null)
            _myGusDetector.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!_AIActive) return;

        if (_agent.enabled && (!_doubt && Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)))
        {
            //Debug.Log("<color=#26c5f0> LLege al destino </color>");
            SetNewDestination(_actualNode);

        }


        //if (Input.GetKey(KeyCode.LeftAlt))
        //    if (Input.GetKey(KeyCode.Alpha0))
        //        StartChase();



        DoCheckScare();
        DoCheckToy();
    }

    public override void GetScared(float a,int roomIndex, Transform t = null)
    {
        //base.GetScared(a, t);
    }

    public override void GetDoubt(Vector3 pos, int g)
    {
        //base.GetDoubt(pos);
    }

    void CheckScare()
    {
        //Debug.Log($"<color=#aaf4f1>Buscando npcs asustados</color>");

        Collider[] allInRange = Physics.OverlapSphere(transform.position, _scareRange, _npcMask);
        //List<Asustable> npcInRange = new();

        foreach(var npc in allInRange)
        {
            if (npc.TryGetComponent<Asustable>(out var adultos) && adultos.scared)
            {
                //Debug.Log($"<color=#aaf4f1> {npc.name} esta asustado</color>r>");
                if (CheckLOS(adultos.transform, transform))
                    Cry();
            }
            //else
                //Debug.Log($"<color=#aaf4f1>{npc.name} no esta asustado</color>");

        }
    }



    bool CheckLOS(Transform parent, Transform owner)
    {
        var dir = parent.position - owner.position;
        //var dist = dir.magnitude;

        RaycastHit hit;
        if (Physics.Raycast(owner.position + new Vector3(0,0.2f,0), dir, out hit, dir.magnitude, obstructions))
        {
            // Devuelve false cuando el rayo es cortado por paredes
            //Debug.Log($"<color=green> Rayo cortado por {hit.transform.name} </color>");
            return false;
        }
        else
        {
            //Debug.Log($"<color=red> No se corto el rayo </color>");
            return true;
        }
    }

    void Cry()
    {
        DoCheckScare = delegate { };

        _myGusDetector.active = false;

        //Debug.Log($"<color=#e7aaf4>ahhhh ahhhhh bua bua *bebe llorando*</color>");
        _agent.speed = 0f;

        //_crying = true;
        //_chasingPlayer = false;
        _childState = ChildState.Crying;

        //call parents

        CallParent();

        OnChildCry();
        //OnCryEnter(transform);
    }

    public void StopCry()
    {
        Debug.Log($"<color=#e7aaf4>Brutal</color>");//https://cdn.eldeforma.com/wp-content/uploads/2020/07/brutal-meme.jpg
        _agent.speed = speedNormal;

        //_crying = false;
        _childState = ChildState.None;

        DoCheckScare += CheckScare;

        SetNewDestination();
        _myGusDetector.active = true;
    }

    IEnumerator ChasePlayer()
    {
        //Debug.Log($"<color=#f4aac2></color>");

        while (_childState == ChildState.ChasingPlayer)
        {
            var player = GameManager.Instance.Player.transform;
            _agent.SetDestination(player.position);

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StartChase()
    {
        //if (_crying) return;
        //if (_chasingPlayer) return;
        if (_childState == ChildState.ChasingPlayer) return;

        //Debug.Log($"<color=green>Arranca a perseguir al player la ctm</color>");
        //Debug.Log($"<color=green>{GameManager.Instance.Player.name} {GameManager.Instance.Player.transform.position}</color>");

        //_chasingPlayer = true;
        _childState = ChildState.ChasingPlayer;

        StartCoroutine(ChasePlayer());

        _agent.speed = speedNormal;
    }

    void CallParent()
    {    
        float lastDist = -1;
        int _closeParentIndex = 0;

        for (int i = 0; i < _parents.Length; i++)
        {
            float tempDist = (transform.position - _parents[i].transform.position).sqrMagnitude;

            if(lastDist == -1)
            {
                lastDist = tempDist;
                _closeParentIndex = i;
            }

            if (tempDist < lastDist)
            {
                lastDist = tempDist;
                _closeParentIndex = i;
            }
        }

        _parents[_closeParentIndex].AddBabyCryingList(this);
    }

    public override void SetNewDestination(Transform lastDest = null)
    {

        if (lastDest != null)
            _actualNode = GetNewNode(lastDest);
        else
            _actualNode = GetNewNode();

        _agent.SetDestination(_actualNode.position);

        _agent.speed = speedNormal;
        //Debug.Log($"<color=cyan> Nuevo Destino Elegido {_actualNode.name} </color>");
    }

    //Detector de juguetes

    //si juguete suena en area el pendejo se acerca al juguete y se pone a reir? si, no muy coerente pero bueno, hay que hacer algo
    DelegateType.VoidDelegate DoCheckToy = delegate { };

    [SerializeField] float _detecToyRadius;
    [SerializeField] float _playingDuration;
    Transform _toyPos;

    public event DelegateType.VoidDelegate OnChildLaugh = delegate { };
    public event DelegateType.VoidDelegate OnChildCry = delegate { };

    public void GoToToy(Transform newToy)
    {
        if (_childState == ChildState.Crying) return;
        if(Vector3.SqrMagnitude(newToy.position - transform.position) > (_detecToyRadius * _detecToyRadius))
        {
            Debug.Log($"<color=red>Juguete:{newToy.name} muy lejos </color>");
            return;
        }
        _childState = ChildState.ChasingToy;

        _toyPos = newToy;

        _agent.speed = speedNormal;
        _agent.SetDestination(_toyPos.position);

        DoCheckToy += CheckToyDist;
    }

    void CheckToyDist()
    {
        if(_childState!=ChildState.Playing && Vector3.SqrMagnitude(_toyPos.position - transform.position) < (_detecToyRadius * _detecToyRadius * 0.3f))//_changeNodeDist * _changeNodeDist * 2
        {
            Debug.Log("<color=green>JAJA jugueye</color>");

            OnChildLaugh();
            StartCoroutine(Laugh());
        }
    }

    IEnumerator Laugh()
    {
        DoCheckToy = delegate { };

        _myGusDetector.active = false;
        var duration = 0f;
        _childState = ChildState.Playing;

        while (duration < _playingDuration && _childState == ChildState.Playing)
        {
            if(duration % 2 == 0)
            {
                Debug.Log("<color=#ffff00>BallonBoy.sfx</color>");
            }

            duration += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("<color=#ffff00>Ya basta chicos</color>");
        
        _childState = ChildState.None;
        SetNewDestination();
        _myGusDetector.active = true;

    }

    public enum ChildState
    {
        ChasingPlayer,
        Crying,
        ChasingToy,
        Playing,
        None
    }
}
