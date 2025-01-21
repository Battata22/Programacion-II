using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSusto : MonoBehaviour,IRoomDetectable
{
    [SerializeField] Espejo espejoScript;
    [SerializeField] bool asuste = false;
    [SerializeField] int actualRoom;

    private void Start()
    {
        espejoScript = GetComponentInParent<Espejo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (espejoScript.activo == true && other.GetComponent<Asustable>() != null && asuste == false)
        {
            Asustable asus = other.GetComponent<Asustable>();
            asus.GetScared(0.5f, actualRoom);
            asuste = true;
        }
    }


    public void SetRoom(int room)
    {
        Debug.Log($"Espejo room {room}");
        actualRoom = room;
    }
}
