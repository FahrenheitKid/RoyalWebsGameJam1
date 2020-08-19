using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using Common.Enums;


public class ButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

     [Header("Animation")]
    Vector3 iniitalScale;
    [SerializeField]
    Vector3 scaleEndValue;
    [SerializeField]
    float scaleDuration;
    [SerializeField]
    bool scaleOnHover;
    [SerializeField]
    Ease scaleEase;
    [SerializeField]
    Tween scaleTween;

    [Header("Sounds")]
    [SerializeField]
    bool everyOtherClick;

    [SerializeField]
    bool mustClick;
    [SerializeField]
    List<UISFXs> onEnterUISFXs = new List<UISFXs>();
     [SerializeField]
    List<UISFXs> onExitUISFXs = new List<UISFXs>();
     [SerializeField]
    List<gameSFXs> onEnterGameSFXs = new List<gameSFXs>();
     [SerializeField]
    List<gameSFXs> onExitGameSFXs = new List<gameSFXs>();
    
    [SerializeField]
    List<UISFXs> onClickUISFXs = new List<UISFXs>();
    
    [SerializeField]
    List<gameSFXs> onClickGameSFXs = new List<gameSFXs>();

     private void Start() {
        iniitalScale = transform.localScale;
    }

      void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if(scaleOnHover)
        {
            scaleTween = transform.DOScale(scaleEndValue,scaleDuration).SetEase(scaleEase).OnKill( () => scaleTween = null);
        }
        PlayOnEnter();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if(scaleOnHover)
        {
            scaleTween = transform.DOScale(iniitalScale,scaleDuration).SetEase(scaleEase).OnKill( () => scaleTween = null);
        }
        PlayOnExit();
    }

    public void PlayOnClick()
    {

        if(everyOtherClick)
        {
            
                mustClick = !mustClick;
                if(!mustClick) return;
            
        }
        if(!AudioPlayer.Instance()) return;

        AudioPlayer.Instance().Play(onClickGameSFXs);
        AudioPlayer.Instance().Play(onClickUISFXs);
    }

 public void PlayOnEnter()
    {
        if(!AudioPlayer.Instance()) return;

        AudioPlayer.Instance().Play(onEnterGameSFXs);
        AudioPlayer.Instance().Play(onEnterUISFXs);
    }
     public void PlayOnExit()
    {
        if(!AudioPlayer.Instance()) return;

        AudioPlayer.Instance().Play(onExitGameSFXs);
        AudioPlayer.Instance().Play(onExitUISFXs);
    }

    
    
}
