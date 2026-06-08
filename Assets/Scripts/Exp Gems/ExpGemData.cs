using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ExpGemData", menuName = "ScriptableObjects/New ExpGemData")]
public class ExpGemData : ScriptableObject
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private ExpGem _prefab;
    [SerializeField, Min(1)] private int _expPoints = 1;

    // Getters
    public List<Sprite> Sprites => _sprites;
    public ExpGem Prefab => _prefab;
    public int ExpPoints => _expPoints;

    #endregion
}
