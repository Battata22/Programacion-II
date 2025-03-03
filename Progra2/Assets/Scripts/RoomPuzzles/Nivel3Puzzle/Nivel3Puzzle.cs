using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasaCatolicaPuzzle
{
    public class Nivel3Puzzle : MonoBehaviour
    {
        [SerializeField] protected string[] _objectiveTexts;
        [SerializeField] protected int _textIndex = 0;

        [SerializeField] protected GameObject[] _flechas;


        protected void ChangeText()
        {
            GameManager.Instance.ChangeObjectiveText(_objectiveTexts[_textIndex]);
        }
    }
}