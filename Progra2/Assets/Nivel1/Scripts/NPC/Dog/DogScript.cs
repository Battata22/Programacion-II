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

        if (_doubt)
            _searchingTimer += Time.deltaTime;
        if (_searchingTimer > 12f) StopSearching();
        if (_doubt && _inPlace && _waitDoubt >= 2)
        {
            StopSearching();
        }
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
    }

    public override void GetDoubt(Vector3 pos)
    {
        //if (playing) return;        
        //base.GetDoubt(pos);
    }
}
