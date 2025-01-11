using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExorcistaAttack : MonoBehaviour
{
    //a
    [SerializeField] HolyWatherAttack _watherAtk;
    [SerializeField] ParticleSystem _watherParticle;
    [SerializeField] Transform _watherOrigin;
    [SerializeField] float _watherAtkDuration;
    bool _alreadyAtking = false;

    public event DelegateType.VoidDelegate OnWatherEnd = delegate { };

    public void DoHolyWatherAttack()
    {

        if(_alreadyAtking) return;
        _alreadyAtking = true;
        Debug.Log("<color=yellow>Holy Watha</color>");

        var _myOwner = transform.GetComponent<Exorcista>();
        if (_myOwner.inRagdoll)
        {
            Debug.Log("<color=yellow>Estas en ragdoll pah, que intentas?</color>");
            EndWatherAtk();
            return;
        }


        var newAtk = Instantiate(_watherAtk, _watherOrigin.position, Quaternion.identity);
        newAtk.Initialize(_watherAtkDuration);

        //particulas
        _watherParticle.Play();


        Invoke("EndWatherAtk", _watherAtkDuration);
    }

    void EndWatherAtk()
    {
        Debug.Log("<color=blue>Ya ta loco, corta eww, apagaaaaaaaaa</color>");

        _alreadyAtking = false;

        _watherParticle.Stop();

        OnWatherEnd();
    }
}
