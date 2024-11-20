using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanSlide
{  
    /// <summary>
    /// 
    /// </summary>
    /// <param name="n">Default = 8</param>
    public void StartSlide(Vector3 dir, float n = 8f);

    public void StopSlide();
}
