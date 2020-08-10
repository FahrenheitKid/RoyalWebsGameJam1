using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using Common.Enums;

namespace Common.Enums
{
    public enum Weapon
    {
        Hammer,
        Wrench,
        Backsaw,
        Scythe,
        Shovel,
        Pitchfork,
        Knife,
        Crowbar,
        Screwdriver,
        Axe,
        Cleaver,
        UtilityKnife,
        Duster,
        Pickaxe,
        WateringCan,
        Cultivator,
        Pliers,
        Broom,
        Scraper,
        PipeWrench,
        TapeMeasure,
        Gouge
    }
     public enum WeaponFeature
    {
        Blade,
        Piercing,
        Gardening,
        Construction,
        Harmless,
        Blunt
    }

    public enum Place
    {
        House,
        Barn,
        Building,
        Stadium,
        GasStation,
        Shop,
        Bank,
        Hospital,
        Port,
        Airport,
        BusStop,
        Warehouse,
        Theater,
        Church,
        Graveyard,
        Factory,
        NuclearPlant,
        Castle,
        Park,
        Lighthouse,
        Windmill,
        Vulcan,

    }

    public enum PlaceFeature
    {
        Indoors,
        Outdoors,
        Busy,
        Calm,
        Common,
    }

   

    public enum Element
    {
        Earth,
        Fire,
        Air,
        Dark
    }

    public enum Question{

        who,
        where,
        how
    }
    public enum MonsterCharacter
    {
        AirSlime,
        EarthKoala,
        EarthGoomba,
        EarthTrunk,
        EarthTree,
        EarthGolem,
        EarthPumpkin,
        Earthling,
        EarthKing,
        FireSlime,
        FireSlima,
        FireSlimaga,
        FireTank,
        Ghost,
        FireKing,
        FireAlien,
        AirBird,
        AirBirda,
        AirBirdling,
        AirBirdaga,
        AirEgg,
        AirBirdaja,
        AirOwl,
        DarkSlime,
        DarkPig,
        DarkBat,
        DarkGhoul,
        DarkWizard,
        DarkImp,
        DarkDemon,
        DarkBrain

    }

}

public struct Autopsy
{
    public Weapon weapon;
    public Place place;
    public MonsterCharacter character;
    public bool deadAssassin;

    public Autopsy(Weapon weapon, Place place, bool deadAssassin, MonsterCharacter character)
    {
        this.weapon = weapon;
        this.place = place;
        this.deadAssassin = deadAssassin;
        this.character = character;
    }
}
public class Monster : MonoBehaviour
{
    [SerializeField]
    public Game game_ref;
    [SerializeField]
    Button button;
     [SerializeField]
    Image image;
    [SerializeField]
    CanvasGroup infoGroup;
    [SerializeField]
    Image infoPanel;
    [SerializeField]
    TextMeshProUGUI title;
    [SerializeField]
    Image titlePanel;

    [SerializeField]
    CanvasGroup questionMark;
    [SerializeField]
    CanvasGroup exclamationMark;
    [SerializeField]
    CanvasGroup talkBubble;
    [SerializeField]
    TextRevealer talkText;
     [SerializeField]
    Animator animator;
     [SerializeField]
    FlexibleGridLayout weaponParent;
     [SerializeField]
    FlexibleGridLayout placeParent;

    [SerializeField]
    MonsterData monsterData;
    [SerializeField]
    List<Weapon> weapons = new List<Weapon>();
    [SerializeField]
    List<Place> places = new List<Place>();
    [SerializeField]
    Autopsy autopsy;
    [SerializeField]
    string answer;
    [SerializeField]

    public static float titlePanelDefaultPaddingY = 70f;
    public static float infoPanelDefaultPaddingX = 200f;
    public static float infoShowDuration = 0.5f;
    [SerializeField]
    SpritesHolder spritesHolder;
    [SerializeField]
    MonsterDataObject monsterDataHolder;

    public bool useFade = true;

    public bool isSacrifice = false;
    public bool isDead = false;
    public bool wasAsked = false;
    public bool wasInterrogated = false;


    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }
    public void Setup()
    {
       
        if(!game_ref)
        game_ref = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();

        if(spritesHolder)
        {
            foreach(Weapon wp in weapons)
            {
                GameObject go = spritesHolder.InstantiatePrefab(wp);
                if(go)
                {
                    go.transform.SetParent(weaponParent.transform);
                }
            }

             foreach(Place pl in places)
            {
                GameObject go = spritesHolder.InstantiatePrefab(pl);
                if(go)
                {
                    go.transform.SetParent(placeParent.transform);
                }
            }
        }

        ResizeSprite();
         ResizeInfoPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickTest()
    {
        print("click test");
    }

    public static Element GetElement(MonsterCharacter character)
    {
        string enumName = System.Enum.GetName(typeof(MonsterCharacter),character);
        enumName = enumName.ToLower();

        if(enumName.FirstOrDefault() == 'e')
        {
            return Element.Earth;
        }
        else if(enumName.FirstOrDefault() == 'a')
        {
            return Element.Air;
        }
        else if(enumName.FirstOrDefault() == 'f')
        {
            return Element.Fire;
        }
        else// if(enumName.FirstOrDefault() == 'd')
        {
            return Element.Dark;
        }
    }

    public void ResizeInfoPanel()
    {
        bool weaponsBigger = Game.weaponDefaultWidth > Game.placeDefaultWidth;
        bool moreWeapons = weapons.Count > places.Count;

        float cellWidth = moreWeapons ?  Game.weaponDefaultWidth : Game.placeDefaultWidth;

        if(weapons.Count == places.Count )
        {
            
            cellWidth = weaponsBigger ?   Game.weaponDefaultWidth : Game.placeDefaultWidth;
            if(weapons.Count > 1|| places.Count > 1)
            {
                cellWidth += moreWeapons ? weaponParent.spacing.x : placeParent.spacing.x;
            }
        }

        infoPanel.rectTransform.sizeDelta = new Vector2( cellWidth * ((moreWeapons ? weapons.Count : places.Count) + 1.5f),Game.infoDefaultHeight);

      
    }

    public void ResizeSprite()
    {
       image.rectTransform.sizeDelta = new Vector2( image.sprite.rect.width,  image.sprite.rect.height)* Game.monsterSpriteSizeMultiplier;

    }

    public void  ShowBasicInfo(bool on)
    {
        infoGroup.DOFade(on ? 1 : 0, infoShowDuration);
        infoGroup.transform.DOLocalMoveY(on ? 0 : -50f, infoShowDuration);
    }

    public void ShowQuestionMark(bool on)
    {
        //Game.FadeImage(questionMark,on,infoShowDuration, Vector2.zero);
        questionMark.DOFade(on ? 1 : 0, infoShowDuration);
        questionMark.transform.DOLocalMoveY(on ? 0 : -50f, infoShowDuration);
    }

     public void ShowExclamationMark(bool on)
    {
         exclamationMark.DOFade(on ? 1 : 0, infoShowDuration);
        exclamationMark.transform.DOLocalMoveY(on ? 0 : -50f, infoShowDuration);
    }

    public void ShowTalkBubble(bool on)
    {
        Tween t = Game.FadeGroup(talkBubble,on,infoShowDuration);
        if(on)
        {   
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(GetRandomSacrificeText());
            queue.Enqueue(GetRandomSacrificeText());
            talkText.SetQueue(queue);
            
        t.onComplete+= () => talkText.RevealText();
        }
        else
        t.onComplete+= () => talkText.StopRevealing();
    }

   
    public void SetSacrifice()
    {
        if(game_ref)
        {
            game_ref.SetSacrifice(this);
        }
    }
    public void Sacrifice(bool on)
    {
        if(isDead && on)
        {
            print("cant be accused");
            return;
        }
        else
        {
            ShowExclamationMark(on);
            ShowTalkBubble(on);
            if(!on)
            ShowQuestionMark(false);
            isSacrifice = on;
        }
    }

    public void Assassinate(Monster assassin)
    {
        autopsy.place = Random.value  <= 0.5 ? assassin.places[Random.Range(0,assassin.places.Count)] : places[Random.Range(0,places.Count)];
        autopsy.weapon = assassin.weapons[Random.Range(0,assassin.weapons.Count)];
        autopsy.deadAssassin = assassin.isDead;

        image.sprite = spritesHolder.GetMonsterSpriteAndAnimator(MonsterCharacter.Ghost)?.sprite;
        animator = spritesHolder.GetMonsterSpriteAndAnimator(MonsterCharacter.Ghost)?.animator;

         ResizeSprite();
         ResizeInfoPanel();
    }


    public void ActionClick()
    {
        if(isDead == false )
        {
            if(game_ref.isSacrificeMode == true)
            {
                 SetSacrifice();
            }
            else
            {
                if(wasAsked == false)
                {
                    Ask();
                   
                }
            }   
           
        }
        else 
        {
            
        }
    }


    public void Ask()
    {
         wasAsked = true;
          ShowQuestionMark(false);
          ShowBasicInfo(true);
    }

    public string GetRandomSacrificeText()
    {
        List<string> sacrificeTexts = new List<string> {
            "Don't kill me please!!!1!",
            "Do you always solve your cases by randonly killing people?",
            "If you kill me, I'll wait for you in hell (:"
        };

        return sacrificeTexts[Random.Range(0, sacrificeTexts.Count)];
    }

    public static List<PlaceFeature> GetFeatures(Place place)
    {
        List<PlaceFeature> features = new List<PlaceFeature>();

        return features;
    }

    public static List<WeaponFeature> GetFeatures(Weapon weapon)
    {
        List<WeaponFeature> features = new List<WeaponFeature>();

        return features;
    }

    public string GetFeatureString(PlaceFeature place)
    {
        return System.Enum.GetName(typeof(PlaceFeature),place);
    }

    public string GetFeatureString(WeaponFeature weapon)
    {
          return System.Enum.GetName(typeof(WeaponFeature),weapon);
    }
      public string GetAnswer(Question question, Autopsy autopsy, MonsterDataObject monsterDataObj)
    {

        
            switch(question)
            {
                case Question.who:

                if(autopsy.deadAssassin)
                {
                    if(Random.value < 0.5f)
                    {
                        return "I was killed by another ghost";
                    }
                    
                }

                bool hasHands = monsterDataObj.HasHands(autopsy.character); 
                bool hasFeet = monsterDataObj.HasFeet(autopsy.character); 

                if(hasHands && hasFeet)
                {
                    return Random.value <= 0.5f ? "On the floor, I could see that it had " +  Game.GetColoredString("feet", game_ref.membersHighlightColor) : 
                    "If it wasn't for his " + Game.GetColoredString("feet", game_ref.membersHighlightColor) + " , I would have make it...";

                } else if( hasHands || hasFeet)
                {
                    if(Random.value <= 0.5f)

                     return hasFeet ? "On the floor, I could see that it had " +  Game.GetColoredString("feet", game_ref.membersHighlightColor) : 
                    "If it wasn't for his " + Game.GetColoredString("feet", game_ref.membersHighlightColor) + " , I would have make it...";
                }


                return "I remember it clearly, he soul is made of " + System.Enum.GetName(typeof(Element),  Monster.GetElement(autopsy.character));
                

                break;

                case Question.where:

                List<PlaceFeature> placeFeats = GetFeatures(autopsy.place);
                if(placeFeats.Any())
                return "I could say that place is " + GetFeatureString(placeFeats[Random.Range(0,placeFeats.Count)]);

                break;

                case Question.how:

                List<WeaponFeature> weaponFeats = GetFeatures(autopsy.weapon);
                if(weaponFeats.Any())
                return "I could say that place is " + GetFeatureString(weaponFeats[Random.Range(0,weaponFeats.Count)]);

                break;
            }
        return null;
    }


}
