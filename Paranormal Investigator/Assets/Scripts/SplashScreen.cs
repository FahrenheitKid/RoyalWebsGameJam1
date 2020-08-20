using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Common.Enums;
using DG.Tweening;
using TMPro;

public class SplashScreen : MonoBehaviour
{

    [SerializeField]
    CanvasGroup parent;
    [SerializeField]
    TextMeshProUGUI clickHere;
    [SerializeField]
    Vector3 clickHereEndPos;

      [SerializeField]
    TextMeshProUGUI credits;
    [SerializeField]
    Vector3 creditsEndPos;
    [SerializeField]
    TextMeshProUGUI title;

    [SerializeField]
    public float fadeInDuration;
     [SerializeField]
    public float fadeOutDuration;
    [SerializeField]
    public bool canPressStart = false;

    // Start is called before the first frame update
    void Start()
    {
         #if UNITY_EDITOR
            fadeInDuration = 0.1f;
            fadeOutDuration = fadeInDuration / 3;
         #endif
        canPressStart = false;
        Fade(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fade(bool fadeIn)
    {
        if(fadeIn)
        {
            Sequence fadeInSequence = DOTween.Sequence();

           fadeInSequence.Append( parent.DOFade(1,fadeInDuration));
           fadeInSequence.Join(  title.transform.DOScale(Vector3.one,fadeInDuration).SetEase(Ease.OutBack).OnComplete(()=> {
                 title.rectTransform.DOLocalMoveY(title.rectTransform.localPosition.y + 50f, fadeInDuration).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);

           }));
           fadeInSequence.Append(clickHere.transform.DOLocalMoveY(clickHereEndPos.y,1f));
           fadeInSequence.Join(clickHere.DOFade(1,0.75f));

           fadeInSequence.OnComplete(() => {
               Sequence flickSequence =  DOTween.Sequence().SetLoops(-1,LoopType.Yoyo);
               flickSequence.SetDelay(0.5f);
               flickSequence.Append( clickHere.DOFade(0,0.1f).SetEase(Ease.Linear));
               flickSequence.AppendInterval(0.5f);
               
               credits.rectTransform.DOLocalMoveX(creditsEndPos.x,1f).SetEase(Ease.OutSine);
               canPressStart = true;
           });

        }
        else
        {
                    Sequence fadeOutSequence = DOTween.Sequence();

           fadeOutSequence.Append( parent.DOFade(0,fadeOutDuration));
            fadeOutSequence.Join(  title.transform.DOScale(Vector3.one * 1.5f,fadeOutDuration).SetEase(Ease.InOutQuad));
            AudioPlayer.Instance()?.Play(Soundtracks.game,fadeOutDuration * 1.5f);

            fadeOutSequence.OnComplete(()=>{
                   
            });
         

        }
    }
}
