using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New UpgradeCardData", menuName = "ScriptableObjects/New UpgradeCardData")]
public class UpgradeCardData : ScriptableObject
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [ShowAssetPreview(100, 100)] public Sprite sprite;

    [Space] public string title;

    [Space, TextArea(2, 5)] public string impact;

    [Space, TextArea(2, 5)] public string description;

    [HorizontalLine, Header("UPGRADE")]
    public bool addMaxHealth;
    [Min(1), ShowIf("addMaxHealth")] public int bonusMaxHealth = 1;

    [Space] public bool addPhysicalDamage;
    [Min(1), ShowIf("addPhysicalDamage")] public int bonusPhysicalDamage = 1;

    [Space] public bool addMoveSpeed;
    [Range(0.02f, 1f), ShowIf("addMoveSpeed")] public float bonusMoveSpeed = 0.02f;

    [Space] public bool addBulletMoveSpeed;
    [Range(0.02f, 1f), ShowIf("addBulletMoveSpeed")] public float bonusBulletMoveSpeed = 0.02f;

    [Space] public bool addAttackSpeed;
    [Range(0.02f, 1f), ShowIf("addAttackSpeed")] public float bonusAttackSpeed = 0.02f;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void ApplyUpgrade()
    {
        Player player = LevelManager.Ins.Player;

        if (addMaxHealth)
        {
            player.PHealth.HealthCrtl.MaxHealth += bonusMaxHealth;
            player.PHealth.HealthCrtl.AddHealth(bonusMaxHealth);
        }
        if (addPhysicalDamage)
        {
            player.Stats.bulletDamage.physical.damage += bonusPhysicalDamage;
        }
        if (addMoveSpeed)
        {
            player.Stats.MoveSpeed *= 1 + bonusMoveSpeed;
        }
        if (addBulletMoveSpeed)
        {
            player.Stats.bulletMoveSpeed *= 1 + bonusBulletMoveSpeed;
        }
        if (addAttackSpeed)
        {
            player.Stats.attackSpeed *= 1 + bonusAttackSpeed;
        }
    }

    private void OnValidate()
    {
        title = name;

        description = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(description.ToLower());

        List<string> impactTexts = new();

        if (addMaxHealth)
        {
            impactTexts.Add($"+ {bonusMaxHealth:N0} Max Health");
        }
        if (addPhysicalDamage)
        {
            impactTexts.Add($"+ {bonusPhysicalDamage:N0} Physical Dmg");
        }
        if (addMoveSpeed)
        {
            impactTexts.Add($"+ {bonusMoveSpeed:P0} Move Speed");
        }
        if (addBulletMoveSpeed)
        {
            impactTexts.Add($"+ {bonusBulletMoveSpeed:P0} Bullet Speed");
        }
        if (addAttackSpeed)
        {
            impactTexts.Add($"+ {bonusAttackSpeed:P0} Attack Speed");
        }

        impact = string.Join("\n", impactTexts);
    }

    #endregion
}
