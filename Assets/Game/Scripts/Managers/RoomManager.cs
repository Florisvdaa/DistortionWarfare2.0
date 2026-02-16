using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public enum RoomState
    {
        Unvisited,
        Active,
        Cleared
    }

    [Header("Connected Portals to this room")]
    [SerializeField] private List<GameObject> portals = new List<GameObject>();

    [Header("Room State")]
    [SerializeField] private RoomState currentState = RoomState.Unvisited;

    [SerializeField] bool canChangeState = false;

    //[Header("Enemy Spawner")]
    //[SerializeField] private EnemySpawner spawner;   // need to create this

    private void Start()
    {
        DeactivatePortals();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        canChangeState = true;

        if (currentState == RoomState.Unvisited)
        {
            EnterRoom();
        }
    }

    private void Update()
    {
        if (canChangeState)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                OnRoomCleared();
            }
        }
    }

    private void EnterRoom()
    {
        currentState = RoomState.Active;

        DeactivatePortals();

       

        //if (spawner != null)
        //    spawner.StartSpawning();
    }

    public void OnRoomCleared()
    {
        currentState = RoomState.Cleared;
        ActivatePortals();
    }

    private void DeactivatePortals()
    {
        foreach (var port in portals)
            port.SetActive(false);
    }

    private void ActivatePortals()
    {
        foreach (var port in portals)
            port.SetActive(true);
    }
}