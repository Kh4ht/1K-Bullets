using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObjects/BulletData")]
public class BulletData : ScriptableObject
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [Header("PREFAB")]
    public Bullet prefab;

    [HorizontalLine, Header("STATES")]
    public KHDamage defaultDamage;
    [Min(0)] public float defaultMoveSpeed = 100f;
    [Range(0f, 1f)] public float defaultSpeedReduction = 0.1f;


    [HorizontalLine, Header("TYPE")]
    public GameEnums.BulletType defaultBulletType = GameEnums.BulletType.Straight;

    #endregion
}
