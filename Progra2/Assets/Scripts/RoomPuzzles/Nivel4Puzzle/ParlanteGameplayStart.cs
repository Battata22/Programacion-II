using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParlanteGameplayStart : GameplayModeTrigger
{
    [SerializeField] float tempo;
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

}
