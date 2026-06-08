using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerScreenUIManager : MonoBehaviour
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    // Static
    public static PlayerScreenUIManager Ins { get; private set; }

    // Getters

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [Header("TEXT")]
    [SerializeField] private TextMeshProUGUI goldTxt;
    [SerializeField] private TextMeshProUGUI redCrystalsTxt;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void OnEnable()
    {
        GameManager.Currency.AddOnGoldChangedListener(UpdateGoldTxt);
        GameManager.Currency.AddOnRedCrystalsChangedListener(UpdateRedCrystalsTxt);
    }

    private void OnDisable()
    {
        GameManager.Currency.RemoveOnGoldChangedListener(UpdateGoldTxt);
        GameManager.Currency.RemoveOnRedCrystalsChangedListener(UpdateRedCrystalsTxt);
    }

    private void Awake()
    {
        if (Ins == null)
            Ins = this;
        else
            Destroy(gameObject);

        // DontDestroyOnLoad(this);
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████



    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public void UpdateGoldTxt(int gold)
    {
        goldTxt.SetText(gold.ToString("N0"));
    }

    public void UpdateRedCrystalsTxt(int redCrystals)
    {
        redCrystalsTxt.SetText(redCrystals.ToString("N0"));
    }

    #endregion
}
