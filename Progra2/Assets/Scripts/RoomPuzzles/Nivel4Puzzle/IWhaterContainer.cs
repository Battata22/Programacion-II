using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IWhaterContainer
{
    public void GetWather();
    public void LostWather();

    //Need a bool _hasWather
    /// <summary>
    /// Returns true if has wather
    /// </summary>
    /// <returns></returns>
    public bool CheckWather();
}
