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
    #region INSPECTOR FIELDS
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

    // Methods

    public void ApplyUpgrade()
    {
        Player player = LevelManager.Ins.Player;

        if (addMaxHealth)
        {
            player.PHealth.HealthCrtl.MaxHealth += bonusMaxHealth;
        }
        if (addPhysicalDamage)
        {
            player.States.bulletDamage.physical.damage += bonusPhysicalDamage;
        }
        if (addMoveSpeed)
        {
            player.States.MoveSpeed *= 1 + bonusMoveSpeed;
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

        impact = string.Join("\n", impactTexts);
    }

    #endregion
}
