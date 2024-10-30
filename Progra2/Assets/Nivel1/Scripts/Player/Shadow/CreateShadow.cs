using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateShadow : MonoBehaviour
{
    Player player;
    [SerializeField] Shadow _shadowPrefab;
    bool selected = false;
    float waitCD, cooldown = 5f;

    //[SerializeField] int _maxShadows;
    //public int currentShadows;

    private void Start()
    {
        player = GameManager.Instance.Player; //GetComponent<Player>();
        //cooldown = 5f;
    }

    private void Update()
    {
        //waitCD += Time.deltaTime;

        //esto para evitar que la trampa se sume al evento
        if (gameObject.name != "Player") return;

        if (SelectorUI.habAct == 2 && !selected)
        {
            SelectorUI.habilitiesManager += SpawnShadow;
            selected = true;
        }
        if (SelectorUI.habAct != 2 && selected)
        {
            SelectorUI.habilitiesManager -= SpawnShadow;
            selected = false;
        }


    }
    public void SpawnShadow(Transform myPos)
    {
        //if (player.currentShadows >= player.maxShadows) return;
        //if (waitCD <= cooldown) return;
        Shadow newShadow = Instantiate(GameManager.Instance.ShadowPrefab, myPos.position, Quaternion.identity);
        newShadow.Initialize();
        player.currentShadows++;
    }
}
