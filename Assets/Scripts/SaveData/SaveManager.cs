using System.Collections;
using KH;
using NaughtyAttributes;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private static SaveManager Ins;

    public static SaveData GameSaveData;

    private Coroutine autoSaveRoutine;

    private static bool isDirty;
    private static float lastSaveTime;
    private static float saveCooldown = 1f;

    // Properties
    public static int Gold
    {
        get => GameSaveData.gold;
        set
        {
            GameSaveData.gold = value;
            MarkDirty();
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    [Tooltip("Enable periodic autosave")]
    public bool enableAutoSave = true;

    [ShowIf("enableAutoSave"), Tooltip("Save every N seconds when enabled")]
    public float autoSaveInterval = 30f;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void Awake()
    {
        if (Ins != null)
        {
            Destroy(gameObject);
            return;
        }

        Ins = this;
        DontDestroyOnLoad(gameObject);

        LoadGame();
    }
    private void OnEnable()
    {
        if (enableAutoSave && autoSaveRoutine == null)
            autoSaveRoutine = StartCoroutine(AutoSaveCoroutine());
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private IEnumerator AutoSaveCoroutine()
    {
        while (enableAutoSave)
        {
            yield return new WaitForSeconds(autoSaveInterval);
            SaveGame();
            Debug.Log($"[{nameof(SaveManager)}] Auto -saved.");
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    [Button]
    private void ManualSave()
    {
        SaveGame();
        KHDebug.Log($"[{nameof(SaveManager)}] Manually-saved.");
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static void SaveGame(bool force = false)
    {
        if (!force && !isDirty)
            return;

        if (Time.time - lastSaveTime < saveCooldown)
            return;

        lastSaveTime = Time.time;

        GameSaveData ??= new SaveData();
        KHSaveSystem.Save(GameSaveData);

        isDirty = false;
    }

    public static void LoadGame()
    {
        GameSaveData = KHSaveSystem.Load<SaveData>();
        GameSaveData ??= new SaveData();
    }

    public static void MarkDirty()
    {
        isDirty = true;
    }

    #endregion
}