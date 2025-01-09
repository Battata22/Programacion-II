using System;
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
    float _timeToBark, _lastDoubt, _timeToEndDoubt;

    DelegateType.VoidDelegate InputCheck = delegate { };

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
    }

    public void StopLadrido()
    {
        areaSpawn = false;
        playing = false;
        _audioSource.Stop();

        EndAlert();
    }

    public override void GetDoubt(Vector3 pos)
    {
        //base.GetDoubt(pos);
    }

    public void StartAlert()
    {
        if (_chassingToy) return;

        //Dejar quieto al perro
        //Hacer sonido para llamar atencion del player
        //Arrancar a dudar
        //Mientras duda si te moves, ladra
        //Si no ladro en X tiempo dejar de dudar

        //Debug.Log($"<color=green>ENTRADA A START ALETR</color>");

        _agent.speed = 0;

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

    }

    void EndAlert()
    {

        InputCheck = delegate { };
        _timeToBark = 0;
        _timeToEndDoubt = 0;
        _lastDoubt = Time.time;

        _agent.speed = speedNormal;
    }

    //Toy shit

    public void StartChaseToy(Transform newToy)
    {
        if (_playing) return;
        EndAlert();
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
}
