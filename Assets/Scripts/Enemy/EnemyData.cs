using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/New EnemyData")]
public class EnemyData : ScriptableObject
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Fields
    [SerializeField] private Enemy _prefab;
    [SerializeField, Min(0)] private float _defaultMoveSpeed = 10f;
    [SerializeField, Min(1)] private int defaultMaxHealth = 25;
    [SerializeField] private KHDamage _defaultDamage;
    [SerializeField] private ExpGemData _expGemData;


    // Getters
    public Enemy Prefab => _prefab;
    public float DefaultMoveSpeed => _defaultMoveSpeed;
    public int DefaultMaxHealth => defaultMaxHealth;
    public KHDamage DefaultDamage => _defaultDamage;
    public ExpGemData ExpGemData => _expGemData;

    #endregion
}
