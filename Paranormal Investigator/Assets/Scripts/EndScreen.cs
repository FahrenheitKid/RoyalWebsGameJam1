using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Common.Enums;
using DG.Tweening;
using TMPro;

public class EndScreen : MonoBehaviour
{

    [SerializeField]
    Game game_ref;
    [SerializeField]
    CanvasGroup parent;
    [SerializeField]
    TextMeshProUGUI title;
     [SerializeField]
    TextMeshProUGUI subText;
    [SerializeField]
    Vector3 titleEndPos;
    Vector3 subtextInitialPos;
     Vector3 titleInitialPos;

      [SerializeField]
    Button answerButton;

      [SerializeField]
    Button playAgainButton;
      [SerializeField]
    Button quitButton;
   

    [SerializeField]
    public float fadeInDuration;
     [SerializeField]
    public float fadeOutDuration;
    [SerializeField]
    public bool canPressStart = false;
    public bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        subtextInitialPos = subText.transform.localPosition;
        titleInitialPos = title.rectTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fade(bool on)
    {
        if(on == isOn) return;
         if(on)
        {
            answerButton.transform.localScale = Vector3.zero;
            playAgainButton.transform.localScale = Vector3.zero;
            quitButton.transform.localScale = Vector3.zero;
            subText.transform.localPosition = subtextInitialPos;
              title.rectTransform.localPosition = titleInitialPos;

            Sequence fadeInSequence = DOTween.Sequence();

           fadeInSequence.Append( parent.DOFade(1,0.1f));
           fadeInSequence.Join(  title.rectTransform.DOLocalMoveY(title.rectTransform.localPosition.y - title.rectTransform.rect.height,fadeInDuration).SetEase(Ease.InOutQuad).OnComplete(()=> {
                 title.rectTransform.DOLocalMoveY(title.rectTransform.localPosition.y + 50f, fadeInDuration).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);

           }));
           fadeInSequence.Append(subText.DOFade(1,0.35f));
           fadeInSequence.Join(subText.transform.DOLocalMoveY(0,0.25f));

           fadeInSequence.OnComplete(() => {
               Sequence buttonsSequence =  DOTween.Sequence();
               buttonsSequence.Append( answerButton.transform.DOScale(Vector3.one,0.5f).SetEase(Ease.OutBounce));
               buttonsSequence.AppendInterval(0.2f);
                buttonsSequence.Append( playAgainButton.transform.DOScale(Vector3.one,0.5f).SetEase(Ease.OutBounce));
               buttonsSequence.AppendInterval(0.2f);
                buttonsSequence.Append( quitButton.transform.DOScale(Vector3.one,0.5f).SetEase(Ease.OutBounce));
               canPressStart = true;
           });

           isOn = true;

        }
        else
        {
                    Sequence fadeOutSequence = DOTween.Sequence();

           fadeOutSequence.Append( parent.DOFade(0,fadeOutDuration));
            //fadeOutSequence.Join(  title.transform.DOScale(Vector3.one * 1.5f,fadeOutDuration).SetEase(Ease.InOutQuad));
          

            fadeOutSequence.OnComplete(()=>{
                   
            });
            isOn = false;

        }

        parent.blocksRaycasts = isOn;
        parent.interactable = isOn;
         answerButton.GetComponent<GraphicRaycaster>().enabled = isOn;
            playAgainButton.GetComponent<GraphicRaycaster>().enabled = isOn;
            quitButton.GetComponent<GraphicRaycaster>().enabled = isOn;
    }


    public void ShowEndScreen(bool? victory)
    {
        if(victory == true)
        {
            title.text = "YOU WIN!";
            subText.text = "Who is Sherlock close to you?";
        }
        else if(victory == null)
        {
             title.text = "YOU LOSE...";
            subText.text = "...kinda? At least you weren't completely wrong";
        }
        else
        {
               title.text = "YOU LOSE!";
                subText.text = "May god bless the souls of the innocents you killed for this wrong guess";
        }

        AudioPlayer.Instance()?.Play( victory == true ? gameSFXs.crowdJoy : gameSFXs.crowdBoo);

        Fade(true);
    }
}
