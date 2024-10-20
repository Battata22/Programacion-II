using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnchantable
{
    public void GetEnchanted(Player pl);
    public void EnchantedAction(Player pl);
    public void Unenchant(Player pl);
}
