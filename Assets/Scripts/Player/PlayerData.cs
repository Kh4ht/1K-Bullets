using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Fields
    [SerializeField] private float _defaultMoveSpeed = 10f;
    [SerializeField] private List<Sprite> _engineFireSprites = new();
    [SerializeField] private int _defaultMaxHealth = 100;
    [SerializeField] private BulletData _defaultBulletData;
    [SerializeField] private float _defaultShootCD = 1;


    // Getters
    public float DefaultMoveSpeed => _defaultMoveSpeed;
    public List<Sprite> EngineFireSprites => _engineFireSprites;
    public int DefaultMaxHealth => _defaultMaxHealth;
    public BulletData DefaultBulletData => _defaultBulletData;
    public float DefaultShootCD => _defaultShootCD;

    #endregion
}
