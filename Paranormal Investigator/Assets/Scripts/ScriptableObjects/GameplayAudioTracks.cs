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
        nightTime,
        sacrificeMode
    }

    public enum gameSFXs
    {

        //gameplayBásico
        stageStart,
        stageComplete,
        stageRestart,
        monsterGrowl,
        monsterDoomed,
        monsterDeath,
        clockTicking,
        crowdJoy,

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
    AudioTrack nightTimeST;
     [SerializeField]
    AudioTrack sacrificeModeST;
    [SerializeField]
    AudioTrack stageStart;

     [SerializeField]
    AudioTrack monsterDoomed;
     [SerializeField]
    AudioTrack monsterDeath;
    [SerializeField]
    AudioTrack stageComplete;
    [SerializeField]
    AudioTrack stageRestart;
    [SerializeField]
    AudioTrack[] monsterGrowl;
    [SerializeField]
    AudioTrack clockTicking;
    [SerializeField]
    AudioTrack crowdJoy;
    

    public AudioTrack GetGameSFX(gameSFXs sfx, int monsterIndex = 0)
    {
        int index = monsterIndex;
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
            case gameSFXs.monsterGrowl:

                index = index >= monsterGrowl.Length ? index = monsterGrowl.Length - 1 : index;
                index = index < 0 ? 0 : index;

                return monsterGrowl[index];
                break;
            case gameSFXs.clockTicking:
                return clockTicking;
                break;
            case gameSFXs.crowdJoy:
                return crowdJoy;
                break;

                 case gameSFXs.monsterDoomed:
                return monsterDoomed;
                break;

                    case gameSFXs.monsterDeath:
                return monsterDeath;
                break;
           

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
            case Soundtracks.nightTime:
                return nightTimeST;
                   case Soundtracks.sacrificeMode:
                return sacrificeModeST;
        }
        return null;
    }
}