using System;
using System.Collections.Generic;
using KH;

public class LMExperience : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public LMExperience(LevelManager newOwner)
    {
        _owner = newOwner;
    }

    private readonly LevelManager _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Listeners
    public event Action<int, int> OnExpPointsChanged;
    public event Action<int> OnExpLevelChanged;

    //
    private int _expPoints = 0;
    private int _expLevel = 1;
    private int ExpLimit => (int)(_expLevel * 1.5f) + 4;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IOnEnable() { }
    public void IOnDisable() { }
    public void IAwake() { }
    public void IStart() { }
    public void IUpdate() { }
    public void IFixedUpdate() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void NextExpLevel()
    {
        _expPoints = 0;
        _expLevel++;

        ShowUpgradeCards();

        OnExpLevelChanged?.Invoke(_expLevel);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void ShowUpgradeCards()
    {
        _owner.SetLevelActive(false);

        int cardsCount = LevelUIManager.Ins.cards.Count;

        List<UpgradeCardData> randomCards = GameManager.Ins.Data.upgradeCardDatas.KHPickRandom(cardsCount);

        LevelUIManager.Ins.cards.KHForEach((c, i) => c.SetCard(randomCards[i]));

        LevelUIManager.Ins.cardsContainer.KH_Show_Pop();
    }

    public void AddExpPoints(int amount)
    {
        if (amount <= 0)
            return;

        _expPoints += amount;

        if (_expPoints >= ExpLimit)
            NextExpLevel();

        OnExpPointsChanged?.Invoke(ExpLimit, _expPoints);
    }

    #endregion
}
