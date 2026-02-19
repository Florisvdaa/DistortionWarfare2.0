using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinParent : MonoBehaviour
{
    private Transform playerTransform;
    private bool magnetActive = false;

    [SerializeField] float magnetSpeed = 10f;

    private void Awake()
    {
        DisableMagnet();
    }

    private void OnEnable()
    {
        RoomManager currentRM = DW_GameManager.Instance.CurrentRoomManager();
        currentRM.RegisterCoin(this);
    }
    private void OnDisable()
    {
        RoomManager currentRM = DW_GameManager.Instance.CurrentRoomManager();
        currentRM.UnregisterCoin(this);
    }

    private void Update()
    {
        if (!magnetActive) return;

        if (playerTransform == null)
            DW_GameManager.Instance.PlayerTransform();

        if(playerTransform == null) return;

        transform.position = Vector3.Lerp(transform.position, playerTransform.position, magnetSpeed * Time.deltaTime);
    }

    public void EnableMagnet()
    {
        playerTransform = DW_GameManager.Instance.PlayerTransform();
        magnetActive = true;
    }

    public void DisableMagnet()
    {
        magnetActive = false;
    }
}
