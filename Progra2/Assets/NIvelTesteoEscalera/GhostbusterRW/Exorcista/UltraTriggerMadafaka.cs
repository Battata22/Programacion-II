using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraTriggerMadafaka : MonoBehaviour
{
    [SerializeField] OrbitalStrike _laserOfTheCock;

    [SerializeField] bool _isIn = false;

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.GetComponent<Player>())
        {
            _isIn = true;
            Debug.Log("<color=red>AHHHHHHHHHHHHHHHHHHH</color>");
            _laserOfTheCock.StartAttack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<Player>())
        {
            StartCoroutine(StopAtk());
        }
    }

    IEnumerator StopAtk()
    {
        _isIn = false;

        //Debug.Log("<color=red>conio</color>");

        yield return new WaitForSeconds(0.3f);
        if (!_isIn)
        {
            //Debug.Log("<color=red>pingo</color>");

            _laserOfTheCock.StopAttack();
        }
    }
}
