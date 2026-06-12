using System;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/New EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("PREFAB")]
    [SerializeField] private Enemy _prefab;
    [SerializeField, Min(0)] private float _defaultMoveSpeed = 10f;
    [SerializeField, Min(1)] private int defaultMaxHealth = 25;
    [SerializeField] private ExpGemData _expGemData;

    [HorizontalLine, Header("ATTACK")]
    public bool hasMeleeAttack1;
    [ShowIf("hasMeleeAttack1")] public EnemyAttackType meleeAttack1;


    // Getters
    public Enemy Prefab => _prefab;
    public float DefaultMoveSpeed => _defaultMoveSpeed;
    public int DefaultMaxHealth => defaultMaxHealth;
    public ExpGemData ExpGemData => _expGemData;

    private void OnValidate()
    {
        if (meleeAttack1.targetDetectionRadius >= meleeAttack1.attackRadius)
        {
            meleeAttack1.targetDetectionRadius = meleeAttack1.attackRadius - 0.01f;
        }
    }

    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CLASS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [Serializable]
    public struct EnemyAttackType
    {
        [Header("DAMAGE")]
        public KHDamage damage;

        [HorizontalLine, Header("OTHER")]
        [Min(0.1f)] public float targetDetectionRadius;
        [Min(0.1f)] public float attackRadius;
        [Min(0)] public float speed;
    }

    #endregion
}
