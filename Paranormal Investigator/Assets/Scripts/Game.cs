using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Common.Enums;
using DG.Tweening;
using TMPro;

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
      [SerializeField]
      FlexibleGridLayout monsterGridGroup;
      [SerializeField]
      public Color greenColor;
      [SerializeField]
      public Color redColor;
      [SerializeField]
      public Color brownColor;

      [SerializeField]
      public Color membersHighlightColor;
      [SerializeField]
      public Color weaponHighlightColor;
       [SerializeField]
      public Color placeHighlightColor;


      [Header("Game Logic")]

      [SerializeField]
      int dayCount = 1;
        [SerializeField]
        bool hasSacrificed = false;
        [SerializeField]
        public bool isSacrificeMode =false;
        [SerializeField]
        Button sacrificeModeButton;
        [SerializeField]
        TextMeshProUGUI sacrificeButtonText;
        [SerializeField]
        Material [] sacrificeTextMats;
        [SerializeField]
        public bool isPaused = false;

        [SerializeField]
        List<Monster> monsters = new List<Monster>();
        [SerializeField]
        Monster assassin;
        [SerializeField]
         Monster currentSacrifice;
         [SerializeField]
         int questionMAX;
         [SerializeField]
         int questionCount;

         [SerializeField]
         TextMeshProUGUI clockText;
         [SerializeField]
         int hours = 6;
         [SerializeField]
         float secondsPerHour = 20;
         [SerializeField]
         string ampm = "am";
         [SerializeField]
         Timer gameClock;


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

        foreach(Transform child in monsterGridGroup.transform)
        {
            Monster m = child.GetComponent<Monster>();


            if(m) 
            {
                monsters.Add(m);
            }
        }

       

        gameClock = Timer.Register(secondsPerHour, () => {
            
                if(hours < 12)
                {
                    hours++;
                    
                }
                else
                {
                   
                    hours = 1;
                    ampm = (ampm == "am") ? "pm" : "am";
                     NightTurn();
                     
                }

                UpdateClock();
            
            },null,true);

            SetupNewRound();
    }

    public void SetupNewRound()
    {
         assassin = monsters[Random.Range(0,monsters.Count)];
         


         if(gameClock.isPaused)
         {
             hours = 6;
            ampm = "am";
             gameClock.Resume();
         }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    private void UpdateClock()
    {
        clockText.text =  (hours == 0) ? 12.ToString("00") :hours.ToString("00") + "" + /*minutes.ToString("00") + */ " " +  ampm;
        
    }

    public void NightTurn()
    {
        isPaused = true;
        gameClock.Pause();
       

        if(hasSacrificed == false)
        {
            currentSacrifice = assassin;
            while(currentSacrifice == assassin)
            {
                currentSacrifice = monsters[Random.Range(0,monsters.Count)];
            }

        }

        currentSacrifice.Assassinate(assassin);
        currentSacrifice = null;
        hasSacrificed = false;


        DayReveal();

    }

    public void DayReveal()
    {

        isPaused = false;
         hours = 6;
        ampm = "am"; 
         gameClock.Resume();

         print("A new day Has begun");
    }

     public static Tween FadeImage(Image image, bool on, float fadeDuration, Vector2 normalPosition, bool forceReposition = false)
    {

         image.DOFade(on ? 1 : 0, fadeDuration);

         if(forceReposition)
         {
             
              image.rectTransform.localPosition = (on) ?  new Vector2(normalPosition.x,normalPosition.y - 50f) : normalPosition;
         }
        
    return image.rectTransform.DOLocalMove(on ? normalPosition : new Vector2(normalPosition.x,normalPosition.y - 50f), fadeDuration);
        
    }

    public static Tween FadeGroup(CanvasGroup image, bool on, float fadeDuration, float normalY =0)
    {

         image.DOFade(on ? 1 : 0, fadeDuration);
    
        return image.transform.DOLocalMoveY(on ? normalY :normalY - 50f, fadeDuration);
        
    }

    public void SetSacrifice(Monster m)
    {
        if(currentSacrifice == m && m != null)
        {
            currentSacrifice.Sacrifice(false);
            currentSacrifice = null;
        }
        else
        {
            if(currentSacrifice)
            currentSacrifice.Sacrifice(false);
            currentSacrifice = m;
             if(currentSacrifice)
            currentSacrifice.Sacrifice(true);
        }

    }

    public void SetSacrificeMode()
    {
        isSacrificeMode = !isSacrificeMode;
        sacrificeButtonText.material = (isSacrificeMode) ? sacrificeTextMats.FirstOrDefault() : sacrificeTextMats.LastOrDefault();
        sacrificeButtonText.color = (isSacrificeMode)  ? Color.black : brownColor;
        sacrificeButtonText.text = (isSacrificeMode) ? "Sacrifice Mode ON" : "Sacrifice Mode OFF";
        sacrificeModeButton.image.color = (isSacrificeMode) ? redColor : Color.white;
    }

    public static string GetColoredString(string text, Color color)
    {
        ColorUtility.ToHtmlStringRGB(color);
        return "<color=#" +  ColorUtility.ToHtmlStringRGB(color) +"> " + text + " </color>"; 
    }
}