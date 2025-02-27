using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class StinkBomb : Pickable, IInteractable
{
    [Header("<color=#6e5810> Stink bomb </color>")]
    [SerializeField] float _delay;
    [SerializeField] float _duration;
    [SerializeField] float _expRad;
    [SerializeField] LayerMask _npcsMask;
    [SerializeField] ParticleSystem _stinkGen;

    public event DelegateType.VoidDelegate OnExplode = delegate { };

    bool _active = false;
    bool _exploded = false;

    public void Interact()
    {
        if(_exploded) return;

        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        //prender mecha
        //hacer sonido

        _active = true;

        yield return new WaitForSeconds(_delay);

        if (_active)
            Explode();

    }

    void Explode()
    {
        _exploded = true;

        //Sonidos
        //Particulas
        Debug.Log("<color=#6e5810> Oh Oh Stinky </color>");

        CheckForNpc();
        _stinkGen.Play();

        OnExplode();

        Destroy(gameObject, 8f);
    }


    void CheckForNpc()
    {
        var npcsInRange = Physics.OverlapSphere(transform.position, _expRad, _npcsMask);

        foreach(var npcs in npcsInRange)
        {
            if(npcs.TryGetComponent<NPC>(out var npc) && CheckForWalls(npc.transform))
            {
                npc.GetScared(0.5f, actualRoom);
            }
        }
    }

    bool CheckForWalls(Transform newObj)
    {
        var wallDetected = false;
        var dir = ((newObj.position + new Vector3(0,0.1f,0))- transform.position).normalized;

        if(Physics.Raycast(transform.position, dir, _expRad, GameManager.Instance.DropLayers))
            wallDetected = true;

        return wallDetected;
    }
}
