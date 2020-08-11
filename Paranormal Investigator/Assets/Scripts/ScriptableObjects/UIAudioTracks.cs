﻿using System.Collections;
using System.Collections.Generic;
using Common.Enums;
using UnityEngine;

namespace Common.Enums
{

    public enum UISFXs
    {

        mouseOverMonster,
        mouseOverButton,
        buttonClick,
        sacrificeButtonClick,
        textRevealing,
        cantAsk

    }

}

[CreateAssetMenu(menuName = "ScriptableObject/UIAudioTracks")]
public class UIAudioTracks : SingletonScriptableObject<UIAudioTracks>
{

    [Header("Main Menu")]
    [SerializeField]
    AudioTrack mouseOverMonster;
    [SerializeField]
    AudioTrack mouseOverButton;
    [SerializeField]
    AudioTrack buttonClick;

    [SerializeField]
    AudioTrack sacrificeButtonClick;
    [SerializeField]
    AudioTrack textRevealing;
    [SerializeField]
    AudioTrack cantAsk;

    public AudioTrack GetUISFX(UISFXs sfx)
    {

        switch (sfx)
        {

            case UISFXs.mouseOverMonster:
                return mouseOverMonster;
            case UISFXs.mouseOverButton:
                return mouseOverButton;
            case UISFXs.buttonClick:
                return buttonClick;
            case UISFXs.sacrificeButtonClick:
                return sacrificeButtonClick;
            case UISFXs.textRevealing:
                return textRevealing;
                 case UISFXs.cantAsk:
                return cantAsk;

            default:
                return null;

        }
        return null;
    }

}