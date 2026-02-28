using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSetupManager : MonoBehaviour
{
    public static RoomSetupManager Instance { get; private set; }

    [Header("Possible Rooms")]
    [SerializeField] private List<GameObject> roomPrefabs = new List<GameObject>();

    [SerializeField] private Transform[] roomSpawnPositions;

    private int spawnPosIndex = 0;

    [SerializeField] private GameObject startRoom;
    private Vector3 currentRoomPosition = Vector3.zero;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Later expand on this, for weighted rooms, type progression etc.
    public GameObject GetRandomRoom()
    {
        int index = Random.Range(0, roomPrefabs.Count);
        return roomPrefabs[index];
    }

    public Transform GetSpawnPosition()
    {
        spawnPosIndex++;

        if(spawnPosIndex >= roomSpawnPositions.Length)
            spawnPosIndex = 0;

        return roomSpawnPositions[spawnPosIndex];
    }
    public void UpdateCurrentRoomPosition(Vector3 newPos)
    {
        currentRoomPosition = newPos;
    }
    public Vector3 GetNextRoomPosition()
    {
        return currentRoomPosition + new Vector3(40, 0, 0);
    }
}
