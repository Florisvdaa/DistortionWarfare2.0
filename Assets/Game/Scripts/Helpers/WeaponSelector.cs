using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoreMountains.TopDownEngine;

public class WeaponSelector : MonoBehaviour
{
    [SerializeField] private ProjectileWeapon projectileWeapon_1;
    [SerializeField] private ProjectileWeapon projectileWeapon_2;
    [SerializeField] private ProjectileWeapon projectileWeapon_3;
    [SerializeField] private Button weaponButton_1;
    [SerializeField] private Button weaponButton_2;
    [SerializeField] private Button weaponButton_3;

    private Character playerChar;

    private void Awake()
    {
        weaponButton_1.onClick.AddListener(() => SetWeapon(projectileWeapon_1));
        weaponButton_2.onClick.AddListener(() => SetWeapon(projectileWeapon_2));
        weaponButton_3.onClick.AddListener(() => SetWeapon(projectileWeapon_3));
    }

    private void Start()
    {
        Invoke("GetPlayer", 1f);
    }

    private void SetWeapon(ProjectileWeapon selectedWeapon)
    {
        CharacterHandleWeapon weapon = playerChar.GetComponent<CharacterHandleWeapon>();
        weapon.InitialWeapon = selectedWeapon;
        weapon.Setup();
    }

    private void GetPlayer()
    {
        playerChar = DW_GameManager.Instance.GetPlayerChar();
    }


}
