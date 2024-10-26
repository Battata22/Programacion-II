using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateShadow : MonoBehaviour
{
    Player player;
    [SerializeField] Shadow _shadowPrefab;

    //[SerializeField] int _maxShadows;
    //public int currentShadows;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    public void SpawnShadow()
    {
        //if (player.currentShadows >= _maxShadows) return;
        Shadow newShadow = Instantiate(_shadowPrefab, transform.position, Quaternion.identity);
        newShadow.Initialize();
        player.currentShadows++;
    }
}
