using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SecObjectives : MonoBehaviour
{
    [SerializeField] TMP_Text[] _toDoText;
    [SerializeField] List<Luces> _lights;
    [SerializeField] Plunger _plunger;
    [SerializeField] BathColission _bath;

    private void Awake()
    {
        _toDoText = GetComponentsInChildren<TMP_Text>();       
    }
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        //_lights = GameManager.Instance._lights;
        //foreach (var l in _lights)
        //{
        //    l.OnBroken += CompleteObjective;
        //}
        _plunger.OnStuck += CompleteObjective;
        _bath.OnCatCol += CompleteObjective;
    }

    //Objetivos
    //sopapa al GB
    //gato a la bañera
    //quemar un foco


    void CompleteObjective(int index)
    {
        _toDoText[index].fontStyle = FontStyles.Strikethrough;
    }
}
