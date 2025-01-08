using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] float _doubtTime;
    [SerializeField,Tooltip("<color=green> How much time is needed to bark once in doubt state </color>")]
    float _mercyTime;
    float _timeToBark, _lastDoubt, _timeToEndDoubt;

    DelegateType.VoidDelegate InputCheck = delegate { };

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
}
