using System.Collections;
using System.Collections.Generic;
using Common.Enums;
using UnityEngine;

namespace Common.Enums
{

    public enum Soundtracks
    {
        mainMenu,
        game,
        shop
    }

    public enum gameSFXs
    {

        //gameplayBásico
        stageStart,
        stageComplete,
        stageRestart,
        kingMove,
        kingMissStep,
        gemMove,
        gemMatch,
        gemLastMatch,
        unlockDoor,
        boxMove,
        kingDownstairs,
        kingUpstairs,
        gateOpen,
        leverActivate,
        eggCracks,
        eggHatch,
        eggGameOver,
        dragonFlight,
        lavaIdle,
        lavaDestroy,
        movableFloorMove,
        movableFloorSettle,
        chainedWallActivate,
        fragmentedFloor,
        treadmillBeam,
        hypnosisBackground,
        hypnosisActivate,
        ivyBulbDestroyed,
        ivyDestroyed,
        mirrorWitchLaugh,
        mirrorActivate,
        darknessActivate,
        potionPickup,
        potionConsume

    }

}

[System.Serializable]
public class AudioTrack
{
    [SerializeField]
    private AudioClip audioClip;
    [SerializeField]
    [Range(0, 1)]
    private float defaultVolume;
    [SerializeField]
    private bool isLoopable;
    [SerializeField]
    private bool isSFX;

    public bool IsLoopable { get => isLoopable; private set => isLoopable = value; }
    public float DefaultVolume { get => defaultVolume; private set => defaultVolume = value; }
    public AudioClip AudioClip { get => audioClip; private set => audioClip = value; }
    public bool IsSFX { get => isSFX; private set => isSFX = value; }

    public AudioTrack(AudioClip audioClip_, float defaultVolume_, bool isLoopable_, bool isSFX_)
    {
        AudioClip = audioClip_;
        DefaultVolume = defaultVolume_;
        IsLoopable = isLoopable_;
        IsSFX = isSFX_;
    }

}

[CreateAssetMenu(menuName = "ScriptableObject/GameplayAudioTracks")]
public class GameplayAudioTracks : SingletonScriptableObject<GameplayAudioTracks>
{

    [Header("Gameplay Básico")]
    [SerializeField]
    AudioTrack gameST;
    [SerializeField]
    AudioTrack menuST;
    [SerializeField]
    AudioTrack shopST;
    [SerializeField]
    AudioTrack stageStart;
    [SerializeField]
    AudioTrack stageComplete;
    [SerializeField]
    AudioTrack stageRestart;
    [SerializeField]
    AudioTrack kingMove;
    [SerializeField]
    AudioTrack kingMissStep;
    [SerializeField]
    AudioTrack gemMove;
    [SerializeField]
    AudioTrack gemMatch;
    [SerializeField]
    AudioTrack gemLastMatch;
    [SerializeField]
    AudioTrack unlockDoor;

    [Header("Gameplay Pacote 1")]
    [SerializeField]
    AudioTrack boxMove;
    [SerializeField]
    AudioTrack kingDownstairs;
    [SerializeField]
    AudioTrack kingUpstairs;
    [SerializeField]
    AudioTrack gateOpen;
    [SerializeField]
    AudioTrack leverActivate;
    [SerializeField]
    AudioTrack[] eggCracks;
    [SerializeField]
    AudioTrack eggHatch;
    [SerializeField]
    AudioTrack eggGameOver;
    [SerializeField]
    AudioTrack dragonFlight;

    [Header("Gameplay Pacote 2")]
    [SerializeField]
    AudioTrack lavaIdle;
    [SerializeField]
    AudioTrack lavaDestroy;
    [SerializeField]
    AudioTrack movableFloorMove;
    [SerializeField]
    AudioTrack movableFloorSettle;
    [SerializeField]
    AudioTrack chainedWallActivate;
    [SerializeField]
    AudioTrack[] fragmentedFloor;
    [SerializeField]
    AudioTrack treadmillBeam;

    [Header("Gameplay Pacote 3")]
    [SerializeField]
    AudioTrack hypnosisBackground;
    [SerializeField]
    AudioTrack hypnosisActivate;
    [SerializeField]
    AudioTrack ivyBulbDestroyed;
    [SerializeField]
    AudioTrack ivyDestroyed;
    [SerializeField]
    AudioTrack mirrorWitchLaugh;
    [SerializeField]
    AudioTrack mirrorActivate;
     [SerializeField]
    AudioTrack darknessActivate;
    [SerializeField]
    AudioTrack potionPickup;
    [SerializeField]
    AudioTrack potionConsume;
    

    public AudioTrack GetGameSFX(gameSFXs sfx, int eggCrack_RemainingSteps = 2)
    {
        int index = eggCrack_RemainingSteps;
        switch (sfx)
        {

            case gameSFXs.stageStart:
                return stageStart;
                break;
            case gameSFXs.stageComplete:
                return stageComplete;
                break;
            case gameSFXs.stageRestart:
                return stageRestart;
                break;
            case gameSFXs.kingMove:
                return kingMove;
                break;
            case gameSFXs.kingMissStep:
                return kingMissStep;
                break;
            case gameSFXs.gemMove:
                return gemMove;
                break;
            case gameSFXs.gemMatch:
                return gemMatch;
                break;
            case gameSFXs.gemLastMatch:
                return gemLastMatch;
                break;
            case gameSFXs.unlockDoor:
                return unlockDoor;
                break;

            case gameSFXs.boxMove:
                return boxMove;
            case gameSFXs.kingDownstairs:
                return kingDownstairs;
            case gameSFXs.kingUpstairs:
                return kingUpstairs;
            case gameSFXs.gateOpen:
                return gateOpen;
            case gameSFXs.leverActivate:
                return leverActivate;
            case gameSFXs.eggCracks:

                index = index >= eggCracks.Length ? index = eggCracks.Length - 1 : index;
                index = index < 0 ? 0 : index;

                return eggCracks[index];
            case gameSFXs.eggHatch:
                return eggHatch;
            case gameSFXs.eggGameOver:
                return eggGameOver;
            case gameSFXs.dragonFlight:
                return dragonFlight;
            case gameSFXs.lavaIdle:
                return lavaIdle;
            case gameSFXs.lavaDestroy:
                return lavaDestroy;
            case gameSFXs.movableFloorMove:
                return movableFloorMove;
            case gameSFXs.movableFloorSettle:
                return movableFloorSettle;
            case gameSFXs.chainedWallActivate:
                return chainedWallActivate;
            case gameSFXs.fragmentedFloor:

                index = index >= eggCracks.Length ? index = fragmentedFloor.Length - 1 : index;
                index = index < 0 ? 0 : index;

                return fragmentedFloor[index];
            case gameSFXs.treadmillBeam:
                return treadmillBeam;

                case gameSFXs.hypnosisBackground:
                return hypnosisBackground;
                case gameSFXs.hypnosisActivate:
                return hypnosisActivate;
                case gameSFXs.ivyBulbDestroyed:
                return ivyBulbDestroyed;
                case gameSFXs.ivyDestroyed:
                return ivyDestroyed;
                case gameSFXs.mirrorWitchLaugh:
                return mirrorWitchLaugh;
                case gameSFXs.mirrorActivate:
                return mirrorActivate;
                case gameSFXs.darknessActivate:
                return darknessActivate;
                case gameSFXs.potionPickup:
                return potionPickup;
                case gameSFXs.potionConsume:
                return potionConsume;

            default:
                return null;

        }
        return null;
    }

    public AudioTrack GetMusic(Soundtracks ost)
    {
        switch (ost)
        {
            case Soundtracks.mainMenu:
                return menuST;
            case Soundtracks.game:
                return gameST;
            case Soundtracks.shop:
                return shopST;
        }
        return null;
    }
}