using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soloquierodormirjefe : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = GetComponent<Player>();    
    }


    void Update()
    {
        if (player.enabled == false)
        {
            player.enabled = true;
        }
    }
}
