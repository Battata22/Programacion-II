using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class StinkBomb : Pickable, IInteractable
{
    [Header("<color=#6e5810> Stink bomb </color>")]
    [SerializeField] float _delay;
    [SerializeField] float _duration;

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

        OnExplode();


        Destroy(gameObject);
    }
}
