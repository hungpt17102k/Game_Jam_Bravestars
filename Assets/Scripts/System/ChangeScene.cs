#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;


public class ChangeScene : Editor
{

    [MenuItem("Open Scene/Game Scene")]
    public static void OpenGame()
    {
        OpenGameScene("GameScene");
    }

    [MenuItem("Open Scene/Example Scene")]
    public static void OpenExample()
    {
        OpenExampleScene("Example Scene");
    }

    private static void OpenGameScene(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity");
        }
    }

    private static void OpenExampleScene(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/ToonWater/" + sceneName + ".unity");
        }
    }
    
}
#endif