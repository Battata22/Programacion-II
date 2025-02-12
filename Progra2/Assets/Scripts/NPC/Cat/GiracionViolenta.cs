using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiracionViolenta : MonoBehaviour
{
    //pingocho
    [SerializeField] float rotSpeed;
    [SerializeField] Vector3 randomAxis;
    [SerializeField] float changeAxisTime;

    bool active = false;
    //a
    private void OnEnable()
    {
        active = true;

        StartCoroutine(ChangeAxis());
    }

    private void OnDisable()
    {
        active = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetNewAxis();
        }

        Rotacion();
    }

    void SetNewAxis()
    {
        var x = Random.Range(0, 360);
        var y = Random.Range(0, 360);
        var z = Random.Range(0, 360);

        randomAxis = new Vector3(x, y, z);
    }

    void Rotacion()
    {
        transform.Rotate(randomAxis, 90 * rotSpeed * Time.deltaTime);
    }

    IEnumerator ChangeAxis()
    {
        while (active)
        {
            SetNewAxis();

            yield return new WaitForSeconds(changeAxisTime);
        }
    }


}
