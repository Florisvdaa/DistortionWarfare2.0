using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomReward", menuName = "DistortionWarfare/RoomReward")]
public class RoomRewards : ScriptableObject
{
    public List<GameObject> coinRewards;
    public List<GameObject> weaponRewards;
    public List<GameObject> upgradeRewards;
    public List<GameObject> healthRewards;
}
