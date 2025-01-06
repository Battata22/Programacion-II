using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GB_Cam : GB_Gadget
{
    [SerializeField] int _hp;
    [SerializeField] float _speed;
    [SerializeField,Range(0, 180), Tooltip("Aplica a ambos lados")] float _maxAngle;
    [SerializeField] float rot;

    [SerializeField] Animator _animator;


    float rotDir = -1;

    private void FixedUpdate()
    {
        //RotateCam();
    }

    void RotateCam()
    {
        rot = rotDir * _speed * Time.fixedDeltaTime;

        transform.Rotate(0, transform.rotation.y + rot, 0);
        if(rotDir > 0)
        {
            if (transform.rotation.y > _maxAngle)
            {
                rotDir *= -1;
                Debug.Log($"Direccion cambiada {rotDir}");
            }
        }
        else
        {
            if (transform.rotation.y < _maxAngle)
            {
                rotDir *= -1;
                Debug.Log($"Direccion cambiada {rotDir}");
            }
        }
        
    }

    public void DetectGhost()
    {
        //Pitido
        //Activar flash bang del GB

        Debug.Log($"<color=green> Gus Detectado </color>");
    }

    public override void GetDamage(int dmgAmount = 1)
    {
        _hp -= dmgAmount;

        if (_hp < 0 )
            Break();
    }

    public override void Break()
    {
        // efectos de rotura
        // desactivar deteccion
        // si no se cae hacer que se caiga, para efectos dramaticos


    }

    public override void Repair()
    {
        // efectos de reparacion
        // reactivar mierda


    }
}
