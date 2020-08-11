using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{

    [SerializeField]
    GameplayAudioTracks gameplayTracks;
    [SerializeField]
    UIAudioTracks uiTracks;

    [SerializeField]
    AudioMixer masterMix;
    [SerializeField]
    AudioMixerGroup musicMixer;

    [SerializeField]
    AudioMixerGroup sfxMixer;

    float masterMixVolume;
    float musicMixVolume;

    float sfxMixVolume;

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private float musicfadeTime = 0.25f;

    [SerializeField]
    private bool isMusicMuted = false;

    [SerializeField]
    private bool isSFXMuted = false;

    [SerializeField]
    private bool isMasterMuted = false;

    public static string SFXVolumeParam = "SFXVolume";
    public static string MusicVolumeParam = "MusicVolume";
    public static string MasterMixVolumeParam = "MasterVolume";

    public bool syncedSliders = false;

    private static AudioPlayer _instance;

    private void Awake()
    {
        if (_instance == null)_instance = this;
        else
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
        }

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    // Start is called before the first frame update
    private void Start()
    {

        if (musicSource == null)
            musicSource = GetComponent<AudioSource>();

        if (musicSource)
        {
            AudioTrack at = GetAudioTrackFromClip(musicSource.clip);
            if (at != null)
            {
                musicSource.volume = at.DefaultVolume;
            }
        }
        isMusicMuted = false;

        DontDestroyOnLoad(this);

    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    // Update is called once per frame
    private void Update() { }

    public static AudioPlayer Instance()
    {
        return _instance;
    }

    public void ToggleMuteMusic()
    {
        float defaultVolume = 0.9f;
        musicMixer.audioMixer.GetFloat(MusicVolumeParam, out defaultVolume);
        AudioPlayer.ToggleMute(musicMixer, ref isMusicMuted, musicfadeTime, MusicVolumeParam, musicMixVolume);
    }

    public void ToggleMuteSFX()
    {
        float defaultVolume = 0.9f;
        musicMixer.audioMixer.GetFloat(SFXVolumeParam, out defaultVolume);
        AudioPlayer.ToggleMute(sfxMixer, ref isSFXMuted, 0, SFXVolumeParam, sfxMixVolume);
    }

    public void ToggleMuteMaster()
    {
        float defaultVolume = 0.9f;
        musicMixer.audioMixer.GetFloat(MasterMixVolumeParam, out defaultVolume);
        AudioPlayer.ToggleMute(masterMix, ref isMasterMuted, musicfadeTime, MasterMixVolumeParam, masterMixVolume);
    }

    public static void ToggleMute(AudioMixerGroup mixerGroup, ref bool _isMuted, float fadeTime, string volumeParam, float normalVolume = -8000)
    {
        _isMuted = !_isMuted;

        if (!mixerGroup)return;

        if (normalVolume <= -8000)normalVolume = 0;

        float newVolume = _isMuted ? -80f : normalVolume;
        mixerGroup.audioMixer.DOSetFloat(volumeParam, normalVolume, fadeTime);

    }

    public static void ToggleMute(AudioMixer mixer, ref bool _isMuted, float fadeTime, string volumeParam, float normalVolume = -8000)
    {
        _isMuted = !_isMuted;

        if (!mixer)return;

        if (normalVolume <= -8000)normalVolume = 0;

        float newVolume = _isMuted ? -80f : normalVolume;
        mixer.DOSetFloat(volumeParam, normalVolume, fadeTime);

    }

    //old play
    public void Play(AudioClip _music, bool overrideMute = true)
    {
        if (!overrideMute && isMusicMuted || musicSource == null)return;
        if (!overrideMute && !isMusicMuted && musicSource.clip == _music && musicSource.isPlaying)return;

        if (_music == null)return;

        if (overrideMute)
        {
            if (isMusicMuted && musicSource.clip == _music)ToggleMuteMusic();
            else if (isMusicMuted)
            {
                musicSource.clip = _music;
                ToggleMuteMusic();
            }
        }

        if (!isMusicMuted)
        {
            if (musicSource.clip == _music)
            {
                if (!musicSource.isPlaying)musicSource.Play();
            }
            else
            {
                musicSource.clip = _music;
                if (musicSource.isPlaying)
                {
                    musicSource.DOFade(0f, musicfadeTime / 2).OnComplete(() => { musicSource.DOFade(1, musicfadeTime / 2); });
                }
                else
                {
                    musicSource.volume = 0;
                    musicSource.DOFade(1, musicfadeTime);
                    musicSource.Play();
                }

            }

        }

    }

    public void Play(gameSFXs sfx, int eggCracks_RemainingSteps = 2, bool? oneShot = null)
    {
        //print("played" + sfx);
        if (eggCracks_RemainingSteps != 2)
            Play(GetSFX(sfx, eggCracks_RemainingSteps), oneShot);
        else Play(GetSFX(sfx), oneShot);
    }

    public void Play(UISFXs sfx, bool? oneShot = null)
    {
        Play(GetSFX(sfx), oneShot);
    }

    public static AudioTrack GetSFX(gameSFXs sfx, int eggCracks_RemainingSteps = 2)
    {
        if (eggCracks_RemainingSteps == 2)
            return AudioPlayer.Instance()?.gameplayTracks?.GetGameSFX(sfx);
        else return AudioPlayer.Instance()?.gameplayTracks?.GetGameSFX(sfx, eggCracks_RemainingSteps);
    }

    public static AudioTrack GetSFX(UISFXs sfx)
    {
        return AudioPlayer.Instance()?.uiTracks?.GetUISFX(sfx);
    }

    public static AudioTrack GetMusic(Soundtracks ost)
    {
        return AudioPlayer.Instance()?.gameplayTracks?.GetMusic(ost);
    }

    public AudioSource GetAudioSource(bool sfx)
    {
        return sfx ? sfxSource : musicSource;
    }

    public void Play(AudioTrack track, bool? oneShot = null)
    {

        if (track == null || !sfxSource || !musicSource)return;

        if (track.IsSFX)
        {
            if (oneShot == null || oneShot == true)sfxSource.PlayOneShot(track.AudioClip, track.DefaultVolume);
            else
            {
                sfxSource.clip = track.AudioClip;
                sfxSource.volume = track.DefaultVolume;
                sfxSource.Play();
            }
        }
        else
        {
            if (musicSource.clip == track.AudioClip && musicSource.isPlaying)return;

            if (oneShot == true)musicSource.PlayOneShot(track.AudioClip, track.DefaultVolume);
            else
            {
                musicSource.clip = track.AudioClip;
                musicSource.volume = track.DefaultVolume;
                musicSource.loop = track.IsLoopable;
                musicSource.Play();
            }
        }
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == Game.gameScene)
        {

            Start();
            Play(GetMusic(Soundtracks.game));
        }
        else if (scene.name != Game.gameScene)
        {

            Start();
            Play(GetMusic(Soundtracks.mainMenu));
        }
        //Debug.Log("Level Loaded");
        //Debug.Log(scene.name);
        //Debug.Log(mode);
    }

    public void PlayGameMusic()
    {
        Play(GetMusic(Soundtracks.game));
    }

     public void PlayShopMusic()
    {
        Play(GetMusic(Soundtracks.nightTime));
    }

    public void StopMusic()
    {
        if (!musicSource)return;
        if (musicSource.isPlaying)
        {
            musicSource.DOFade(0, musicfadeTime).OnKill(() => musicSource.Stop());
        }
    }

    public void UpdateSFXMasterVolume(Slider slider)
    {
        if (sfxMixer && slider)
        {
            float remapedValue = slider.value.Remap(0, 1, -80, 0);
            sfxMixer.audioMixer.SetFloat(SFXVolumeParam, remapedValue);
            sfxMixVolume = remapedValue;
        }
    }

    public void UpdateMusicMasterVolume(Slider slider)
    {
        if (musicMixer && slider)
        {

            float remapedValue = slider.value.Remap(0, 1, -80, 0);
            float test = -1;
            musicMixer.audioMixer.GetFloat(MusicVolumeParam, out test);
         

            musicMixer.audioMixer.SetFloat(MusicVolumeParam, remapedValue);

            musicMixer.audioMixer.GetFloat(MusicVolumeParam, out test);

            musicMixVolume = remapedValue;
        }
    }

    public void UpdateMasterMixVolume(Slider slider)
    {
        if (masterMix && slider)
        {
            float remapedValue = slider.value.Remap(0, 1, -80, 0);
            masterMix.SetFloat(MasterMixVolumeParam, remapedValue);
            masterMixVolume = remapedValue;
        }
    }

    public AudioTrack GetAudioTrackFromClip(AudioClip clip)
    {
        AudioTrack at = null;
        foreach (gameSFXs sfx in System.Enum.GetValues(typeof(gameSFXs)))
        {
            at = GetSFX(sfx);
            if (at?.AudioClip == clip && clip != null)
            {
                return at;
            }
        }

        foreach (Soundtracks music in System.Enum.GetValues(typeof(Soundtracks)))
        {
            at = GetMusic(music);
            if (at?.AudioClip == clip && clip != null)
            {
                return at;
            }
        }

        return null;
    }

}