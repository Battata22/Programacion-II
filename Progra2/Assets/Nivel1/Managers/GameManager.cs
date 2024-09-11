using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    private Player _player;
    public Player Player 
    {
        get { return _player; } 
        set { _player = value; }
    }

    [SerializeField] private List<NPC> _npc;
    public List<NPC> Npc
    {
        get { return _npc; }
        set { _npc = value; }
    }

    [SerializeField] private Cam _camera;
    public Cam Camera 
    {
        get { return _camera; } 
        set {  _camera = value; } 
    }

    private Transform _itemHolde;
    public Transform ItemHolde
    {
        get { return _itemHolde;}
        set { _itemHolde = value;}
    }
}
