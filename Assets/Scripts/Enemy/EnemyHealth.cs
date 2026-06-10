public class EnemyHealth : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public EnemyHealth(Enemy newOwner, SliderController healthBar)
    {
        _owner = newOwner;
        _healthBar = healthBar;
    }

    private readonly Enemy _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public KHHealthController HealthCrtl { get; private set; }

    private readonly SliderController _healthBar;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IOnEnable()
    {
        HealthCrtl.AddOnHealthChangedListener(OnHealthChanged);
        HealthCrtl.AddOnHealthDecreaseListener(OnHealthDecreased);
        HealthCrtl.AddOnMaxHealthReachedListener(OnMaxHealth);
        HealthCrtl.AddOnDeathListener(OnDeath);
        HealthCrtl.AddOnReviveListener(OnRevive);
    }
    public void IOnDisable()
    {
        HealthCrtl.RemoveOnHealthChangedListener(OnHealthChanged);
        HealthCrtl.RemoveOnHealthDecreaseListener(OnHealthDecreased);
        HealthCrtl.RemoveOnMaxHealthReachedListener(OnMaxHealth);
        HealthCrtl.RemoveOnDeathListener(OnDeath);
        HealthCrtl.RemoveOnReviveListener(OnRevive);
    }

    public void IAwake()
    {
        HealthCrtl = new(_owner, _owner.Data.DefaultMaxHealth, _owner.Data.DefaultMaxHealth);
    }

    public void IStart()
    {
        _healthBar.gameObject.SetActive(false);
    }

    public void IUpdate() { }
    public void IFixedUpdate() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void OnHealthChanged()
    {
        _healthBar.UpdateSlider(_owner.EHealth.HealthCrtl.MaxHealth, _owner.EHealth.HealthCrtl.Health);
    }

    private void OnMaxHealth()
    {
        _healthBar.gameObject.SetActive(false);
    }

    private void OnHealthDecreased()
    {
        _healthBar.gameObject.SetActive(true);
    }

    private void OnDeath()
    {
        ExpGem.GetOrCreateNew(_owner.transform.position, _owner.Data.ExpGemData);
        _healthBar.gameObject.SetActive(false);
        LevelManager.Ins.EnemyKilled();

        _owner.DisableEnemy();
    }

    private void OnRevive()
    {
        _healthBar.gameObject.SetActive(false);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}
