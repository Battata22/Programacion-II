using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaGB : MonoBehaviour
{
    // Script de Martin hecho con ayuda de Gianluca, te amamos Gianluca
    [SerializeField] Chocamiento chocamientoScript;
    [SerializeField] Ghostbuster _gBScript;
    [SerializeField] GameObject _electricArea;

    public void Initialize(Ghostbuster newGB)
    {
        _gBScript = newGB;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            //chocamientoScript.Choco(player.transform.position);
            _gBScript.GetDoubt(player.transform.position);
            player._traped = true;
            var _newObj = Instantiate(_electricArea, this.transform.position,Quaternion.identity);
            //dejarte quieto 1s o 0.5s
            //relentizarte durante 5s
            //sacarte 1 de vida quiza? UwU
        }

    }

}
