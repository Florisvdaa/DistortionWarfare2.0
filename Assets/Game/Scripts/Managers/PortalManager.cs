using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [Header("Connected Portals to this room")]
    [SerializeField] private List<GameObject> portals = new List<GameObject>();
}
