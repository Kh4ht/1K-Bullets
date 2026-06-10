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

    [HorizontalLine, Header("DAMAGE")]
    public int defaultKnockBackForce;
    public KHDamage defaultDamage;

    [HorizontalLine, Header("STATES")]
    [Min(0)] public float defaultMoveSpeed = 100f;

    [HorizontalLine, Header("TYPE")]
    public GameEnums.BulletType defaultBulletType = GameEnums.BulletType.Straight;

    #endregion
}
