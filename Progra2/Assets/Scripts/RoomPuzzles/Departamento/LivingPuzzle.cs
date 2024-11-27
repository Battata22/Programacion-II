using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingPuzzle : MonoBehaviour
{
    [SerializeField] Bath _bath;
    [SerializeField] Door[] _doors;
    [SerializeField] GameObject[] _pets;
    [SerializeField] RoomTrigger[] _nextRooms;
    [SerializeField] Asustable[] asustables;
    AINodeManager _nodeManager;

    [SerializeField] DepaRoomPuzzle _roomPuzzle;

    private IEnumerator Start()
    {
        _nodeManager = GetComponentInParent<AINodeManager>();

        yield return new WaitForEndOfFrame();
        GameManager.Instance.Rociadores.OnRociadoresActive += CompleteRoom;
    }

    void CompleteRoom()
    {
        Debug.Log($"<color=green> CUARTO COMPLETADO </color>");

        foreach (var door in _doors)
        {
            door.OpenDoor();
        }

        bool resetList = true;
        foreach (var room in _nextRooms)
        {
            _nodeManager.SetActiveNodes(room.roomIndex, resetList);
            resetList = false;
        }

        foreach (var pet in _pets)
        {
            pet.SetActive(true);
        }

        foreach (var asus in asustables)
        {
            asus.GetScared(1);
        }
        GameManager.Instance.Rociadores.OnRociadoresActive -= CompleteRoom;

        _roomPuzzle.Shit();

    }

    private void OnDestroy()
    {
        GameManager.Instance.Rociadores.OnRociadoresActive -= CompleteRoom;
    }
}
