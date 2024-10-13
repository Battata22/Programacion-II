using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpamAlert : MonoBehaviour
{
    int lastGbAmount;
    [SerializeField] RawImage _spamImage;

    private void Start()
    {
        foreach(Ghostbuster gb in GameManager.Instance.Gb)
        {
            gb.OnAttackStart += SpamOn;
            gb.OnAttackEnd += SpamOff;
            lastGbAmount++;
        }
    }

    private void Update()
    {
        if (lastGbAmount < GameManager.Instance.Gb.Count)
        {
            lastGbAmount = 0;
            foreach (Ghostbuster gb in GameManager.Instance.Gb)
            {
                gb.OnAttackStart -= SpamOn;
                gb.OnAttackEnd -= SpamOff;
                gb.OnAttackStart += SpamOn;
                gb.OnAttackEnd += SpamOff;
                lastGbAmount++;
            }
        }
    }

    void SpamOn()
    {
        _spamImage.gameObject.SetActive(true);
    }

    void SpamOff()
    {
        _spamImage.gameObject.SetActive(false);
    }
}
