using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

/// <summary>
/// Extended from MISoundManger which provides the basic fucntionalies for managing game sounds.
/// Implements other game specific sound methods
/// </summary>
public class SoundManager : PnCSoundManger
{

    public static SoundManager m_instance;

    /// <summary>Singleton Instance </summary>
    public static SoundManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<SoundManager>();
            }
            return m_instance;
        }
    }

    //Game's sound state
    private bool isSoundOn = true;

    void Start()
    {
        //get the last stored sound setting
        // isSoundOn = PlayerData.Instance.soundSetting;
        applySoundSetting();
    }

    /// <summary>
    /// Toggles the sound on off and saves to storage
    /// </summary>
    public void toggleSoundOnOff()
    {
        // playSound(AudioClips.UI);
        isSoundOn = !isSoundOn;
        applySoundSetting();
        // PlayerData.Instance.soundSetting = isSoundOn;
        // PlayerData.Instance.SaveData();
    }

    /// <summary>
    /// Applies the sound setting.
    /// </summary>
    void applySoundSetting()
    {
        AudioListener.volume = isSoundOn ? 1 : 0;
        // UIManager.Instance.ToggleSoundSprite(isSoundOn);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PnCSoundManger), true)]
public class SoundManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PnCSoundManger soundManager = (PnCSoundManger)target;

        EditorGUILayout.LabelField("");
        EditorGUILayout.HelpBox("After assigning Audio clips, Hit this button to get an enum of audioclip names created/updated. Makes method calls easy.\n ** NOTE : Make sure the name is a valid enum name **\n" +
                                " Example - SoundManager.Instance.playSound(AudioClips.perfect);", MessageType.Info);

        if (GUILayout.Button("Generate Audioclip Names Enum"))
        {
            GenerateAudioClipsEnum();
        }
    }

    /// <summary>
    /// Creates the audioClips enum at the soundManager's location
    /// </summary>
    public void GenerateAudioClipsEnum()
    {
        //Get the list of audios
        PnCSoundManger soundManager = (PnCSoundManger)target;
        List<Audio> audios = soundManager.audios;

        //Get the script's path
        MonoScript thisScript = MonoScript.FromMonoBehaviour(soundManager);
        string ScriptFilePath = AssetDatabase.GetAssetPath(thisScript);

        //Create a path for the enum file
        string enumName = "AudioClips";
        string filePathAndName = ScriptFilePath.Replace(thisScript.name + ".cs", "") + "/" + enumName + ".cs";

        //Wrire the enum at above path
        using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
        {
            streamWriter.WriteLine("public enum " + enumName);
            streamWriter.WriteLine("{");
            for (int i = 0; i < audios.Count; i++)
            {
                streamWriter.WriteLine("\t" + audios[i].audioName + ",");
            }
            streamWriter.WriteLine("}");
        }
        AssetDatabase.Refresh();

        Debug.Log("Audioclips  enum created/updated at " + ScriptFilePath);

    }
}
#endif