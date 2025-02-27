using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextoCambiaPiso : MonoBehaviour
{
    [SerializeField] GameObject _marco;
    [SerializeField] TMP_Text _myText;
    [SerializeField] string[] _texts;
    
    [SerializeField] PyChangeFloor _changeFloor;
    //a
    private void Awake()
    {
        _changeFloor = GetComponent<PyChangeFloor>();
    }

    public void Activate()
    {

        _marco.SetActive(true);
        ChangeText();

        _changeFloor.OnFloorUp += TextoBajar;

    }

    void ChangeText(int index = 0)
    {
        if (index >= _texts.Length) return;

        _myText.text = _texts[index];
    }

    void TextoBajar()
    {
        _changeFloor.OnFloorUp -= TextoBajar;
        _changeFloor.OnFloorDown += TextoNoMore;

        ChangeText(1);
    }

    void TextoNoMore()
    {
        _changeFloor.OnFloorDown -= TextoNoMore;

        _marco.SetActive(false);
    }


}
