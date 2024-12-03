using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AguaBendita : MonoBehaviour
{
    [SerializeField] float radioCheck;
    [SerializeField] LayerMask targetLayer, playerLayer;
    [SerializeField] SplashScript _splashGen;

    void Start()
    {
        gameObject.GetComponent<Pickable>().aguaRompible = true;
    }

    private void OnDestroy()
    {
        BlessEverything();
        var newShit = Instantiate(_splashGen, transform.position, transform.rotation);
        newShit.Initialize();
    }

    public void BlessEverything()
    {
        Collider[] collCercanos = Physics.OverlapSphere(transform.position, radioCheck, targetLayer);
        foreach (Collider coll in collCercanos)
        {
            if(coll.gameObject.TryGetComponent<IBlessable>(out var coso))
                coso.GetBlssed();
        }

        Collider[] playersCercanos = Physics.OverlapSphere(transform.position, radioCheck, playerLayer);
        foreach (Collider player in playersCercanos)
        {

            GameManager.Instance.Player.GetComponent<Player>().GetDamage();
        }

    }
}
