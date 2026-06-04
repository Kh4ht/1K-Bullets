using System.Collections.Generic;
using NaughtyAttributes;
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
    [SerializeField, ShowAssetPreview] private Sprite _sprite;

    [Space, Space]

    [SerializeField, Min(0)] private float _defaultMoveSpeed = 100f;
    [SerializeField, Min(0)] private int _defaultDamage = 3;
    [SerializeField, Range(0f, 1f)] private float _defaultSpeedReduction = 0.1f;


    [Header("Bullet Options")]
    // [SerializeField] private bool _hasAreaDamage;
    [SerializeField] private GameEnums.BulletType _defaultBulletType = GameEnums.BulletType.Straight;
    // [SerializeField] private bool _hasKnockBackEffect;


    // Getters
    public float DefaultMoveSpeed => _defaultMoveSpeed;
    public float DefaultSpeedReduction => _defaultSpeedReduction;
    public int DefaultDamage => _defaultDamage;
    // public bool HasAreaDamage => _hasAreaDamage;
    public Bullet Prefab => _prefab;
    public Sprite Sprite => _sprite;
    public GameEnums.BulletType DefaultBulletType => _defaultBulletType;


    #endregion
}
