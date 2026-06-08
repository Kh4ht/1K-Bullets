using System.Collections.Generic;
using KH;
using UnityEngine;

public class LMEnemySpawner : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public LMEnemySpawner(LevelManager newOwner, List<EnemyData> levelEnemiesData, Collider2D levelGroundCollider, Player player)
    {
        _owner = newOwner;
        _levelEnemiesData = levelEnemiesData;
        _levelBoundsCollider = levelGroundCollider;
        _player = player;
    }

    private readonly LevelManager _owner;
    private readonly List<EnemyData> _levelEnemiesData;
    private readonly Collider2D _levelBoundsCollider;
    private readonly Player _player;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private readonly KHTimer _spawnTimer = new();
    private float _temporarySpawnInterval = 1f;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IUpdate()
    {
        if (!_owner.LevelActive || _owner.IsLevelTimerDone)
            return;

        _spawnTimer.Update();

        if (_spawnTimer.DidExceed(_temporarySpawnInterval))
        {
            SpawnEnemyAtRandomPos(_levelEnemiesData[0]); // Placeholder for now, will implement enemy selection logic later

            _temporarySpawnInterval = Random.Range(_owner.Data.spawnInterval,
                                                   _owner.Data.spawnInterval + 0.5f);
            _spawnTimer.Reset();
        }
    }

    public void IOnEnable() { }
    public void IOnDisable() { }
    public void IAwake() { }
    public void IStart() { }
    public void IFixedUpdate() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void SpawnEnemyAtRandomPos(EnemyData enemyData)
    {
        Enemy.GetOrCreateEnemy(GetRandomPointInLevelBounds(), enemyData);
    }

    private Vector2 GetRandomPointInLevelBounds()
    {
        Vector2 randomPoint;

        // Enable the collider to use its bounds for point generation
        _levelBoundsCollider.enabled = true;

        // Keep generating random points until we find one that is outside the player's spawn radius and within the level bounds
        while (true)
        {
            if (_levelBoundsCollider.OverlapPoint(
                    randomPoint = (Vector2)_player.transform.position + (Kh.GetDir(Random.Range(0, 360)) * _owner.Data.DefaultSpawnDisFromPlayer)
                ))
            {
                // Disable the collider if it's not needed for anything else
                _levelBoundsCollider.enabled = false;

                return randomPoint;
            }
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}
