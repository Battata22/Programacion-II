using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyChangeFloor : MonoBehaviour
{
    Player _player;
    [SerializeField] LayerMask _layerCheck;
    [SerializeField] float _ofset;
    [SerializeField] KeyCode _goUp;
    [SerializeField] KeyCode _goDown;
    

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(_goUp))
        {
            GoUp();
        }
        if(Input.GetKeyUp(_goDown))
        {
            GoDown();
        }


        ////Dev
        //DrawShit();
        //DrawShit(-1);
    }

    void GoUp()
    {
        if (_player.underAttack) return;
        if(Physics.Raycast(_player.transform.position, _player.transform.up, out var hit, 20f,_layerCheck))
        {
            if(hit.transform.TryGetComponent<Piso>(out var piso))
            {
                if (piso.useParent)
                {
                    _player.transform.position = new Vector3(_player.transform.position.x, piso.myParent.transform.position.y + _ofset, _player.transform.position.z);

                    return;
                }
                //Debug.Log($"<color=red> Player pos {_player.transform.position} Floor pos {piso.transform.position} </color>");

                _player.transform.position = new Vector3(_player.transform.position.x, piso.transform.position.y - piso.transform.localPosition.y + _ofset, _player.transform.position.z);
                
                //Debug.Log($"<color=green> Player Final {_player.transform.position} Floor pos {piso.transform.position} </color>");

                //Debug.Log($"<color=yellow> Piso detectado {piso.name} </color>");
            }
        }
    }

    void GoDown()
    {
        if (_player.underAttack) return;
        if (Physics.Raycast(_player.transform.position - new Vector3(0, 01f, 0), -_player.transform.up, out var hit, 20f, _layerCheck))
        {
            if (hit.transform.TryGetComponent<Piso>(out var piso))
            {
                if (piso.useParent)
                {
                    _player.transform.position = new Vector3(_player.transform.position.x, piso.myParent.transform.position.y + _ofset, _player.transform.position.z);

                    return;
                }

                //Debug.Log($"<color=red> Player pos {_player.transform.position} Floor pos {piso.transform.position} </color>");

                _player.transform.position = new Vector3(_player.transform.position.x, piso.transform.position.y - piso.transform.localPosition.y+ _ofset, _player.transform.position.z);
                
                //Debug.Log($"<color=green> Player Final {_player.transform.position} Floor pos {piso.transform.position} </color>");
                
                //Debug.Log($"<color=yellow> Piso detectado {piso.name} </color>");
            }
        }
    }

    //dev
    void DrawShit(float num = 1)
    {
        Color color = Color.red;
        if(num == -1)
            color = Color.green;

        Debug.DrawRay(_player.transform.position, _player.transform.up * 20f * num, color);
    }
}
