using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pickable))]
public class Disco : MonoBehaviour
{
    //ya esta, xd
    [SerializeField] AudioClip _song;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<TocaDiscos>(out var tocaDiscos))
        {
            DoShit(tocaDiscos);
        }
    }

    void DoShit(TocaDiscos caca)
    {
        caca.ChangeDisc(_song);
        Destroy(gameObject);
    }
}
