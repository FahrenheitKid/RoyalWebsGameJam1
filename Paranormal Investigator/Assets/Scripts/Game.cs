using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    public static string gameScene = "Game";
    public static string mapEditorScene = "Map Editor";
    public static string mapSelectScene = "Map Select";
    public static string mainMenuScene = "Main Menu";
    public static string editorMapSelectScene = "Editor Map Select";
      public static string emptyScene = "Empty Scene";
    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR
        if(SceneManager.GetActiveScene().name == emptyScene)
        {
            UnityEditor.EditorUtility.UnloadUnusedAssetsImmediate();
            print("finished unloading");
        }
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad8))
        {
             print("load game scene");
            SceneManager.LoadScene(gameScene);
        }

         if(Input.GetKeyDown(KeyCode.Keypad9))
        {
            print("load empty scene");
              SceneManager.LoadScene(emptyScene,LoadSceneMode.Single);
        }
    }
}