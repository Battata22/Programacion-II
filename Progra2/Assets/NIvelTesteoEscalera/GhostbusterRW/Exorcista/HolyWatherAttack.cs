using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWatherAttack : MonoBehaviour
{
    [SerializeField] float _wait;
    bool _canDamage = false;

    //private void Awake()
    //{
    //    StartCoroutine(TurnOn(_wait));
    //    Destroy(gameObject, _wait * 2);
    //}

    public void Initialize(float duration)
    {
        StartCoroutine(TurnOn(_wait, duration));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_canDamage && other.gameObject.TryGetComponent<Player>(out var player))
        {
            DamagePlayer(player);
        }
    }

    void DamagePlayer(Player player)
    {
        Debug.Log("<color=red>Ataque al jugador</color>");

        _canDamage = false;
        player.GetDamage();
    }

    IEnumerator TurnOn(float newWait, float newAtkDur)
    {
        Debug.Log("<color=red>HitBox prendida</color>");

        WaitForSeconds wait = new WaitForSeconds(newWait);
        yield return wait;

        _canDamage = true;
        transform.GetComponent<Collider>().enabled = true;

        Destroy(gameObject, newAtkDur);
    }
}
