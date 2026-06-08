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
    [SerializeField, Min(0)] private float _defaultMoveSpeed = 10f, _defaultAttackSpeed = 2;
    [SerializeField, Min(1)] private int _defaultMaxHealth = 100;
    [SerializeField] private BulletData _defaultBulletData;
    [SerializeField] private Directions8 _bulletSpawnPoints;


    // Getters
    public float DefaultMoveSpeed => _defaultMoveSpeed;
    public float DefaultAttackSpeed => _defaultAttackSpeed;
    public int DefaultMaxHealth => _defaultMaxHealth;
    public BulletData DefaultBulletData => _defaultBulletData;
    public Directions8 BulletSpawnPoints => _bulletSpawnPoints;

    #endregion
}
