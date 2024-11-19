using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDetecter : MonoBehaviour
{
    [SerializeField] float radioDestroy;
    bool pri = false, deleted = false;

    void Start()
    {
        Collider[] collidersTraps = (Physics.OverlapSphere(transform.position, radioDestroy));
        foreach (Collider collider in collidersTraps)
        {
            collider.gameObject.TryGetComponent<PlayerTrap>(out PlayerTrap TrapScript);
            if (TrapScript != null && pri == false)
            {
                pri = true;
            }
            else if (TrapScript != null && pri == true)
            {
                GameManager.Instance.Player.gameObject.TryGetComponent<CreatePlayerTrap>(out CreatePlayerTrap create);
                if (deleted == false)
                {
                    create.currentTraps--;
                }
                deleted = true;
                Destroy(gameObject);
            }
        }
    }
 
}
