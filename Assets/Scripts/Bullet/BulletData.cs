using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObjects/BulletData")]
public class BulletData : ScriptableObject
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Field
    [SerializeField] private Bullet _prefab;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _defaultMoveSpeed = 100f;
    [SerializeField] private int _defaultDamage = 3;

    [Header("Bullet Options")]
    // [SerializeField] private bool _hasAreaDamage;
    [SerializeField] private GameEnums.BulletType _defaultBulletType = GameEnums.BulletType.Straight;
    // [SerializeField] private bool _hasKnockBackEffect;


    // Getters
    public float DefaultMoveSpeed => _defaultMoveSpeed;
    public int DefaultDamage => _defaultDamage;
    // public bool HasAreaDamage => _hasAreaDamage;
    public Bullet Prefab => _prefab;
    public Sprite Sprite => _sprite;
    public GameEnums.BulletType DefaultBulletType => _defaultBulletType;


    #endregion
}
