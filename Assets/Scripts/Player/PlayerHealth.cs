public class PlayerHealth : IKHIUnityMethods
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region CONSTRUCTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public PlayerHealth(Player newOwner, int maxHealth)
    {
        _owner = newOwner;
        _maxHealth = maxHealth;
    }

    private readonly Player _owner;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public KHHealthController HealthCrtl { get; private set; }
    private int _maxHealth;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void IOnEnable()
    {
        HealthCrtl.AddOnHealthChangedListener(OnHealthChanged);
    }
    public void IOnDisable()
    {
        HealthCrtl.AddOnHealthChangedListener(OnHealthChanged);
    }

    public void IAwake()
    {
        HealthCrtl = new(_maxHealth, _maxHealth);

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

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
}
