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
    [SerializeField] private float _defaultMoveSpeed = 10f;
    [SerializeField] private int defaultMaxHealth = 25;
    [SerializeField] private int defaultDamage = 3;


    // Getters
    public float DefaultMoveSpeed => _defaultMoveSpeed;
    public int DefaultMaxHealth => defaultMaxHealth;
    public int DefaultDamage => defaultDamage;

    #endregion
}
