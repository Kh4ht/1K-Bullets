using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameData", menuName = "ScriptableObjects/New GameData")]
public class GameData : ScriptableObject
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [Header("AUDIO")]
    public AudioClip PopShow;
    public AudioClip PopHide;

    public List<UpgradeCardData> upgradeCardDatas;

    #endregion
}
