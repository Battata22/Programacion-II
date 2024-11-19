using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavamanoTrap : SpecialObject
{
    [SerializeField] List<GameObject> aguas = new List<GameObject>();
    protected override void Awake()
    {
        CreateTrap();
    }

    protected override void ObjectAbility(Transform origin)
    {
        foreach(GameObject go in aguas)
        {
            go.SetActive(true);
        }
    }

    public override void CreateTrap()
    {
        currentAbility = ObjectAbility;
        var newTrap = Instantiate(_trapPrefab, transform.position + transform.forward + new Vector3(0.8f,0.2f,1), Quaternion.identity);
        newTrap.transform.forward = transform.forward;
        newTrap.Initialize(currentAbility, _trapCD);
    }
}
