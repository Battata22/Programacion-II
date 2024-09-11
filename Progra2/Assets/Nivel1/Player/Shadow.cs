using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    Player player;
    [SerializeField] float _lTShadow;
    public void Initialize(Player newPlayer)
    {
        //pedir a manager lista de npc para girar a verlos o algo
        player = newPlayer;
        Destroy(gameObject, _lTShadow);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Chocaste con {other.name}");
        if (other.gameObject.GetComponent<NPC>())
        {
            other.GetComponent<NPC>().GetScare();
        }
    }

    private void OnDestroy()
    {
        player.currentShadows--;
    }
}
