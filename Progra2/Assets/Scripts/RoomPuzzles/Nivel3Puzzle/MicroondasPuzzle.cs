using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CasaCatolicaPuzzle;

public class MicroondasPuzzle : Nivel3Puzzle
{
    //idea
    //npc se acerca a calentar la lechita
    //despues de un rato la agarra
    //player puede pasar de rosca el microndas
    //si npc agarra se quema/ asusta / tira la merda

    //despues
    //hace la leche en algo fragil
    //si le pegas un slide o ragdoll deja caer la segunda leche

    //tercer shit
    //agarra unas galletas y se las lleva al pendejo

    [SerializeField] PuzzleLvl3Cocina _myPuzzle;
    [SerializeField] float duration;
    bool _contando = false;
    [SerializeField] public bool active;
    AudioClip noSeUsa;


    [SerializeField] float checkDist;

    bool deberiaQuemar = false;
    public bool doCheck = false;

    public bool startCheck = false;

    private void Update()
    {
        if(doCheck && Vector3.SqrMagnitude(_myPuzzle.npcPuzzle.transform.position - transform.position) < (checkDist * checkDist))
        {
            if (_myPuzzle.npcPuzzle.taQuePela)
                _myPuzzle.npcPuzzle.QuemarNpc();
        }

        if(startCheck && !_contando && Vector3.SqrMagnitude(_myPuzzle.npcPuzzle.transform.position - transform.position) < (checkDist * checkDist))
        {
            _myPuzzle.microwave.PlayMusic(noSeUsa);
            startCheck = false;
        }
    }

    private void FixedUpdate()
    {
        if (_contando && duration!= 10000)
        {
            duration -= Time.fixedDeltaTime;
        }
        if(_contando && duration <= 0f) 
        {
            TerminaLaCalentadaDeLechita();
        }
    }

    void TerminaLaCalentadaDeLechita()
    {
        Debug.Log($"<color=#f9aa0f> Microondas termino su wea</color>");
        _contando = false;
        duration = 0f;

        _myPuzzle.npcPuzzle.GoCheckMicrowave();

    }

    public void StartCountDown(float newDuration)
    {
        Debug.Log($"<color=#f9aa0f> Entre a countdown y active = {active}, duracion{newDuration}</color>");

        if (!active) return;

        _contando = true;
        duration = newDuration;
    }

    public void RestartCountDown(float newDuration)
    {
        Debug.Log($"<color=#f9aa0f> Entre a restart</color>");

        duration = newDuration;
        deberiaQuemar = true;
        _myPuzzle.npcPuzzle.taQuePela = deberiaQuemar;
    }

}
