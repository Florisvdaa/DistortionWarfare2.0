using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    private GameObject nextRoomPrefab;
    private RoomManager.RewardType rewardType;
    private Transform spawnPoint;

    [SerializeField] private SpriteRenderer rewardIcon;
    [SerializeField] private List<Sprite> rewardSprites = new();
    public void RollNextRoom()
    {
        nextRoomPrefab = RoomSetupManager.Instance.GetRandomRoom();
        rewardType = nextRoomPrefab.GetComponent<RoomManager>().GetRewardType();

        // update UI / icon
        ShowRewardIcon(rewardType);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
            return;

        // Spawn next room
        GameObject room = Instantiate(nextRoomPrefab, RoomSetupManager.Instance.GetSpawnPosition().position, Quaternion.identity);
        RoomSetupManager.Instance.UpdateCurrentRoomPosition(room.transform.position);
        // Find the spawnpoint inside the room
        spawnPoint = room.GetComponent<RoomManager>().GetStartingTransform();

        Character character = collision.GetComponent<Character>();
        if(character == null )
            return;

        character.transform.position = spawnPoint.position;

        var movement = character.FindAbility<CharacterMovement>();
        if (movement != null)
            movement.SetMovement(Vector2.zero);

        var rb = character.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;

        DestoryThisRoom();
    }

    private void DestoryThisRoom()
    {
        Destroy(transform.root.gameObject, 2f);
    }

    public void ShowRewardIcon(RoomManager.RewardType type)
    {
        rewardIcon.sprite = rewardSprites[(int)type];
    }
}
