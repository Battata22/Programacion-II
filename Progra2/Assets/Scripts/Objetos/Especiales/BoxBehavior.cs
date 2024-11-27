using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : MonoBehaviour, IFlamable
{
    bool OnFire = false;
    [SerializeField] ParticleSystem _fireGen;

    private void OnCollisionEnter(Collision collision)
    {
        if (!OnFire) return;
        if(collision.gameObject.TryGetComponent<IFlamable>(out var flamable))
        {
            flamable.SetOnFire();
        }
    }


    public void ExtinguishFire()
    {
        //_fireGen.Stop();
    }

    public void SetOnFire()
    {
        //Activar particulas de humo
        if (OnFire) return;
        Debug.Log($"<color=red> Set on fire </color>");

        OnFire = true;
        //_fireGen.Play();

        GameManager.Instance.Rociadores.OnRociadoresActive += ExtinguishFire;
        GameManager.Instance.Rociadores.objectsOnFire++;
    }

}
