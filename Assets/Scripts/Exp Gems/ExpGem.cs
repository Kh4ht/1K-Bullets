using System.Collections.Generic;
using KH;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer))]
public class ExpGem : MonoBehaviour
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Static
    public static List<ExpGem> EnabledExpGems { get; private set; } = new();
    public static List<ExpGem> DisabledExpGems { get; private set; } = new();
    private static GameObject _ExpGemsContainer;

    // Components

    // Systems

    // Getters
    public ExpGemData Data => _data;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [SerializeField] private ExpGemData _data;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void Reset()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;

        tag = GameTags.EXP_GEM;
    }

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = _data.Sprites.KHPickRandom();
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private ExpGem ResetExpGem(Vector2 newPos, ExpGemData expGemData)
    {
        transform.position = newPos;
        _data = expGemData;

        return this;
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static void ResetStaticFields()
    {
        EnabledExpGems.Clear();
        DisabledExpGems.Clear();
        _ExpGemsContainer = null;
    }

    public void DisableExpGem()
    {
        gameObject.SetActive(false);
        EnabledExpGems.Remove(this);
        DisabledExpGems.Add(this);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region STATIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static ExpGem GetOrCreateNew(Vector2 spawnPoint, ExpGemData newData)
    {
        if (_ExpGemsContainer == null)
        {
            _ExpGemsContainer = new GameObject($"ExpGems: {EnabledExpGems.Count + DisabledExpGems.Count}");
        }

        if (DisabledExpGems.KHIsEmpty())
        {
            // Case 1: Create New
            ExpGem newExpGem = Instantiate(newData.Prefab, spawnPoint, Quaternion.Euler(0, 0, 0), _ExpGemsContainer.transform);
            EnabledExpGems.Add(newExpGem);

            _ExpGemsContainer.name = $"ExpGems: {EnabledExpGems.Count + DisabledExpGems.Count}";

            return newExpGem;
        }
        else
        {
            // Get Disabled
            ExpGem selectedExpGem = DisabledExpGems[0];

            DisabledExpGems.Remove(selectedExpGem);
            EnabledExpGems.Add(selectedExpGem);

            selectedExpGem.gameObject.SetActive(true); // TEST: move this to the last to see if previous lines will work, or you need to set it to active first for them to work.

            return selectedExpGem.ResetExpGem(spawnPoint, newData);
        }
    }

    #endregion
}
