using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour, ILockeable
{
    [SerializeField] int _id;
    public int id {  get { return _id; }}

    [SerializeField] Transform _target;
    public void Unlock()
    {
        if (!_target.TryGetComponent<ILockeable>(out var worthless))
        {
            Debug.Log($"<color=red> UNLOCK TARGET IS NOT A Ilockeable </color>");
            return;
        }
        worthless.Unlock();
        KeyUsed();

    }

    void ILockeable.Lock()
    {
        if (!_target.TryGetComponent<ILockeable>(out var scum))
        {
            Debug.Log($"<color=red> LOCK TARGET IS NOT A Ilockeable </color>");
            return;
        }
        scum.Lock();
    }

    void KeyUsed()
    {
        Debug.Log("<color=green> ABRIENDO CANDADO </color>");
        //Hacer animacion
        //hacer sonido
        //hacer un cafe
        //Destroy cuando todo este listo
        Destroy(gameObject, 0.2f);
    }
}
