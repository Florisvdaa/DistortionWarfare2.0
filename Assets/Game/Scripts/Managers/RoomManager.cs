using MoreMountains.Feedbacks;
using MoreMountains.TopDownEngine;
using System.Collections;
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
    public enum RewardType
    {
        None,
        Coins,
        Weapon,
        Upgrade,
        Healing,
        Shop
    }

    [Header("Room settings")]
    [SerializeField] private bool isStartingRoom = false;
    [SerializeField] private bool isCorridor = false;

    [SerializeField] private GameObject startingPortal;

    [Header("Connected Portals to this room")]
    [SerializeField] private List<GameObject> portals = new List<GameObject>();

    [Header("Enums")]
    [SerializeField] private RoomState currentState = RoomState.Unvisited;
    [SerializeField] private RewardType rewardType = RewardType.None;

    [Header("Active Coins In Room")]
    [SerializeField] private List<CoinParent> coinsInRoom = new List<CoinParent>();

    private void Start()
    {
        if (isStartingRoom)
        {
            if (startingPortal == null) Debug.Log("Correct");
            ActivatePortals();
        }
        else if (isCorridor)
            ActivatePortals();
        else
            DeactivatePortals();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (currentState == RoomState.Unvisited)
        {
            EnterRoom();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
    }
    private void EnterRoom()
    {
        DW_GameManager.Instance.SetCurrentRoomManager(this);

        if (!isStartingRoom)
        {
            currentState = RoomState.Active;
            DeactivatePortals();

            EnemySpawner enemySpawner = GetComponent<EnemySpawner>();
            if (enemySpawner != null)
                enemySpawner.StartSpawning();
        }
        else
        {
            OnRoomCleared();
        }
            

    }
    public void OnRoomCleared()
    {
        currentState = RoomState.Cleared;
        ActivatePortals();

        foreach (var port in portals)
        {
            port.gameObject.SetActive(true);

            port.GetComponent<Teleporter>().RollNextRoom();
        }

        StartCoroutine(ChangeCoinMagnetism());
    }

    private IEnumerator ChangeCoinMagnetism()
    {
        yield return new WaitForSeconds(1);

        foreach (var coin in coinsInRoom)
        {
            if (coin != null)
                coin.EnableMagnet();
        }
    }
    
    public void RegisterCoin(CoinParent coin)
    {
        coinsInRoom.Add(coin);
    }

    public void UnregisterCoin(CoinParent coin)
    {
        coinsInRoom.Remove(coin);
    }
    private void DeactivatePortals()
    {
        startingPortal.SetActive(false);

        foreach (var port in portals)
        {
            port.SetActive(false);
        }
    }

    private void ActivatePortals()
    {
        foreach (var port in portals)
        {
            port.SetActive(true);
            //MMF_Player mmf_Player = port.GetComponentInChildren<MMF_Player>();
            //mmf_Player.PlayFeedbacks();
        }
    }

    public RewardType GetRewardType() => rewardType;
    public Transform GetStartingTransform() => startingPortal.transform;
}