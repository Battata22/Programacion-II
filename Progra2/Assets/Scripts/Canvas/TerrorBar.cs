using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrorBar : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.terrorBar = this.GetComponent<Slider>();
    }
}
