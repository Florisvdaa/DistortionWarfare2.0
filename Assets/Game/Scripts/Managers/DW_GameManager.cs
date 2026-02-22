using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_GameManager : MonoBehaviour
{
    public static DW_GameManager Instance {  get; private set; }

    [SerializeField] private RoomManager currentRoomManager;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Character playerChar;
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
    private void Start()
    {
        FindPlayer();

        Invoke("GameStart", 1.5f);
    }

    private void GameStart()
    {
        // weapon selection
        GUIManager.Instance.SetWeaponSelection(true);
    }

    private void FindPlayer()
    {
        if (playerTransform != null) return;

        playerChar = FindObjectOfType<Character>();
        if(playerChar != null)
            playerTransform = playerChar.transform;
    }
    public void SetCurrentRoomManager(RoomManager currentRM)
    {
        if (currentRoomManager != null)
            currentRoomManager = null;

        currentRoomManager = currentRM;
    }

    public RoomManager CurrentRoomManager() => currentRoomManager;
    public Transform PlayerTransform()
    {
        if (playerChar != null)
            FindPlayer();

        return playerChar.transform;
    }
    public Character GetPlayerChar() => playerChar;
}
