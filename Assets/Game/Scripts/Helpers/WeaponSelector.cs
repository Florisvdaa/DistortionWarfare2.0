using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoreMountains.TopDownEngine;

public class WeaponSelector : MonoBehaviour
{
    [SerializeField] private ProjectileWeapon projectileWeapon_1, projectileWeapon_2, projectileWeapon_3;
    [SerializeField] private Sprite weapon_1_sprite, weapon_2_sprite, weapon_3_sprite;
    [SerializeField] private Button weaponButton_1, weaponButton_2, weaponButton_3;

    private Character playerChar;

    private void Awake()
    {
        weaponButton_1.onClick.AddListener(() => SetWeapon(projectileWeapon_1, weapon_1_sprite));
        weaponButton_2.onClick.AddListener(() => SetWeapon(projectileWeapon_2, weapon_2_sprite));
        weaponButton_3.onClick.AddListener(() => SetWeapon(projectileWeapon_3, weapon_3_sprite));
    }

    private void Start()
    {
        Invoke("GetPlayer", 1f);
    }

    private void SetWeapon(ProjectileWeapon selectedWeapon, Sprite weaponSprite)
    {
        CharacterHandleWeapon weapon = playerChar.GetComponent<CharacterHandleWeapon>();
        weapon.InitialWeapon = selectedWeapon;
        weapon.Setup();


        GUIManager.Instance.SetSelectedWeaponSprite(weaponSprite);
        GUIManager.Instance.SetWeaponSelection(false);
        
    }

    private void GetPlayer()
    {
        playerChar = DW_GameManager.Instance.GetPlayerChar();
    }


}
