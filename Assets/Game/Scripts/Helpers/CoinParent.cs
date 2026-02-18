using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinParent : MonoBehaviour
{
    [Header("Magnetic object")]
    public LayerMask nothing;
    public LayerMask player;
    [SerializeField] private Magnetic magneticOBJ;

    private void Awake()
    {
        magneticOBJ.TargetLayerMask = nothing;
    }

    public void ActivateMagnetic()
    {
        magneticOBJ.TargetLayerMask = player;
    }
}
