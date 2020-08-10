using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Common.Enums;

public class Game : MonoBehaviour
{

    public static string gameScene = "Game";
    public static string mapEditorScene = "Map Editor";
    public static string mapSelectScene = "Map Select";
    public static string mainMenuScene = "Main Menu";
    public static string editorMapSelectScene = "Editor Map Select";
      public static string emptyScene = "Empty Scene";

        public static float infoPanelDefaultPosX = 405f;
      public static float weaponDefaultWidth = 120f;
      public static float placeDefaultWidth = 100f;
      public static float infoDefaultHeight = 450f;
      public static float monsterSpriteSizeMultiplier = 2.5f;
    [SerializeField]
    Texture2D cursorTexture;
    [SerializeField]
      CursorMode cursorMode = CursorMode.Auto;
      [SerializeField]
     Vector2 hotSpot = Vector2.zero;
    [SerializeField]
    bool useCustomCursor;
      public GameObject placePrefab;
      public GameObject weaponPrefab;

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

        if(useCustomCursor)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}