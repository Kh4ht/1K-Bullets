using KH;
using UnityEngine;

public class PlayerHealth : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public PlayerHealth(Player newOwner)
    {
        _owner = newOwner;
    }

    private readonly Player _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public KHHealthController HealthCrtl { get; private set; }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IOnEnable()
    {
        HealthCrtl.AddOnHealthChangedListener(OnHealthChanged);
        HealthCrtl.AddOnDeathListener(OnDeath);
        HealthCrtl.AddOnMaxHealthChangedListener(OnMaxHealthChanged);
    }
    public void IOnDisable()
    {
        HealthCrtl.AddOnHealthChangedListener(OnHealthChanged);
        HealthCrtl.RemoveOnDeathListener(OnDeath);
        HealthCrtl.RemoveOnMaxHealthChangedListener(OnMaxHealthChanged);
    }

    public void IAwake()
    {
        HealthCrtl = new(_owner, _owner.Data.DefaultMaxHealth, _owner.Data.DefaultMaxHealth);
    }

    public void IStart()
    {
        LevelUIManager.Ins.UpdatePlayerHealthSlider(HealthCrtl.MaxHealth, HealthCrtl.Health);
    }

    public void IUpdate() { }
    public void IFixedUpdate() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void OnHealthChanged()
    {
        LevelUIManager.Ins.UpdatePlayerHealthSlider(HealthCrtl.MaxHealth, HealthCrtl.Health);
    }

    private void OnDeath()
    {
        Enemy.StopAllEnemiesFromMoving();

        LevelManager.Ins.Lost();

        _owner.Rb2d.linearVelocity = Vector2.zero;
    }

    private void OnMaxHealthChanged()
    {
        LevelUIManager.Ins.UpdatePlayerHealthSlider(HealthCrtl.MaxHealth, HealthCrtl.Health);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}
