using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRagdoll
{
    public void CallRagdollOn();

    public void CallRagdollOn(Vector3 dir);

    public void CallRagdollOff(float wait = 0f, bool scareOnEnd = false);

    //abstract IEnumerator RagdollOff(float wait = 0f, bool scareOnEnd = false);

}
