using CasaFiesta;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParlanteGameplayStart : GameplayModeTrigger
{
    [SerializeField] float tempo;
    [SerializeField] Nivel4Puzzle[] _allPuzzles;

    [SerializeField] TextoCambiaPiso _pingo;
    [SerializeField] LavaderoPuzzle _lavadero;

    [SerializeField] GameObject[] _bailaores;
    [SerializeField] AudioSource _myAudio;

    private void Awake()
    {
        //StartCoroutine(SoyUnBoludo());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<IWhaterContainer>(out var container) && container.CheckWather())
        {
            StartGameplay();
        }
    }

    protected override void StartGameplay()
    {
        _myAudio.Stop();

        foreach(var item in _allPuzzles)
        {
            item.ActivatePuzzle();
        }
        _pingo.Activate();

        //base.StartGameplay();

        foreach (var obj in _objectToActivate)
        {
            obj.SetActive(true);
            if (obj.TryGetComponent<Asustable>(out var asus))
            {
                asus.GetScared(1, -1);
            }
        }

        foreach(var item in _bailaores)
        {
            item.SetActive(false);
        }

        GameManager.Instance.pasoActual = 1;
        //StartGampelayPhase();
        GameManager.Instance.GetComponent<PhaseManager>().CallGamePlayPhase();

        this.enabled = false;

        _lavadero.ChauFlechitas();
    }

    IEnumerator SoyUnBoludo()
    {
        yield return new WaitForSeconds(0.05f * tempo);
        Debug.Log("<color=yellow>Punchi</color>");
        yield return new WaitForSeconds(0.1f * tempo);
        Debug.Log("<color=yellow>Punchi</color>");
        yield return new WaitForSeconds(0.1f * tempo);
        Debug.Log("<color=yellow>Chi</color>");
        yield return new WaitForSeconds(0.03f * tempo);
        Debug.Log("<color=yellow>Chi</color>");
        yield return new WaitForSeconds(0.1f * tempo);
        Debug.Log("<color=yellow>Pum</color>");
        yield return new WaitForSeconds(0.15f * tempo);
        Debug.Log("<color=yellow>Suena el agudo</color>");
        yield return new WaitForSeconds(0.3f * tempo);
        Debug.Log("<color=yellow>De mi corneta</color>");
        yield return new WaitForSeconds(0.2f * tempo);
        Debug.Log("<color=yellow>Se escucha el saxo</color>");
        yield return new WaitForSeconds(0.5f * tempo);
        Debug.Log("<color=yellow>Bailan las chetas B]</color>");
    }

    public void StartMusic()
    {
        _myAudio.Play();
    }
}
