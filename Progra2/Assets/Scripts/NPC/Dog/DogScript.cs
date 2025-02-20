using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DogScript : NPC
{
    [Header("<color=#c5a53f> Capibara, coconut doggy </color>")]
    [SerializeField] Player _target;
    [SerializeField] AudioClip _clipLadrido;
    [SerializeField] GameObject _areDuda;
    bool playing = false, areaSpawn = false;

    //Rework
    //Ladrido
    [SerializeField] float _doubtTime;
    [SerializeField, Tooltip("<color=green> How much time is needed to bark once in doubt state </color>")]
    float _mercyTime;
    [SerializeField] DogAlertIcon _alertIcon;

    float _timeToBark, _lastDoubt, _timeToEndDoubt;

    DelegateType.VoidDelegate InputCheck = delegate { };

    [SerializeField] float _chaseDuration;
    [SerializeField, Tooltip("firs Min Time, sec Max Time")] float[] _timeBtChase = new float[2];

    //Juguetes
    [SerializeField] Transform _toy;
    [SerializeField] float _playDuration;
    float _playingTime;
    bool _chassingToy = false , _playing = false;

    DelegateType.VoidDelegate DoPlayCheck = delegate { };

    //Ragdolleador
    [SerializeField] DogRagdollHitbox _ragHitbox;

    protected override void Start()
    {
        base.Start();
        _ragHitbox = GetComponentInChildren<DogRagdollHitbox>();
        _alertIcon = GetComponentInChildren<DogAlertIcon>();
        _alertIcon.SetMaxTimers(_doubtTime,_mercyTime);

        StartCoroutine(StopChase());
    }

    private void Update()
    {
        if (!_AIActive) return;
        if (_target == null) _target = GameManager.Instance.Player;
        if (_actualNode == null) Initialize();

        if ((!_doubt && Vector3.SqrMagnitude(transform.position - _actualNode.position) <= (_changeNodeDist * _changeNodeDist)))
        {
            //Debug.Log("<color=#26c5f0> LLege al destino </color>");

            _actualNode = GetNewNode(_actualNode);

            _agent.SetDestination(_actualNode.position);
        }

        if (playing && areaSpawn == false)
        {
            Instantiate(_areDuda, transform.position, Quaternion.identity);
            areaSpawn = true;
        }

        InputCheck();
        DoPlayCheck();

        //if (_doubt)
        //    _searchingTimer += Time.deltaTime;
        //if (_searchingTimer > 12f) StopSearching();
        //if (_doubt && _inPlace && _waitDoubt >= 2)
        //{
        //    StopSearching();
        //}
    }

    public void Ladrido()
    {
        playing = true;
        _audioSource.clip = _clipLadrido;
        _audioSource.Play();

        _alertIcon.active = false;
    }

    public void StopLadrido()
    {
        areaSpawn = false;
        playing = false;
        _audioSource.Stop();

        EndAlert();
    }

    public override void GetDoubt(Vector3 pos, int g)
    {
        //base.GetDoubt(pos);
    }

    [SerializeField] bool _alert = false;

    public void StartAlert()
    {
        if (_chassingToy) return;
        _alert = true;
        //Dejar quieto al perro
        //Hacer sonido para llamar atencion del player
        //Arrancar a dudar
        //Mientras duda si te moves, ladra
        //Si no ladro en X tiempo dejar de dudar

        //Debug.Log($"<color=green>ENTRADA A START ALETR</color>");

        _agent.speed = 0;
        _alertIcon.active = true;

        //Debug.Log($"<color=green> INPUT CHECK ACTIVADO </color>");

        //CheckPlayerInput
        InputCheck = CheckPlayerInput;

    }

    void CheckPlayerInput()
    {
        _timeToEndDoubt += Time.deltaTime;
        if(_timeToEndDoubt > _doubtTime)
        {
            //Debug.Log($"<color=green>SE TERMINO EL TIEMPO DE ALERTA</color>");

            EndAlert();
            return;
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            _timeToBark += Time.deltaTime;
        }

        if (!playing && _timeToBark > _mercyTime)
        {
            //Debug.Log($"<color=red> CAGASTE </color>");

            Ladrido();
        }

        _alertIcon.timer = _timeToEndDoubt;
        _alertIcon.charge = _timeToBark;

    }

    void EndAlert()
    {

        InputCheck = delegate { };
        _timeToBark = 0;
        _timeToEndDoubt = 0;
        _lastDoubt = Time.time;

        _alertIcon.active = false;
        _alert = false;

        _agent.speed = speedNormal;

        StartCoroutine(StopChase());
    }

    //Toy shit

    public void StartChaseToy(Transform newToy)
    {
        if (_playing) return;
        EndAlert();
        StartCoroutine(StopChase());
        //if(_chassingToy) return;

        Debug.Log($"<color=blue> Busca la pelota </color>");

        _chassingToy = true;
        _toy = newToy;

        _agent.speed = speedScared;

        StartCoroutine(ChaseToy());
        RagdollHitboxState(true);

        DoPlayCheck += StartPlayCheck;
        //DoChaseToy += ChaseToy;

    }

    IEnumerator ChaseToy()
    {
        Debug.Log($"<color=blue> Buscando </color>");

        var wait = new WaitForSeconds(0.5f);

        while ( _chassingToy )
        {
            _actualNode = _toy.transform;
            _agent.SetDestination(_actualNode.position);

            yield return wait;
        }

    }

    void StartPlayCheck()
    {
        if (_playing && Time.time - _playingTime > _playDuration)
        {
            StopPlaying();
        }
        if (!_playing && Vector3.SqrMagnitude(transform.position - _toy.position) <= (_changeNodeDist * _changeNodeDist * 2))
        {
            //Debug.Log("<color=#26c5f0> LLege al destino </color>");

            PlayWithToy();
        }
    }

    void PlayWithToy()
    {
        //DoPlayCheck = delegate { };
        Debug.Log($"<color=blue> ke vonita da peloitta </color>");

        _chassingToy = false;
        _playing = true;

        _agent.speed = 0f;

        RagdollHitboxState(false);

        _playingTime = Time.time;

    }

    void StopPlaying()
    {
        Debug.Log($"<color=blue> pelota aburrida fea pelotuda </color>");


        _playing = false;

        _actualNode = GetNewNode(_actualNode);

        _agent.SetDestination(_actualNode.position);

        _agent.speed = speedNormal;


    }

    void RagdollHitboxState(bool newState)
    {
        _ragHitbox.active = newState;
    }


    void StartChasePlayer()
    {
        Debug.Log("<color=green>Start Chase</color>");

        StartCoroutine(ChaseTarget());
        StartCoroutine(ChaseDuration());
    }

    bool _activeChase = false;

    IEnumerator ChaseTarget()
    {
        Debug.Log("<color=green>Woof Woof MADAFAKA</color>");

        _activeChase = true;
        var target = GameManager.Instance.Player;
        //Debug.Log("<color=#825aef>Inicia Cazeria</color>");
        //consigue pos de Gus cada medio segundo

        while (_activeChase && !_playing && _agent.enabled && !_chassingToy)
        {
            Debug.Log("<color=green>Come here PUSSY</color>");

            _actualNode = target.transform;
            _agent.SetDestination(_actualNode.position);

            yield return new WaitForSeconds(0.5f);
        }

        _activeChase = false;
        //yield return null;       
    }

    IEnumerator StopChase()
    {
        Debug.Log("<color=green>Stop Chase</color>");

        _activeChase = false;

        var nextChase = Random.Range(_timeBtChase[0], _timeBtChase[1]);

        _actualNode = GetNewNode(_actualNode);
        _agent.SetDestination(_actualNode.position);

        yield return new WaitForSeconds(nextChase);
        StartChasePlayer();
    }

    IEnumerator ChaseDuration()
    {

        yield return new WaitForSeconds(_chaseDuration);

        if (_activeChase)
            StartCoroutine(StopChase());

    }
}
