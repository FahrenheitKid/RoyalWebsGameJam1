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

    public enum Element
    {
        Earth,
        Fire,
        Air,
        Dark
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


public class Monster : MonoBehaviour
{
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

    public static float titlePanelDefaultPaddingY = 70f;
    public static float infoPanelDefaultPaddingX = 200f;
    public static float infoShowDuration = 0.5f;
    [SerializeField]
    SpritesHolder spritesHolder;

    public bool useFade = true;


    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }
    public void Setup()
    {
       
        

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


}
