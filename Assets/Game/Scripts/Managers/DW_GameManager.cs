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
    [SerializeField] private WeaponSelector weaponSelector;
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
        //FindPlayerAndStartGame();
        StartCoroutine(FindPlayerAndStartGameCOR());
        //Invoke("GameStart", .5f);
    }

    private void GameStart()
    {
        // weapon selection
        GUIManager.Instance.SetWeaponSelection(true);
    }

    private IEnumerator FindPlayerAndStartGameCOR()
    {
        if (playerTransform != null) yield return null;

        playerChar = FindObjectOfType<Character>();

        if (playerChar != null)
            playerTransform = playerChar.transform;

        if (weaponSelector != null) yield return null;

        weaponSelector = FindObjectOfType<WeaponSelector>();

        weaponSelector.SetPlayer(playerChar);

        yield return new WaitForEndOfFrame();

        // weapon selection & pauses the game time
        GUIManager.Instance.SetWeaponSelection(true);
    }

    private void FindPlayerAndStartGame()
    {
        if (playerTransform != null) return;

        playerChar = FindObjectOfType<Character>();
        if(playerChar != null)
            playerTransform = playerChar.transform;

        // weapon selection & pauses the game time
        GUIManager.Instance.SetWeaponSelection(true);
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
            FindPlayerAndStartGame();

        return playerChar.transform;
    }
    public Character GetPlayerChar() => playerChar;
}
