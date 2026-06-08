using System;

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

    private void ShowUpgradeCards()
    {
        _owner.SetLevelActive(false);

        LevelUIManager.Ins.card1.SetRandomUpgrade();
        LevelUIManager.Ins.card2.SetRandomUpgrade();
        LevelUIManager.Ins.card3.SetRandomUpgrade();

        LevelUIManager.Ins.cardsContainer.KH_Show_Pop();
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

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
