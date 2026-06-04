using System.Collections.Generic;
using KH;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    private float _spawnInterval = 1f;
    private float _spawnDistanceFromPlayer = 5f;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IUpdate()
    {
        if (!_owner.LevelActive)
            return;

        _spawnTimer.Update();

        if (_spawnTimer.DidExceed(_spawnInterval))
        {
            SpawnEnemyAtRandomPos(_levelEnemiesData[0]); // Placeholder for now, will implement enemy selection logic later

            _spawnInterval = Random.Range(1f, 3f);
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

        _levelBoundsCollider.enabled = true; // Enable the collider to use its bounds for point generation

        // Keep generating random points until we find one that is outside the player's spawn radius and within the level bounds
        while (true)
        {
            if (_levelBoundsCollider.OverlapPoint(
                    randomPoint = (Vector2)_player.transform.position + (Kh.GetDir(Random.Range(0, 360)) * _spawnDistanceFromPlayer)
                ))
            {
                _levelBoundsCollider.enabled = false; // Disable the collider if it's not needed for anything else

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
