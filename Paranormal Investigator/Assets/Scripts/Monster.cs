using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        Blunt,
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
        Leisure,
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

    public enum Question
    {

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
    [Header("Info Panel")]
    [SerializeField]
    CanvasGroup infoGroup;
    [SerializeField]
    Image infoPanel;
    [SerializeField]
    TextMeshProUGUI title;
    [SerializeField]
    public string monsterName;
    [SerializeField]
    Image titlePanel;

    [Header("Questions Panel")]
    [SerializeField]
    CanvasGroup questionsGroup;

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
    public MonsterData monsterData;
    [SerializeField]
    public List<Weapon> weapons = new List<Weapon>();
    [SerializeField]
    public List<Place> places = new List<Place>();
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

    public bool isQuestionPopUpOn = false;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }
    public void Setup()
    {

        if (!game_ref)
            game_ref = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();

        ResizeSprite();
        ResizeInfoPanel();
    }

    public void BuildCharacter(MonsterData mData, List<Weapon> _weapons, List<Place> _places, string _name)
    {

        monsterData = mData;
        weapons = _weapons;
        places = _places;
        monsterName = _name;
        wasAsked = false;
        wasInterrogated = false;
        isDead = false;

        // clear childs

        for(int i = weaponParent.transform.childCount - 1; i >= 0 ; i--)
        {
            Destroy( weaponParent.transform.GetChild(i).gameObject);
        }

         for(int i = placeParent.transform.childCount - 1; i >= 0 ; i--)
        {
            Destroy( placeParent.transform.GetChild(i).gameObject);
        }

        if (spritesHolder)
        {
            foreach (Weapon wp in _weapons)
            {
                GameObject go = spritesHolder.InstantiatePrefab(wp);
                if (go)
                {
                    go.transform.SetParent(weaponParent.transform);
                    //need to reset this because of unity upgrade bug
                    go.transform.localScale = Vector3.one;
                    go.transform.DOLocalMoveZ(0,0);
                }
            }

            foreach (Place pl in _places)
            {
                GameObject go = spritesHolder.InstantiatePrefab(pl);
                if (go)
                {
                    go.transform.SetParent(placeParent.transform);

                     go.transform.localScale = Vector3.one;
                    go.transform.DOLocalMoveZ(0,0);
                }
            }

            SetSprite(monsterData.monster);
            SetName(_name);

        }

        ResizeSprite();
        ResizeInfoPanel();

        gameObject.name = monsterName + "(" +  System.Enum.GetName(typeof(MonsterCharacter),monsterData.monster)+ ")";

    }

    void SetSprite(MonsterCharacter character)
    {
        image.sprite = spritesHolder.GetMonsterSpriteAndAnimator(character)?.sprite;
        animator.runtimeAnimatorController = spritesHolder.GetMonsterSpriteAndAnimator(character)?.animator;
    }

    void SetName(string _name)
    {
        monsterName = _name;
        title.text = monsterName;
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
        string enumName = System.Enum.GetName(typeof(MonsterCharacter), character);
        enumName = enumName.ToLower();

        if (enumName.FirstOrDefault() == 'e')
        {
            return Element.Earth;
        }
        else if (enumName.FirstOrDefault() == 'a')
        {
            return Element.Air;
        }
        else if (enumName.FirstOrDefault() == 'f')
        {
            return Element.Fire;
        }
        else // if(enumName.FirstOrDefault() == 'd')
        {
            return Element.Dark;
        }
    }

    public void ResizeInfoPanel()
    {
        bool weaponsBigger = Game.weaponDefaultWidth > Game.placeDefaultWidth;
        bool moreWeapons = weapons.Count > places.Count;

        float cellWidth = moreWeapons ? Game.weaponDefaultWidth : Game.placeDefaultWidth;

        if (weapons.Count == places.Count)
        {

            cellWidth = weaponsBigger ? Game.weaponDefaultWidth : Game.placeDefaultWidth;
            if (weapons.Count > 1 || places.Count > 1)
            {
                cellWidth += moreWeapons ? weaponParent.spacing.x : placeParent.spacing.x;
            }
        }

        infoPanel.rectTransform.sizeDelta = new Vector2(cellWidth * ((moreWeapons ? weapons.Count : places.Count) + 1.5f), Game.infoDefaultHeight);

    }

    public void ResizeSprite()
    {
        image.rectTransform.sizeDelta = new Vector2(image.sprite.rect.width, image.sprite.rect.height) * Game.monsterSpriteSizeMultiplier;

    }

    public void ShowBasicInfo(bool on)
    {
        infoGroup.DOFade(on ? 1 : 0, infoShowDuration);
        infoGroup.transform.DOLocalMoveY(on ? 0 : -50f, infoShowDuration);
        if(on)
        {
             if(AudioPlayer.Instance())
            {
                AudioPlayer.Instance().Play(UISFXs.infoPanelShowUp);
            }
        }
    }
    public void ShowQuestionOptions(bool on)
    {
        questionsGroup.DOFade(on ? 1 : 0, infoShowDuration);
        questionsGroup.transform.DOLocalMoveY(on ? 0 : -50f, infoShowDuration);
        questionsGroup.blocksRaycasts = on;

        foreach (GraphicRaycaster gr in questionsGroup.GetComponentsInChildren<GraphicRaycaster>(true))
        {
            if (gr)
            {
                gr.enabled = on;
            }
        }

        isQuestionPopUpOn = on;

        if(on)
        {
             if(AudioPlayer.Instance())
            {
                AudioPlayer.Instance().Play(UISFXs.infoPanelShowUp);
            }
        }
    }

    public void ShowQuestionMark(bool on)
    {
          if(on)
        {
            //print("play question yo");
            AudioPlayer.Instance()?.Play(UISFXs.questionMark);
        }

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
        talkText.ClearTexts();
        Tween t = Game.FadeGroup(talkBubble, on, infoShowDuration);
        if (on)
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(GetRandomSacrificeText());
            queue.Enqueue(GetRandomSacrificeText());
            talkText.SetQueue(queue);

            t.onComplete += () => talkText.RevealText();
        }
        else
            t.onComplete += () => talkText.StopRevealing();
    }

    public void ShowTalkBubble(bool on, Queue<string> queue)
    {

        talkText.ClearTexts();
        Tween t = Game.FadeGroup(talkBubble, on, infoShowDuration);
        if (on)
        {
            talkText.SetQueue(queue);

            t.onComplete += () => talkText.RevealText();
        }
        else
            t.onComplete += () => talkText.StopRevealing();
    }

    public void ShowTalkBubbleAnswer(bool on)
    {
        Queue<string> answers = new Queue<string>();
        answers.Enqueue(answer);

        ShowTalkBubble(on, answers);
    }

    public void SetSacrifice()
    {
        if (game_ref && isDead == false)
        {
            game_ref.SetSacrifice(this);
        }
    }
    public bool Sacrifice(bool on)
    {
        if (isDead && on)
        {
            print("cant be accused");
            return false;
        }
        else
        {
            ShowExclamationMark(on);
            ShowTalkBubble(on);
            if (!on)
                ShowQuestionMark(false);
            isSacrifice = on;
            return isSacrifice;

            if(on)
            {
                AudioPlayer.Instance()?.Play(gameSFXs.monsterDoomed);
            }
        }
    }

    public void Assassinate(Monster assassin)
    {
        autopsy.place = Random.value <= 0.5 ? assassin.places[Random.Range(0, assassin.places.Count)] : places[Random.Range(0, places.Count)];
        autopsy.weapon = assassin.weapons[Random.Range(0, assassin.weapons.Count)];
        autopsy.deadAssassin = assassin.isDead;
        autopsy.character = assassin.monsterData.monster;

        isDead = true;
        //questionMark.GetComponentInChildren<Image>().color = Color.black;
        Sacrifice(false);

        SetSprite(MonsterCharacter.Ghost);
        //image.sprite = spritesHolder.GetMonsterSpriteAndAnimator(MonsterCharacter.Ghost)?.sprite;
        //animator.runtimeAnimatorController = spritesHolder.GetMonsterSpriteAndAnimator(MonsterCharacter.Ghost)?.animator;
        AudioPlayer.Instance()?.Play(gameSFXs.monsterDeath);
        ResizeSprite();
        ResizeInfoPanel();
       game_ref.ShakeScreen();
    }

    public void ActionClick()
    {
        if (isDead == false)
        {
            if (game_ref.isSacrificeMode == true)
            {
                SetSacrifice();
            }
            else
            {
                if (wasAsked == false)
                {
                    Ask();

                }
            }

        }
        else
        {
            if (wasInterrogated == false)
            {
                Interrogate();
            }

        }
    }

    public void Ask()
    {
        if(wasAsked == true || game_ref.CanAsk() == false) return;
        
        game_ref.IncreaseQuestionCount(true);
        wasAsked = true;
        ShowQuestionMark(false);
        ShowBasicInfo(true);
    }

    public void Interrogate()
    {
        //wasInterrogated = true;
        ShowQuestionMark(false);
        ShowQuestionOptions(true);

    }
    public void InterrogateQuestion(int question)
    {
         if(wasInterrogated == true || game_ref.CanInterrogate() == false) return;
        
        game_ref.IncreaseQuestionCount(false);
        wasInterrogated = true;
        Question q = (Question)question;
        print("preessed " + (Question)question);
        ShowQuestionOptions(false);

        Queue<string> answers = new Queue<string>();
        answer = GetAnswer(q, autopsy, monsterDataHolder);
        answers.Enqueue(answer);

        ShowTalkBubble(true, answers);
    }

    public void HideAllPopUps(bool excludeExclamationMark)
    {
        ShowBasicInfo(false);
        if (exclamationMark == false)
            ShowExclamationMark(false);
        ShowQuestionMark(false);
        ShowQuestionOptions(false);
        ShowTalkBubble(false);
    }

    public string GetRandomSacrificeText()
    {
        List<string> sacrificeTexts = new List<string>
        {
            "Don't kill me please!!!1!",
            "Do you always solve your cases by randonly killing people?",
            "If you kill me, I'll wait for you in hell (:",
            "Whatever dood, I'm already dead inside...",
            "Yay, I always wanted to be part of a demon sacrifice for the greater good *-* ",
            "I'm innocent! Don't do this",
            "Are you sure about this? Because I ain't sure either",
        };

        if(game_ref?.textsObject)
        {
            sacrificeTexts = game_ref?.textsObject.monstersSacriceTexts;
        }

        return sacrificeTexts[Random.Range(0, sacrificeTexts.Count)];
    }

    public static List<PlaceFeature> GetFeatures(Place place)
    {
        List<PlaceFeature> features = new List<PlaceFeature>();

        switch (place)
        {

            case Place.House:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Calm, PlaceFeature.Common, PlaceFeature.Indoors, PlaceFeature.Leisure });
                break;
            case Place.Barn:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Calm, PlaceFeature.Indoors, PlaceFeature.Outdoors });
                break;
            case Place.Building:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Busy, PlaceFeature.Common, PlaceFeature.Indoors });
                break;
            case Place.Stadium:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Busy, PlaceFeature.Common, PlaceFeature.Outdoors, PlaceFeature.Leisure });
                break;
            case Place.GasStation:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Busy, PlaceFeature.Common, PlaceFeature.Outdoors });
                break;
            case Place.Shop:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Busy, PlaceFeature.Common, PlaceFeature.Indoors });
                break;
            case Place.Bank:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Busy, PlaceFeature.Common, PlaceFeature.Indoors });
                break;
            case Place.Hospital:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Busy, PlaceFeature.Calm, PlaceFeature.Common, PlaceFeature.Indoors });
                break;
            case Place.Port:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Busy, PlaceFeature.Outdoors });
                break;
            case Place.Airport:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Busy, PlaceFeature.Common, PlaceFeature.Indoors });
                break;
            case Place.BusStop:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Busy, PlaceFeature.Common, PlaceFeature.Outdoors });
                break;
            case Place.Warehouse:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Busy, PlaceFeature.Indoors, PlaceFeature.Outdoors });
                break;
            case Place.Theater:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Busy, PlaceFeature.Calm, PlaceFeature.Common, PlaceFeature.Indoors, PlaceFeature.Leisure });
                break;
            case Place.Church:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Calm, PlaceFeature.Common, PlaceFeature.Indoors, PlaceFeature.Leisure });
                break;
            case Place.Graveyard:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Calm, PlaceFeature.Outdoors });
                break;
            case Place.Factory:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Busy, PlaceFeature.Indoors });
                break;
            case Place.NuclearPlant:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Busy, PlaceFeature.Indoors, });
                break;
            case Place.Castle:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Indoors, PlaceFeature.Leisure });
                break;
            case Place.Park:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Calm, PlaceFeature.Common, PlaceFeature.Outdoors, PlaceFeature.Leisure });
                break;
            case Place.Lighthouse:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Calm, PlaceFeature.Outdoors });
                break;
            case Place.Windmill:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Calm, PlaceFeature.Outdoors });
                break;
            case Place.Vulcan:
                features.AddRange(new List<PlaceFeature> { PlaceFeature.Calm, PlaceFeature.Outdoors });
                break;

        }

        return features;
    }

    public static List<WeaponFeature> GetFeatures(Weapon weapon)
    {
        List<WeaponFeature> features = new List<WeaponFeature>();

        switch (weapon)
        {
            case Weapon.Hammer:
                features.AddRange(new List<WeaponFeature> { WeaponFeature.Construction, WeaponFeature.Blunt });
                break;
            case Weapon.Wrench:
                features.AddRange(new List<WeaponFeature> {  WeaponFeature.Construction, WeaponFeature.Blunt });
                
                break;
            case Weapon.Backsaw:
                features.AddRange(new List<WeaponFeature> { WeaponFeature.Blade, WeaponFeature.Construction});
                
                break;
            case Weapon.Scythe:
                features.AddRange(new List<WeaponFeature> { WeaponFeature.Blade, WeaponFeature.Gardening });
                
                break;
            case Weapon.Shovel:
                features.AddRange(new List<WeaponFeature> { WeaponFeature.Gardening, WeaponFeature.Construction,WeaponFeature.Blunt});
                
                break;
            case Weapon.Pitchfork:
                features.AddRange(new List<WeaponFeature> { WeaponFeature.Piercing, WeaponFeature.Gardening });
                
                break;
            case Weapon.Knife:
                features.AddRange(new List<WeaponFeature> { WeaponFeature.Blade, WeaponFeature.Piercing });
                
                break;
            case Weapon.Crowbar:
                features.AddRange(new List<WeaponFeature> {WeaponFeature.Construction, WeaponFeature.Blunt });
                
                break;
            case Weapon.Screwdriver:
                features.AddRange(new List<WeaponFeature> { WeaponFeature.Construction, WeaponFeature.Harmless, WeaponFeature.Blunt });
                
                break;
            case Weapon.Axe:
                features.AddRange(new List<WeaponFeature> { WeaponFeature.Blade, WeaponFeature.Gardening });
                
                break;
            case Weapon.Cleaver:
                features.AddRange(new List<WeaponFeature> { WeaponFeature.Blade});
                
                break;
            case Weapon.UtilityKnife:
                features.AddRange(new List<WeaponFeature> { WeaponFeature.Blade, WeaponFeature.Piercing, WeaponFeature.Construction, });
                
                break;
            case Weapon.Duster:
                features.AddRange(new List<WeaponFeature> { WeaponFeature.Harmless, WeaponFeature.Blunt });
                
                break;
            case Weapon.Pickaxe:
                features.AddRange(new List<WeaponFeature> { WeaponFeature.Piercing, WeaponFeature.Construction, WeaponFeature.Blunt });
                
                break;
            case Weapon.WateringCan:
                features.AddRange(new List<WeaponFeature> {WeaponFeature.Gardening, WeaponFeature.Harmless, WeaponFeature.Blunt });
                
                break;
            case Weapon.Cultivator:
                features.AddRange(new List<WeaponFeature> {  WeaponFeature.Piercing, WeaponFeature.Gardening });
                
                break;
            case Weapon.Pliers:
                features.AddRange(new List<WeaponFeature> { WeaponFeature.Blade, WeaponFeature.Piercing, WeaponFeature.Gardening, WeaponFeature.Construction});
                
                break;
            case Weapon.Broom:
                features.AddRange(new List<WeaponFeature> {  WeaponFeature.Gardening, WeaponFeature.Construction, WeaponFeature.Harmless, WeaponFeature.Blunt});
                
                break;
            case Weapon.Scraper:
                features.AddRange(new List<WeaponFeature> { WeaponFeature.Construction, WeaponFeature.Harmless, WeaponFeature.Blunt });
                
                break;
            case Weapon.PipeWrench:
                features.AddRange(new List<WeaponFeature> {  WeaponFeature.Construction,WeaponFeature.Blunt });
                
                break;
            case Weapon.TapeMeasure:
                features.AddRange(new List<WeaponFeature> { WeaponFeature.Construction, WeaponFeature.Harmless });
                
                break;
            case Weapon.Gouge:
                features.AddRange(new List<WeaponFeature> { WeaponFeature.Piercing,  WeaponFeature.Construction, WeaponFeature.Harmless, WeaponFeature.Blunt });
                
                break;

        }

        return features;
    }

    public string GetFeatureString(PlaceFeature place)
    {
        return System.Enum.GetName(typeof(PlaceFeature), place);
    }

    public string GetFeatureString(WeaponFeature weapon)
    {
        return System.Enum.GetName(typeof(WeaponFeature), weapon);
    }
    public string GetAnswer(Question question, Autopsy autopsy, MonsterDataObject monsterDataObj)
    {

        switch (question)
        {
            case Question.who:

                if (autopsy.deadAssassin)
                {
                    if (Random.value < 0.5f)
                    {
                        return "I was killed by another ghost";
                    }

                }

                bool hasHands = monsterDataObj.HasHands(autopsy.character);
                bool hasFeet = monsterDataObj.HasFeet(autopsy.character);

                if (hasHands && hasFeet)
                {
                    return Random.value <= 0.5f ? "On the floor, I could see that it had " + Game.GetColoredString("feet", game_ref.membersHighlightColor) :
                        "If it wasn't for his " + Game.GetColoredString("feet", game_ref.membersHighlightColor) + " , I would have make it...";

                }
                else if (hasHands || hasFeet)
                {
                    if (Random.value <= 0.5f)

                        return hasFeet ? "On the floor, I could see that it had " + Game.GetColoredString("feet", game_ref.membersHighlightColor) :
                            "If it wasn't for his " + Game.GetColoredString("feet", game_ref.membersHighlightColor) + " , I would have make it...";
                }

                return "I remember it clearly, he soul is made of " +  Game.GetColoredString(System.Enum.GetName(typeof(Element), Monster.GetElement(autopsy.character)),game_ref.membersHighlightColor);

                break;

            case Question.where:

                List<PlaceFeature> placeFeats = GetFeatures(autopsy.place);
                if (placeFeats.Any())
                    return "I could say that place is kinda " + Game.GetColoredString(GetFeatureString(placeFeats[Random.Range(0, placeFeats.Count)]),game_ref.placeHighlightColor);

                break;

            case Question.how:

                List<WeaponFeature> weaponFeats = GetFeatures(autopsy.weapon);
                if (weaponFeats.Any())
                    return "The weapon was very " + Game.GetColoredString(GetFeatureString(weaponFeats[Random.Range(0, weaponFeats.Count)]), game_ref.weaponHighlightColor);

                break;
        }
        return null;
    }

}