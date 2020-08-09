using System.Collections;
using System.Collections.Generic;
using Common.Enums;
using UnityEngine;

namespace Common.Enums
{

    public enum UISFXs
    {

        mainMenuSelectOption,
        mainMenuEnterOption,
        mainMenuOpening,
        shopOpening,
        shopClosing,
        shopSelectItem,
        shopBuyItem,
        convertGemCoins,
        tipsOpening,
        tipsClosing,
        inventoryOpening,
        inventoryClosing,
        inventorySelectItem,
        kingLaugh,
        mapSelectionOpening,
        configOpening,
        witchPopUpOn,
        witchPopUpOff

    }

}

[CreateAssetMenu(menuName = "ScriptableObject/UIAudioTracks")]
public class UIAudioTracks : SingletonScriptableObject<UIAudioTracks>
{

    [Header("Main Menu")]
    [SerializeField]
    AudioTrack mainMenuSelectOption;
    [SerializeField]
    AudioTrack mainMenuEnterOption;
    [SerializeField]
    AudioTrack mainMenuOpening;

    [Header("Shop HUD")]
    [SerializeField]
    AudioTrack shopOpening;
    [SerializeField]
    AudioTrack shopClosing;
    [SerializeField]
    AudioTrack shopSelectItem;
    [SerializeField]
    AudioTrack shopBuyItem;
    [SerializeField]
    AudioTrack convertGemCoins;
    [Header("Tips HUD")]
    [SerializeField]
    AudioTrack tipsOpening;
    [SerializeField]
    AudioTrack tipsClosing;

    [Header("Inventory HUD")]

    [SerializeField]
    AudioTrack inventoryOpening;
    [SerializeField]
    AudioTrack inventoryClosing;
    [SerializeField]
    AudioTrack inventorySelectItem;

    [Header("Other HUD")]
    [SerializeField]
    AudioTrack kingLaugh;

    [SerializeField]
    AudioTrack mapSelectionOpening;
    [SerializeField]
    AudioTrack configOpening;

    [SerializeField]
    AudioTrack witchPopUpOn;

     [SerializeField]
    AudioTrack witchPopUpOff;

    public AudioTrack GetUISFX(UISFXs sfx)
    {

        switch (sfx)
        {

            case UISFXs.mainMenuSelectOption:
                return mainMenuSelectOption;
            case UISFXs.mainMenuEnterOption:
                return mainMenuEnterOption;
            case UISFXs.mainMenuOpening:
                return mainMenuOpening;
            case UISFXs.shopOpening:
                return shopOpening;
            case UISFXs.shopClosing:
                return shopClosing;
            case UISFXs.shopSelectItem:
                return shopSelectItem;
            case UISFXs.shopBuyItem:
                return shopBuyItem;
            case UISFXs.convertGemCoins:
                return convertGemCoins;
            case UISFXs.tipsOpening:
                return tipsOpening;
            case UISFXs.tipsClosing:
                return tipsClosing;
            case UISFXs.inventoryOpening:
                return inventoryOpening;
            case UISFXs.inventoryClosing:
                return inventoryClosing;
            case UISFXs.inventorySelectItem:
                return inventorySelectItem;
            case UISFXs.kingLaugh:
                return kingLaugh;
            case UISFXs.mapSelectionOpening:
                return mapSelectionOpening;
            case UISFXs.configOpening:
                return configOpening;
                case UISFXs.witchPopUpOn:
                return witchPopUpOn;
                case UISFXs.witchPopUpOff:
                return witchPopUpOff;

            default:
                return null;

        }
        return null;
    }

}