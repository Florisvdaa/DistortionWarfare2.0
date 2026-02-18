using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Spawner settings")]
    [SerializeField] private List<Transform> spawnLocations = new List<Transform>();
    [SerializeField] private List<GameObject> spawnObjects = new List<GameObject>();
    [SerializeField] private float spawnDelay = 0.5f;
    
    [SerializeField] private List<GameObject> activeEnemies = new List<GameObject>();
    private RoomManager roomManager;
    private bool roomCleared = false;

    //[SerializeField] private List<Enemy> possibleEnemies = new List<Enemy>();

    private void Awake()
    {
        roomManager = GetComponent<RoomManager>();
    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        Debug.Log("Spawning enemies");

        foreach (Transform spawnPoint in spawnLocations)
        {
            GameObject enemyPrefab = GetRandomEnemy();
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            activeEnemies.Add(enemy);

            // Subscribe to enemy death event.
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.OnEnemyDied += HandleEnemyDeath;

            yield return new WaitForSeconds(spawnDelay);
        }

        Debug.Log("All enemies spawned");
        
    }

    private GameObject GetRandomEnemy()
    {
        return spawnObjects[Random.Range(0, spawnObjects.Count)];
    }

    private void HandleEnemyDeath(Enemy enemy)
    {
        if (roomCleared) return;

        activeEnemies.Remove(enemy.gameObject);
        activeEnemies.RemoveAll(e => e == null);

        if (activeEnemies.Count == 0)
        {
            roomCleared = true;
            Debug.Log("Room cleared!");
            roomManager.OnRoomCleared();
        }
    }
}
