using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILockeable
{
    public void Lock();
    public void Unlock();
}
