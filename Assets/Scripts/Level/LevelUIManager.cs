using System;
using KH;
using Michsky.UI.MTP;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class LevelUIManager : MonoBehaviour
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Static
    public static LevelUIManager Ins { get; private set; }

    // Getters
    public StyleManager DeathTitle => _deathTitle;
    public StyleManager WinTitle => _winTitle;
    public KHUI LoseMenu => _loseMenu;
    public KHUI WinMenu => _winMenu;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [Header("SLIDER")]
    [SerializeField] private SliderController _playerHealthSlider;
    [SerializeField] private SliderController _playerExpSlider;

    [HorizontalLine, Header("TEXT")]
    [SerializeField] private TextMeshProUGUI _clockTxt;
    [SerializeField] private TextMeshProUGUI _killsCounterTxt;
    [SerializeField] private TextMeshProUGUI _expTxt;

    [HorizontalLine, Header("STYLE MANAGER")]
    [SerializeField] private StyleManager _deathTitle;
    [SerializeField] private StyleManager _winTitle;

    [HorizontalLine, Header("MENU")]
    [SerializeField] private KHUI _loseMenu;
    [SerializeField] private KHUI _winMenu;

    [HorizontalLine, Header("UPGRADE CARDS")]
    public KHUI cardsContainer;
    public UpgradeCard card1;
    public UpgradeCard card2;
    public UpgradeCard card3;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void OnEnable()
    {
        LevelManager.Ins.LMExperience.OnExpLevelChanged += UpdateExpLevelTxt;
        LevelManager.Ins.LMExperience.OnExpPointsChanged += UpdateExpPointsSlider;
    }

    private void OnDisable()
    {
        LevelManager.Ins.LMExperience.OnExpLevelChanged -= UpdateExpLevelTxt;
        LevelManager.Ins.LMExperience.OnExpPointsChanged -= UpdateExpPointsSlider;
    }

    private void Awake()
    {
        if (Ins == null)
            Ins = this;
        else
            Destroy(gameObject);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void UpdatePlayerHealthSlider(int maxHealth, float health)
    {
        _playerHealthSlider.UpdateSlider(maxHealth, health);
    }
    public void UpdateExpPointsSlider(int maxValue, int expPoints)
    {
        _playerExpSlider.UpdateSlider(maxValue, expPoints);
    }

    public void UpdateLevelClockTxt(float timerSeconds)
    {
        _clockTxt.text = TimeSpan.FromSeconds(timerSeconds).ToString(@"m\:ss");
    }

    public void UpdateExpLevelTxt(int exp)
    {
        _expTxt.text = exp.ToString();
    }

    public void UpdateKillsCounterTxt(int counter)
    {
        _killsCounterTxt.text = counter.ToString("n0");
    }

    #endregion
}
