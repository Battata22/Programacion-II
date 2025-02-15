using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDistance : MonoBehaviour
{
    [SerializeField] Cam _cam;
    [SerializeField] LayerMask _layerMask;
    
    //destino del raycast
    [SerializeField] Transform _lookingAt;
    
    //largo del rayo
    [SerializeField] float _rayDistance;

    Vector3 _origin;

    //pinto de colision
    Vector3 _point;

    private void Update()
    {
        //guardar pos dle pj, en teoria pos del mesh pero no queria usar mesh de una porque soy especial
        if (transform.GetComponent<Player>())
            _origin = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        else
            _origin = transform.position;

        //direccion del ray
        Vector3 _ray = (_lookingAt.position - _origin).normalized;

        float distanceToUse = _rayDistance;

        RaycastHit hit;
        
        Debug.DrawRay(_origin, _ray * distanceToUse, Color.green);

        // Crea Raycast     origen / direccion  /devuelve algo /largo del ray/ layer que lo puede cortar/
        if (Physics.Raycast(_origin, _ray.normalized, out hit, distanceToUse, _layerMask) /*&& !hit.transform.TryGetComponent<RoomTrigger>(out var room)*/ && !hit.collider.isTrigger)
        {
            //Al colicionar con una pared, guarda el liugar y activa el uso de punto para la posicion d ela camara

            _point = hit.point;

            var chota = hit.normal;
            _cam.surfaceNormal = chota;

            //if (hit.transform.TryGetComponent<Pickable>(out var objecto) && objecto.holding)
            //    _cam.usePoint = false;
            //else
            _cam.usePoint = true;
                
        }
        else if(_cam.usePoint)
        {
            //Desactiva uso de punto
            _cam.usePoint = false;
        }
    }

    private void LateUpdate()
    {
        if (_cam.usePoint)
        {
            //Da cordenadas del punto en la pared
            _cam.point = _point;
        }
    }
}
