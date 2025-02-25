using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextoMisiones : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance._objectiveText = GetComponent<TMP_Text>();
    }
}
