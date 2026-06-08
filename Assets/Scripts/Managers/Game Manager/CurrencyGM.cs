using System;
using System.Collections.Generic;
using KH;
using UnityEngine;

public class CurrencyGM : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private int _gold;
    private int _redCrystals;

    // Listeners
    private readonly List<Action<int>> _onGoldChanged = new();
    private readonly List<Action<int>> _onRedCrystalsChanged = new();

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IOnEnable() { }
    public void IOnDisable() { }
    public void IAwake()
    {
        _gold = PlayerPrefs.GetInt(GamePP.GOLD, 300);
        _redCrystals = PlayerPrefs.GetInt(GamePP.RED_CRYSTALS, 10);
    }
    public void IStart() { }
    public void IUpdate()
    {
    }
    public void IFixedUpdate() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private static void AddListener(List<Action<int>> list, Action<int> listener)
    {
        if (listener == null || list.Contains(listener))
            return;

        list.Add(listener);
    }

    private static void RemoveListener(List<Action<int>> list, Action<int> listener)
    {
        if (listener == null)
            return;

        list.Remove(listener);
    }

    private static void Notify(List<Action<int>> list, int currency)
    {
        // Make a copy to safely iterate
        var listenersCopy = list.ToArray();

        listenersCopy.KHForEach(listener =>
        {
            try { listener?.Invoke(currency); }
            catch (Exception e) { Debug.LogError($"Listener threw exception: {e}"); }
        });
    }

    private void Save()
    {
        PlayerPrefs.SetInt(GamePP.GOLD, _gold);
        PlayerPrefs.SetInt(GamePP.RED_CRYSTALS, _redCrystals);
        PlayerPrefs.Save();
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Listeners
    public void AddOnGoldChangedListener(Action<int> listener)
    {
        AddListener(_onGoldChanged, listener);
        listener?.Invoke(_gold);
    }

    public void RemoveOnGoldChangedListener(Action<int> listener)
    {
        RemoveListener(_onGoldChanged, listener);
        listener?.Invoke(_gold);
    }

    public void AddOnRedCrystalsChangedListener(Action<int> listener)
    {
        AddListener(_onRedCrystalsChanged, listener);
        listener?.Invoke(_redCrystals);
    }

    public void RemoveOnRedCrystalsChangedListener(Action<int> listener)
    {
        RemoveListener(_onRedCrystalsChanged, listener);
        listener?.Invoke(_redCrystals);
    }

    // CURRENCY
    public bool AddCurrency(CurrencyType type, int amount)
    {
        if (amount <= 0)
            return false;

        switch (type)
        {
            case CurrencyType.Gold:

                _gold += amount;

                Save();

                Notify(_onGoldChanged, _gold);

                return true;

            case CurrencyType.RedCrystals:

                _redCrystals += amount;

                Save();

                Notify(_onRedCrystalsChanged, _redCrystals);

                return true;

            default:
                return false;
        }
    }

    public bool SpendCurrency(CurrencyType type, int amount)
    {
        if (amount < 0)
            return false;

        switch (type)
        {
            case CurrencyType.Gold:

                if (_gold - amount < 0)
                    return false;

                _gold -= amount;

                Save();

                Notify(_onGoldChanged, _gold);

                return true;

            case CurrencyType.RedCrystals:

                if (_redCrystals - amount < 0)
                    return false;

                _redCrystals -= amount;

                Save();

                Notify(_onRedCrystalsChanged, _redCrystals);

                return true;

            default:

                return false;
        }
    }

    public void SetCurrency(CurrencyType type, int amount)
    {
        if (_gold == amount)
            return;

        amount = Mathf.Max(0, amount);

        switch (type)
        {
            case CurrencyType.Gold:
                _gold = amount;
                Save();
                Notify(_onGoldChanged, _gold);
                break;

            case CurrencyType.RedCrystals:
                _redCrystals = amount;
                Save();
                Notify(_onRedCrystalsChanged, _redCrystals);
                break;
        }
    }

    public int GetCurrency(CurrencyType type)
    {
        return type switch
        {
            CurrencyType.Gold => _gold,
            CurrencyType.RedCrystals => _redCrystals,
            _ => 0
        };
    }

    public bool CanAfford(CurrencyType type, int amount)
    {
        return GetCurrency(type) >= amount;
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region ENUM
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public enum CurrencyType
    {
        Gold,
        RedCrystals
    }

    #endregion
}
