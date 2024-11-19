using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Botones : MonoBehaviour
{
    [SerializeField] GameObject spawn, spawnAbuela, abuela;
    [SerializeField] bool tornado = false, moco = false, sombra = false;
    [SerializeField] CreatePlayerTrap createScript;
    [SerializeField] TornadoItems tornadoScript;
    [SerializeField] CreateShadow shadowScript;
    [SerializeField] MocoSpawn mocoScript;
    [SerializeField] Material act, deAct;
    Renderer render;
    [SerializeField] GameObject textoTrampas;

    void Start()
    {
        createScript = GameManager.Instance.Player.GetComponent<CreatePlayerTrap>();
        render = GetComponent<Renderer>();
        GameManager.Instance.barraGanar.gameObject.SetActive(false);
        if (textoTrampas != null) textoTrampas.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.Instance.canActiveTuto)
        {
            render.material = act;
        }
        else
        {
            render.material = deAct;
        }
    }

    public void Trap()
    {
        if (GameManager.Instance.canActiveTuto)
        {
            #region Ignorar
            SelectorUI.habilitiesManager -= tornadoScript.Tornado;
            SelectorUI.habilitiesManager -= shadowScript.SpawnShadow;
            SelectorUI.habilitiesManager -= mocoScript.SetMoco;
            SelectorUI.habilitiesManager -= tornadoScript.Tornado;
            SelectorUI.habilitiesManager -= shadowScript.SpawnShadow;
            SelectorUI.habilitiesManager -= mocoScript.SetMoco;
            SelectorUI.habilitiesManager -= tornadoScript.Tornado;
            SelectorUI.habilitiesManager -= shadowScript.SpawnShadow;
            SelectorUI.habilitiesManager -= mocoScript.SetMoco;
            #endregion

            if (tornado)
            {
                SelectorUI.habAct = 1;
                SelectorUI.habilitiesManager += tornadoScript.Tornado;
                createScript.CreateTrap(spawn.transform.position);
                Instantiate(abuela, spawnAbuela.transform.position, spawnAbuela.transform.rotation);
            }
            else if (sombra)
            {
                SelectorUI.habAct = 2;
                SelectorUI.habilitiesManager += shadowScript.SpawnShadow;
                createScript.CreateTrap(spawn.transform.position);
                Instantiate(abuela, spawnAbuela.transform.position, spawnAbuela.transform.rotation);
            }
            else if (moco)
            {
                SelectorUI.habAct = 4;
                SelectorUI.habilitiesManager += mocoScript.SetMoco;
                createScript.CreateTrap(spawn.transform.position);
                Instantiate(abuela, spawnAbuela.transform.position, spawnAbuela.transform.rotation);
            }

            GameManager.Instance.canActiveTuto = false;
            StartCoroutine(VolverActivar());
        }

    }

    IEnumerator VolverActivar()
    {
        yield return new WaitForSeconds(6);
        GameManager.Instance.canActiveTuto = true;
    }

}
