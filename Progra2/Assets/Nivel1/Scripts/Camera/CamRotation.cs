using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CamRotation : MonoBehaviour
{
    float _mouseX, _mouseY;

    float _xRotation, _yRotation;

    [SerializeField] Transform _playerOrientation;
    [SerializeField] Transform _meshOrientation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        string json = File.ReadAllText(Application.dataPath + "/SensDataFile.json");
        CamData data = JsonUtility.FromJson<CamData>(json);

        if (data != null )
        {
            _mouseX = Input.GetAxis("Mouse X") * Time.fixedDeltaTime * data._xSens;
            _mouseY = Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * data._ySens;
        }
        else
        {
            SensGuardarJSON();
        }

        _yRotation += _mouseX;
        _xRotation -= _mouseY;

        _xRotation = Mathf.Clamp(_xRotation, -89f, 89f);

    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }

    private void LateUpdate()
    {
        _playerOrientation.rotation = Quaternion.Euler(0, _yRotation, 0);
        _meshOrientation.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }

    private void SensGuardarJSON()
    {
        CamData camDataScript = new CamData();
        camDataScript._xSens = 90;
        camDataScript._ySens = 90;

        string json = JsonUtility.ToJson(camDataScript, true);
        File.WriteAllText(Application.dataPath + "/SensDataFile.json", json);
    }
}
