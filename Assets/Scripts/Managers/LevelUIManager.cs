using System;
using KH;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class LevelUIManager : MonoBehaviour, IUpdateObserver
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static LevelUIManager Ins { get; private set; }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [SerializeField] private SliderController _playerHealthSlider;
    [SerializeField] private TextMeshProUGUI _clockTxt;
    [SerializeField] private TextMeshProUGUI _killsCounterTxt;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void OnEnable()
    {
        UpdateManager.RegisterObserver(this);
    }

    private void OnDisable()
    {
        UpdateManager.UnregisterObserver(this);
    }
    void Awake()
    {
        if (Ins == null)
            Ins = this;
        else
            Destroy(gameObject);
    }

    public void OUpdate() { }

    public void OFixedUpdate() { }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void UpdatePlayerHealthSlider(int maxHealth, float health)
    {
        _playerHealthSlider.UpdateSlider(maxHealth, health);
    }

    public void UpdateLevelClockTxt(float timerSeconds)
    {
        _clockTxt.text = TimeSpan.FromSeconds(timerSeconds).ToString(@"m\:ss");

    }

    public void UpdateKillsCounterTxt(int counter)
    {
        _killsCounterTxt.text = counter.ToString("n0");

    }

    #endregion
}
