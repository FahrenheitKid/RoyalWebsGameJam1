using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

    // Start is called before the first frame update
    void Start()
    {
        
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
}
