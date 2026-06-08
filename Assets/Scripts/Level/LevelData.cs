using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/New Level")]
public class LevelData : ScriptableObject
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [SerializeField] private int _timeLimitInMinutes = 5;
    [SerializeField, Min(1)] public float _defaultSpawnDisFromPlayer = 4f;
    [SerializeField, Min(0.1f)] public float spawnInterval;
    [SerializeField] private List<EnemyData> _levelEnemiesData;

    // Getters
    public int TimeLimitInMinutes => _timeLimitInMinutes;
    public int TimeLimitInSeconds => _timeLimitInMinutes * 60;
    public float DefaultSpawnDisFromPlayer => _defaultSpawnDisFromPlayer;
    public List<EnemyData> LevelEnemiesData => _levelEnemiesData;

    #endregion
}
