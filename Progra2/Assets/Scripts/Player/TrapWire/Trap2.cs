using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Trap2 : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Transform trap1;

    [SerializeField] AudioClip actNpc;
    [SerializeField] LineRenderer linea;
    [SerializeField] float _cooldown;
    [SerializeField] LayerMask _detectableMask;
    float _lastActiveTime = -1;
    [SerializeField] bool _canStun = true;
    //[SerializeField] GameObject lineaPrefab;
    GameObject primer;

    private void Awake()
    {
        linea = Instantiate(linea, new Vector3(), Quaternion.identity);
        primer = GameManager.Instance.firstVisualLinea;
        GameManager.Instance.firstVisualLinea = null;


    }

    void Update()
    {
        var dir = trap1.position - transform.position;
        ray = new Ray(transform.position, dir);
        if (_canStun)
            Debug.DrawRay(transform.position, dir, Color.yellow);
        if(_canStun && Physics.Raycast(ray, out hit, dir.magnitude ,_detectableMask))
        {
            
            if (hit.transform.TryGetComponent<Asustable>(out var asus) && !asus.stuned && asus.scared)
            {
                print("abuelita vivia en peguajo");
                asus.GetStun(actNpc);
                _lastActiveTime = Time.time;
                _canStun = false;
                linea.enabled = false;

                Destroy(linea.gameObject);
                Destroy(primer.gameObject);
                Destroy(gameObject);
                GameManager.Instance.createPlayerTrap.currentTraps--;
                //hit.transform.GetComponent<Asustable>().GetStun(clip);
            }

            if (hit.collider != null)
            {
                print(hit.collider.gameObject.name);
            }
        }



        //if (Time.time - _lastActiveTime > _cooldown && !_canStun)
        //{
        //    _canStun = true;
        //    linea.enabled = true;
        //}
    }

    public void Initialize(Transform localTrap1)
    {
        trap1 = localTrap1;

        linea.enabled = true;
        linea.SetPosition(0, trap1.position);
        linea.SetPosition(1, transform.position);
        //if()
    }


}
