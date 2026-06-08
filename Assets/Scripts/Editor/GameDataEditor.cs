using UnityEditor;

[CustomEditor(typeof(GameData))]
public class GameDataBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var db = (GameData)target;

        if (!EditorApplication.isPlayingOrWillChangePlaymode)
        {
            db.upgradeCardDatas.AutoFillDataBase();

            EditorUtility.SetDirty(db);
        }
    }
}