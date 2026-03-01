using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSetupManager : MonoBehaviour
{
    public static RoomSetupManager Instance { get; private set; }

    [Header("Possible Rooms")]
    [SerializeField] private List<GameObject> easyDifficultyRoomsPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> mediumDifficultyRoomsPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> hardDifficultyRoomsPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> extremeDifficultyRoomsPrefabs = new List<GameObject>();
    [SerializeField] private Transform[] roomSpawnPositions;

    private int spawnPosIndex = 0;
    private int difficultyIndex = 0;
    private int maxDifficultyIndex = 10;

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

    // Later expand on this, for weighted rooms.

    public GameObject GetRandomRoom()
    {
        switch(difficultyIndex)
        {
            case 0:
                int easyIndex = Random.Range(0, easyDifficultyRoomsPrefabs.Count);
                return easyDifficultyRoomsPrefabs[easyIndex];
            case 1:
                int mediumIndex = Random.Range(0, mediumDifficultyRoomsPrefabs.Count);
                return mediumDifficultyRoomsPrefabs[mediumIndex];
            case 2:
                int hardIndex = Random.Range(0, hardDifficultyRoomsPrefabs.Count);
                return hardDifficultyRoomsPrefabs[hardIndex];
            case 3:
                int extremeIndex = Random.Range(0, extremeDifficultyRoomsPrefabs.Count);
                return extremeDifficultyRoomsPrefabs[extremeIndex];
            default:
                int defaultIndex = Random.Range(0, easyDifficultyRoomsPrefabs.Count);
                return easyDifficultyRoomsPrefabs[defaultIndex];
        }
        
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
    public void IncreaseDifficulty()
    {
        difficultyIndex += 1;
        if(difficultyIndex > 10)
        { 
            difficultyIndex = 10;
            Debug.Log($"Currenct Difficulty = {difficultyIndex}, max Difficulty = {maxDifficultyIndex}");
        }
    }    
}
