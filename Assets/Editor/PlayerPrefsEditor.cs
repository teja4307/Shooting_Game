using UnityEditor;
using UnityEngine;

public class PlayerPrefsEditor : EditorWindow
{
    [MenuItem("Tools/PlayerPrefs Manager")]
    public static void ShowWindow()
    {
        GetWindow<PlayerPrefsEditor>("PlayerPrefs Manager");
    }

    private void OnGUI()
    {
      //  GUILayout.Label("PlayerPrefs Manager", EditorStyles.boldLabel);

        if (GUILayout.Button("Delete All PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("All PlayerPrefs have been deleted.");
            /* if (EditorUtility.DisplayDialog("Delete All PlayerPrefs",
                                             "Are you sure you want to delete all PlayerPrefs? This action cannot be undone.",
                                             "Yes", "No"))
             {
                 PlayerPrefs.DeleteAll();
                 PlayerPrefs.Save();
                 Debug.Log("All PlayerPrefs have been deleted.");
             }*/
        }
    }
}
