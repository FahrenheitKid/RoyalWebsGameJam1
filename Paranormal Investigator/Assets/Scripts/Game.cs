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

      public static Point minMaxWeapons = new Point(1,2);
      public static Point minMaxPlaces = new Point(1,2);

      [SerializeField]
      MonsterDataObject monsterDataObject;
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

      [Header("Answer Panel")]
    [SerializeField]
    CanvasGroup answerGroup;

    [SerializeField]
    Monster answerMonster;



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
         TextMeshProUGUI questionCountText;
         [SerializeField]
         int questionMAX = 4;
         [SerializeField]
         float ghostCostPercentage = 0.5f;
         [SerializeField]
         int questionCount = 0;

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
        List<int> excludeMonsterList = new List<int>{ MonsterCharacter.Ghost.EnumToInt() };
        List<MonsterCharacter> characters = UtilityTools.GetUniqueEnums<MonsterCharacter>(monsters.Count,excludeMonsterList);
        List<int> namesIndex = UtilityTools.GetUniqueRandomNumbers(0,MonsterDataObject.randomNames.Count,monsters.Count);

        int monsterIndex = 0;
        while(characters.Any() && monsterIndex < monsters.Count)
        {
            int charIndex = Random.Range(0,characters.Count);


           monsters[monsterIndex].BuildCharacter(monsterDataObject.GetMonsterData(characters[charIndex]),
           UtilityTools.GetUniqueEnums<Weapon>(Random.Range(minMaxWeapons.x,minMaxWeapons.y +1 )),
           UtilityTools.GetUniqueEnums<Place>(Random.Range(minMaxWeapons.x,minMaxWeapons.y +1)),
           MonsterDataObject.randomNames.ElementAtOrDefault(namesIndex.ElementAtOrDefault(charIndex)));
            
            monsterIndex++;
            characters.RemoveAt(charIndex);
            namesIndex.RemoveAt(charIndex);
            
        }
        /*

        for(int i = 0; i < monsters.Count && i <characters.Count; i++)
        {

            monsters[i].BuildCharacter(monsterDataObject.GetMonsterData(characters[i]),null,null);
        }
        */
       

        gameClock = Timer.Register(secondsPerHour, () => {
            
                if(hours < 12)
                {
                    hours++;
                    
                }
                else
                {
                   if(ampm == "pm" && hours >=12)
                     NightTurn();

                    hours = 1;
                    

                    ampm = (ampm == "am") ? "pm" : "am";
                     
                }

                UpdateClock();
            
            },null,true);

            SetupNewRound();
    }

    public void SetupNewRound()
    {
         assassin = monsters[Random.Range(0,monsters.Count)];
         
        if(answerMonster)
        {
            answerMonster.BuildCharacter(monsterDataObject.GetMonsterData(assassin.monsterData.monster),assassin.weapons,assassin.places,assassin.monsterName);
        }

         if(gameClock.isPaused)
         {
             hours = 6;
            ampm = "am";
             gameClock.Resume();
         }
        
         questionCount = 0;
         UpdateQuestionText();
    }

    public bool CanAsk()
    {
        if(questionCount < questionMAX)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanInterrogate()
    {
        if(Mathf.Abs(questionCount - questionMAX) >= (questionMAX * ghostCostPercentage) && questionCount < questionMAX)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void IncreaseQuestionCount(bool isAsk)
    {
        if(isAsk)
        {
            questionCount++;
        }
        else
        {
            questionCount+= Mathf.CeilToInt((float)questionMAX * ghostCostPercentage);
        }

        UpdateQuestionText();
    }

     void UpdateQuestionText()
    {
        int monstersCount = questionCount < questionMAX ? Mathf.Abs(questionMAX - questionCount) : 0;
         float ghostCount = questionCount < questionMAX ? (float)Mathf.Abs(questionCount -questionMAX) / ((float)questionMAX * ghostCostPercentage) : 0;

        questionCountText.text = " You can ask " + monstersCount.ToString() + " Monsters or " + Mathf.Floor(ghostCount) + " Ghosts";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowAnswerMonster()
    {
        if(answerGroup)
        {
            answerGroup.gameObject.SetActive(!answerGroup.gameObject.activeSelf);
            
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
       
        if(monsters.Where(x => x.isDead == true).Count()>= monsters.Count)
        {
            print("all ded");
            return;
        }

        if(hasSacrificed == false)
        {
            currentSacrifice = assassin;
            while(currentSacrifice == assassin || currentSacrifice.isDead == true)
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
        UpdateClock();
         gameClock.Resume();
         questionCount = 0;
         UpdateQuestionText();

         foreach(Monster m in monsters)
         {
             if(m)
             {
                 m.wasAsked = false;
             }
         }

         print("A new day Has begun");
    }

    public void HideOtherPopUps(Monster current)
    {
        foreach(Monster m in monsters)
        {
            if(m != current && m)
            {
                if(m.isQuestionPopUpOn)
                {
                    m.HideAllPopUps(m.isSacrifice);
                }
            }
        }
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
            hasSacrificed = false;
        }
        else
        {
            if(currentSacrifice)
            {
                
            currentSacrifice.Sacrifice(false);
            hasSacrificed = false;
            }
            currentSacrifice = m;
             if(currentSacrifice)
             {
                 
            
                hasSacrificed = currentSacrifice.Sacrifice(true);
             }
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