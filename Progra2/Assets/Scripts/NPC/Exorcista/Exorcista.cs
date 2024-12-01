using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Exorcista : NPC
{
    //a
    [Header("Solo para saber")]
    [SerializeField] bool usingNpcAi = true;

    [SerializeField] Animator _anim;

    [SerializeField] Sahumerio _sahumerioPrefab;
    [SerializeField] CruzBehaviour _cruzPrefab;
    [SerializeField] AguaBendita _holyWatherPrefab;
    [SerializeField] LayerMask _objectLayer;

    [SerializeField] float blessingAnimDuration;//Indicar el delay de la corrutina para que cuadren la animacion
    [SerializeField] float placingCrossDuration;//con las acciones
    [SerializeField] float placingSahumerioDuration;

    Sahumerio _lastSahumerio;
    CruzBehaviour _lastCruz;
    AguaBendita _lastAgua;
    AguaBehaviour _myAguaBehavior;

    [SerializeField] Transform _targetPos;
    [SerializeField] int crossLimit;

    //Usado para evitar Spam mientras blessea
    bool _isBlessing = false;
    bool _isPlacingCross = false;
    bool _isPlacingSahumerio = false;

    List<CruzBehaviour> placedCrosses = new();

    DelegateType.VoidDelegate _myNpcMovement = delegate { }, _WatherActions = delegate { } , _CrossAction = delegate { }, _SahumerioActions = delegate { }; 

    bool _lookingActive = false;

    private void Awake()
    {
        StartCoroutine(ChooseAction(5f, HolyObject.Count));
        _myNpcMovement = NormalNpcMovement;
        usingNpcAi = true;
    }

    protected override void Start()
    {
        base.Start();
        _myAguaBehavior = GetComponent<AguaBehaviour>();
    }

    private void Update()
    {
        if (!_AIActive) return;

        _myNpcMovement();
        _WatherActions();
        _CrossAction();
        _SahumerioActions();
       
    }

    
    void NormalNpcMovement()
    {
        //Debug.Log("<color=#0ee874> Update de NPC </color>");

        if (_actualNode == null) Initialize();

        if (_doubt)
            _searchingTimer += Time.deltaTime;

        if (_searchingTimer > 12f)
        {
            StopSearching();
            _anim.SetBool("Walking", true);
            _anim.SetBool("Doubt", false);
        }

        if (_doubt && _inPlace && _waitDoubt >= 2)
        {
            StopSearching();
            _anim.SetBool("Walking", true);
            _anim.SetBool("Search", false);
        }

        if ((!_doubt && !_lookingActive && Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)))
        {
            StartCoroutine(LookAround());
        }
        if (_doubt && Vector3.SqrMagnitude(transform.position - new Vector3(_searchingPos.x, transform.position.y, _searchingPos.z)) <= (_changeNodeDist * _changeNodeDist))
        {
            _agent.speed = 0;

            if (!_inPlace)
            {
                //_inPlace = true;
                //_waitDoubt = 0;

                _anim.SetBool("Doubt", false);
                StartSearching();
                _anim.SetBool("Search", true);
            }
        }
    }

    IEnumerator ChooseAction(float wait, HolyObject lastAction)
    {
        //Debug.Log($"<color=green> Eligiendo Accion </color>");
        //int choose = (int)lastAction;

        yield return new WaitForSeconds(wait);

        int choose = Random.Range(0, (int)HolyObject.Count);

        while(choose == (int)lastAction)
        {
            choose = Random.Range(0, (int)HolyObject.Count);
            yield return null;
        }

        //choose = (int)HolyObject.HolyWather;
        //choose = (int)HolyObject.Cross;
        //choose = (int)HolyObject.Sahumerio;

        if (!_doubt)
        {
            switch ((HolyObject)choose)
            {
                case HolyObject.HolyWather:
                    //Debug.Log($"<color=green> Usar {(HolyObject)choose} </color>");
                    UseHolyWather();
                    break;
                case HolyObject.Cross:
                    //Debug.Log($"<color=green> Usar {(HolyObject)choose} </color>");
                    UseCross();
                    break;
                case HolyObject.Sahumerio:
                    //Debug.Log($"<color=green> Usar {(HolyObject)choose} </color>");
                    UseSahumerio();
                    break;
            }
            usingNpcAi = false;
        }
        else
        {
            //Debug.Log($"<color=red>Exorcista en duda, Siguiente llamado a aaccion en {wait}</color>");
            StartCoroutine(ChooseAction(wait, lastAction));
        }

    }

    public override void GetScared(float a, Transform t = null)
    {
        base.GetScared(a, t);
    }

    public override void GetDoubt(Vector3 pos)
    {
        if (!usingNpcAi)
        {
            Debug.Log($"<color=green>Exorcista realizando accion, ignora doubt</color>");
            return;
        }
        base.GetDoubt(pos);
    }

    void UseHolyWather()
    {
        //Debug.Log($"<color=yellow> Agua Llamada </color>");

        //if (_lastAgua != null)
        //{
        //    Destroy(_lastAgua.gameObject);
        //}

        //_lastAgua = Instantiate(_holyWatherPrefab, transform.position, Quaternion.identity);

        _myNpcMovement = delegate { };

        Collider[] collCercanos = Physics.OverlapSphere(transform.position, 10f, _objectLayer);

        List<Collider> objectsInRoom = new();
        foreach(Collider coll in collCercanos)
        {
            if(coll.transform.TryGetComponent<Pickable>(out var useless) && useless.actualRoom == actualRoom)
                objectsInRoom.Add(coll);
        }

        var target = GetRandomBlessable(objectsInRoom, out  _targetPos);

        _agent.SetDestination(_targetPos.position);

        _WatherActions = CheckIfInBlessRange;

        //ResetActionChoose();

    }

    void CheckIfInBlessRange()
    {
        //Debug.Log("<color=cyan> Update de Agua Bendita </color>");

        if(_targetPos != null && _agent.destination != _targetPos.position)
            _agent.SetDestination(_targetPos.position);

        if(!_isBlessing && Vector3.SqrMagnitude(new Vector3(transform.position.x, 0 , transform.position.z) - 
            new Vector3(_targetPos.position.x, 0, _targetPos.position.z)) <= (1.5f * 1.5f))
        {

            //Debug.Log($"<color=red>Legue a destino, Empezar a bendecir</color>");
            StartCoroutine(DoBlessing(blessingAnimDuration));
        }
    }

    IEnumerator DoBlessing(float blessingAnimDuration)
    {
        //Debug.Log($"<color=yellow>Bendicion Llamada</color>");

        _isBlessing = true;

        //Do anims y efectos
        _agent.speed = 0;

        yield return new WaitForSeconds(blessingAnimDuration);

        //Debug.Log($"<color=green>Bendicion terminada</color>");

        _myAguaBehavior.Bless();


        if (Random.Range(0, 3) == 0)
            Instantiate(_holyWatherPrefab, transform.position, Quaternion.identity);

        _agent.speed = speedNormal;
        
        _isBlessing = false;

        ResetActionChoose();

    }

    IBlessable GetRandomBlessable(List<Collider> posibleObjects, out Transform finalPos)
    {
        var num = Random.Range(0, posibleObjects.Count);

        var finalObject = posibleObjects[num].GetComponent<IBlessable>();
        finalPos = posibleObjects[num].transform;

        if ( finalObject == null)
        {
            finalObject = GetRandomBlessable(posibleObjects, out finalPos);           
        }

        //Debug.Log($"Blessable seleccionado {finalObject}, llendo a {finalPos.name}");
        return finalObject;
    }

    void UseCross()
    {
        //Debug.Log($"<color=yellow> Cruz Llamada </color>");

        _myNpcMovement = delegate { };

        //if (_lastCruz != null)
        //{
        //    Destroy(_lastCruz.gameObject);
        //}

        //_lastCruz = Instantiate(_cruzPrefab, transform.position, Quaternion.identity);

        _targetPos = GetNewNode(_actualNode);
        //Debug.Log($"<color=red> Posicion a la que hay que caminar {_targetPos.position} </color>");


        _agent.SetDestination(_targetPos.position);

        _CrossAction = CheckIfInCrossPos;
        //ResetActionChoose();

    }

    void CheckIfInCrossPos()
    {
        //Debug.Log("<color=#e8a70e> Update de Cruz </color>");
        //Debug.Log($"<color=#e8a70e> Pos {transform.position} Targer {_targetPos.position} </color>");

        if(_targetPos != null && _agent.destination != _targetPos.position)
            _agent.SetDestination(_targetPos.position);

        if (!_isPlacingCross && Vector3.SqrMagnitude(transform.position - _targetPos.position) <= (1.2f * 1.2f))
        { 

            //Debug.Log($"<color=red>Legue a destino, Plantando Cruz</color>");
            StartCoroutine(PlaceCross(placingCrossDuration));
        }
    }

    IEnumerator PlaceCross(float duration)
    {
        //Debug.Log($"<color=yellow>Place cross llamada</color>");

        _isPlacingCross = true;

        //Do anims y efectos
        _agent.speed = 0;

        yield return new WaitForSeconds(duration);

        //Debug.Log($"<color=green>Cruz Plantada</color>");

        //int num = 0;
        //foreach(var cross in placedCrosses)
        //{

        //    Debug.Log($"<color=red> Cruz {cross.name} index {num}</color>");
        //}

        if (placedCrosses.Count >= crossLimit)
        {
            var cross = placedCrosses[0];
            placedCrosses.RemoveAt(0);

            Destroy(cross.gameObject);
        }

        placedCrosses.Add(Instantiate(_cruzPrefab, transform.position, Quaternion.identity));

        //int i = 0;
        //foreach (var cross in placedCrosses)
        //{

        //    Debug.Log($"<color=red> Cruz {cross.name} index {i} </color>");
        //}

        _agent.speed = speedNormal;

        _isPlacingCross = false;

        ResetActionChoose();

    }

    void UseSahumerio()
    {
        //Debug.Log($"<color=yellow> Sahumerio Llamada </color>");

        //if (_lastSahumerio != null)
        //{         
        //    Destroy(_lastSahumerio.gameObject);
        //}

        //_lastSahumerio = Instantiate(_sahumerioPrefab, transform.position, Quaternion.identity);

        if (_lastSahumerio != null && _lastSahumerio.prendido)
        {
            //Debug.Log($"<color=magenta> Ya existe un sahumerio prendido en la casa </color>");
            ResetActionChoose(HolyObject.Sahumerio);
            return;
        }

        _myNpcMovement = delegate { };

        _targetPos = GetNewNode(_actualNode);

        _agent.SetDestination(_targetPos.position);

        _SahumerioActions = CheckIfInSahumerioPos;

        //ResetActionChoose();
    }

    void CheckIfInSahumerioPos()
    {
        //Debug.Log("<color=#e80ec0> Update de SAhumerio </color>");

        if (_targetPos != null && _agent.destination != _targetPos.position)
            _agent.SetDestination(_targetPos.position);

        if (!_isPlacingSahumerio && Vector3.SqrMagnitude(transform.position - _targetPos.position) <= (1.2f * 1.2f))
        {
            //Debug.Log($"<color=red>Legue a destino, Plantando Sahumerio</color>");
            StartCoroutine(PlaceSahumerio(placingSahumerioDuration));
        }
    }

    IEnumerator PlaceSahumerio(float duration)
    {
        //Debug.Log($"<color=yellow>Place Sahumerio llamada</color>");

        _isPlacingSahumerio = true;

        //Do anims y efectos
        _agent.speed = 0;

        yield return new WaitForSeconds(duration);

        //Debug.Log($"<color=green>Sahumerio Plantada</color>");

        if(_lastSahumerio != null && !_lastSahumerio.prendido)
            Destroy(_lastSahumerio.gameObject);

        _lastSahumerio = Instantiate(_sahumerioPrefab, transform.position, Quaternion.identity);

        _agent.speed = speedNormal;

        _isPlacingSahumerio = false;

        ResetActionChoose();

    }

    void ResetActionChoose(HolyObject lastAction = HolyObject.Count)
    {
        _CrossAction = delegate { };
        _WatherActions = delegate { };
        _SahumerioActions = delegate { };

        usingNpcAi = true;

        _myNpcMovement = NormalNpcMovement;

        _actualNode = GetNewNode(_actualNode);
        _agent.SetDestination(_actualNode.position);

        float num = Random.Range(10, 15f);
        StartCoroutine(ChooseAction(num, lastAction));
    }

    private IEnumerator LookAround()
    {
        _lookingActive = true;

        _anim.SetBool("Walking", false);
        _anim.SetBool("Idle", false);
        _anim.SetBool("Search", false);
        _anim.SetBool("Doubt", false);
        _anim.SetBool("InPos", true);


        var _waitRandom = Random.Range(2f, 5f);

        WaitForSeconds wait = new WaitForSeconds(_waitRandom);
        yield return wait;

        _anim.SetBool("Walking", true);
        _anim.SetBool("InPos", false);
        _anim.SetBool("Idle", false);
        _anim.SetBool("Search", false);

        _actualNode = GetNewNode(_actualNode);
        _agent.SetDestination(_actualNode.position);

        _lookingActive = false;
    }

    enum HolyObject
    {
        HolyWather,
        Cross,
        Sahumerio,
        Count
    }
}
