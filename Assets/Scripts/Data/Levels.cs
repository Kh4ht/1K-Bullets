using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Lvl", menuName = "Scriptable Objects/New Level")]
public class Level : ScriptableObject
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region INSPECTOR FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public SceneAsset scene;
    public string levelName;

    #endregion
}
