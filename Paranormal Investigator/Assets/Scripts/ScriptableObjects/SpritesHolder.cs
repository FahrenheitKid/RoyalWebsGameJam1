using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Common.Enums;

[System.Serializable]
public class MonsterSpriteAndAnimator
{
    public Sprite sprite;
    public AnimatorOverrideController animator;
}
[CreateAssetMenu(menuName = "ScriptableObject/SpritesHolder")]
public class SpritesHolder : ScriptableObject
{
    [Header("Characters")]
        [SerializeField]
        MonsterSpriteAndAnimator airSlime;
        [SerializeField]
        MonsterSpriteAndAnimator earthKoala;
        [SerializeField]
        MonsterSpriteAndAnimator earthGoomba;
        [SerializeField]
        MonsterSpriteAndAnimator earthTrunk;
        [SerializeField]
        MonsterSpriteAndAnimator earthTree;
        [SerializeField]
        MonsterSpriteAndAnimator earthGolem;
        [SerializeField]
        MonsterSpriteAndAnimator earthPumpkin;
        [SerializeField]
        MonsterSpriteAndAnimator earthling;
        [SerializeField]
        MonsterSpriteAndAnimator earthKing;
        [SerializeField]
        MonsterSpriteAndAnimator FireSlime;
        [SerializeField]
        MonsterSpriteAndAnimator fireSlima;
        [SerializeField]
        MonsterSpriteAndAnimator fireSlimaga;
        [SerializeField]
        MonsterSpriteAndAnimator fireTank;
        [SerializeField]
        MonsterSpriteAndAnimator ghost;
        [SerializeField]
        MonsterSpriteAndAnimator fireKing;
        [SerializeField]
        MonsterSpriteAndAnimator fireAlien;
        [SerializeField]
        MonsterSpriteAndAnimator airBird;
        [SerializeField]
        MonsterSpriteAndAnimator airBirda;
        [SerializeField]
        MonsterSpriteAndAnimator airBirdling;
        [SerializeField]
        MonsterSpriteAndAnimator airBirdaga;
        [SerializeField]
        MonsterSpriteAndAnimator airEgg;
        [SerializeField]
        MonsterSpriteAndAnimator airBirdaja;
        [SerializeField]
        MonsterSpriteAndAnimator airOwl;
        [SerializeField]
        MonsterSpriteAndAnimator darkSlime;
        [SerializeField]
        MonsterSpriteAndAnimator darkPig;
        [SerializeField]
        MonsterSpriteAndAnimator darkBat;
        [SerializeField]
        MonsterSpriteAndAnimator darkGhoul;
        [SerializeField]
        MonsterSpriteAndAnimator darkWizard;
        [SerializeField]
        MonsterSpriteAndAnimator darkImp;
        [SerializeField]
        MonsterSpriteAndAnimator darkDemon;
        [SerializeField]
        MonsterSpriteAndAnimator darkBrain;

        [Header("Weapons")]
        [SerializeField]
        Sprite hammer;
        [SerializeField]
        Sprite wrench;
        [SerializeField]
        Sprite backsaw;
        [SerializeField]
        Sprite scythe;
        [SerializeField]
        Sprite shovel;
        [SerializeField]
        Sprite pitchfork;
        [SerializeField]
        Sprite knife;
        [SerializeField]
        Sprite crowbar;
        [SerializeField]
        Sprite screwdriver;
        [SerializeField]
        Sprite axe;
        [SerializeField]
        Sprite cleaver;
        [SerializeField]
        Sprite utilityKnife;
        [SerializeField]
        Sprite duster;
        [SerializeField]
        Sprite pickaxe;
        [SerializeField]
        Sprite wateringCan;
        [SerializeField]
        Sprite cultivator;
        [SerializeField]
        Sprite pliers;
        [SerializeField]
        Sprite broom;
        [SerializeField]
        Sprite scraper;
        [SerializeField]
        Sprite pipeWrench;
        [SerializeField]
        Sprite tapeMeasure;
        [SerializeField]
        Sprite gouge;

        [Header("Places")]
        [SerializeField]
        Sprite house;
        [SerializeField]
        Sprite barn;
        [SerializeField]
        Sprite building;
        [SerializeField]
        Sprite stadium;
        [SerializeField]
        Sprite gasStation;
        [SerializeField]
        Sprite shop;
        [SerializeField]
        Sprite bank;
        [SerializeField]
        Sprite hospital;
        [SerializeField]
        Sprite port;
        [SerializeField]
        Sprite airport;
        [SerializeField]
        Sprite busStop;
        [SerializeField]
        Sprite warehouse;
        [SerializeField]
        Sprite theater;
        [SerializeField]
        Sprite church;
        [SerializeField]
        Sprite graveyard;
        [SerializeField]
        Sprite factory;
        [SerializeField]
        Sprite nuclearPlant;
        [SerializeField]
        Sprite castle;
        [SerializeField]
        Sprite park;
        [SerializeField]
        Sprite lighthouse;
        [SerializeField]
        Sprite windmill;
        [SerializeField]
        Sprite vulcan;


    public MonsterSpriteAndAnimator GetMonsterSpriteAndAnimator(MonsterCharacter character)
    {
        switch(character)
        {
        case MonsterCharacter.AirSlime:
        return airSlime;
        case MonsterCharacter.EarthKoala:
        return earthKoala;
        case MonsterCharacter.EarthGoomba:
        return earthGoomba;
        case MonsterCharacter.EarthTrunk:
        return earthKoala;
        case MonsterCharacter.EarthTree:
        return earthTree;
        case MonsterCharacter.EarthGolem:
        return earthGolem;
        case MonsterCharacter.EarthPumpkin:
        return earthPumpkin;
        case MonsterCharacter.Earthling:
        return earthling;
        case MonsterCharacter.EarthKing:
        return earthKing;
        case MonsterCharacter.FireSlime:
        return FireSlime;
        case MonsterCharacter.FireSlima:
        return fireSlima;
        case MonsterCharacter.FireSlimaga:
        return fireSlimaga;
        case MonsterCharacter.FireTank:
        return fireTank;
        case MonsterCharacter.Ghost:
        return ghost;
        case MonsterCharacter.FireKing:
        return fireKing;
        case MonsterCharacter.FireAlien:
        return fireAlien;
        case MonsterCharacter.AirBird:
        return airBird;
        case MonsterCharacter.AirBirda:
        return airBirda;
        case MonsterCharacter.AirBirdling:
        return airBirdling;
        case MonsterCharacter.AirBirdaga:
        return airBirdaga;
        case MonsterCharacter.AirEgg:
        return airEgg;
        case MonsterCharacter.AirBirdaja:
        return airBirdaja;
        case MonsterCharacter.AirOwl:
        return airOwl;
        case MonsterCharacter.DarkSlime:
        return darkSlime;
        case MonsterCharacter.DarkPig:
        return darkPig;
        case MonsterCharacter.DarkBat:
        return darkBat;
        case MonsterCharacter.DarkGhoul:
        return darkGhoul;
        case MonsterCharacter.DarkWizard:
        return darkWizard;
        case MonsterCharacter.DarkImp:
        return darkImp;
        case MonsterCharacter.DarkDemon:
        return darkDemon;
        case MonsterCharacter.DarkBrain:
        return darkBrain;
        }

        return null;
    }

    public Sprite GetSprite(Weapon weapon)
    {
        switch(weapon)
        {
        case Weapon.Hammer:
        return hammer;
        case Weapon.Wrench:
        return wrench;
        case Weapon.Backsaw:
        return backsaw;
        case Weapon.Scythe:
        return scythe;
        case Weapon.Shovel:
        return shovel;
        case Weapon.Pitchfork:
        return pitchfork;
        case Weapon.Knife:
        return knife;
        case Weapon.Crowbar:
        return crowbar;
        case Weapon.Screwdriver:
        return screwdriver;
        case Weapon.Axe:
        return axe;
        case Weapon.Cleaver:
        return cleaver;
        case Weapon.UtilityKnife:
        return utilityKnife;
        case Weapon.Duster:
        return duster;
        case Weapon.Pickaxe:
        return pickaxe;
        case Weapon.WateringCan:
        return wateringCan;
        case Weapon.Cultivator:
        return cultivator;
        case Weapon.Pliers:
        return pliers;
        case Weapon.Broom:
        return broom;
        case Weapon.Scraper:
        return scraper;
        case Weapon.PipeWrench:
        return pipeWrench;
        case Weapon.TapeMeasure:
        return tapeMeasure;
        case Weapon.Gouge:
        return gouge;
        }

        return null;
    }

    public Sprite GetSprite(Place place)
    {
        switch(place)
        {
        case Place.House:
        return house;
        case Place.Barn:
        return barn;
        case Place.Building:
        return building;
        case Place.Stadium:
        return stadium;
        case Place.GasStation:
        return gasStation;
        case Place.Shop:
        return shop;
        case Place.Bank:
        return bank;
        case Place.Hospital:
        return hospital;
        case Place.Port:
        return port;
        case Place.Airport:
        return airport;
        case Place.BusStop:
        return busStop;
        case Place.Warehouse:
        return warehouse;
        case Place.Theater:
        return theater;
        case Place.Church:
        return church;
        case Place.Graveyard:
        return graveyard;
        case Place.Factory:
        return factory;
        case Place.NuclearPlant:
        return nuclearPlant;
        case Place.Castle:
        return castle;
        case Place.Park:
        return park;
        case Place.Lighthouse:
        return lighthouse;
        case Place.Windmill:
        return windmill;
        case Place.Vulcan:
        return vulcan;
        }

        return null;
    }
}
