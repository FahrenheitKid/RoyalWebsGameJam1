using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TextRevealer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{


   [SerializeField]
   TextMeshProUGUI text;
   [SerializeField]
   float revealDuration;
   [SerializeField]
   float textsInterval;
   [SerializeField]
   Queue<string> textsQueue = new Queue<string>();
   [SerializeField]
   List<string> tipsCheck = new List<string>();
   string currentText;
   [SerializeField]
   Tween textRevealTween;
   [SerializeField]
   bool loopTexts = true;
    [SerializeField]
   bool infiniteLoops = true;

   bool pointerIsDown = false;
   Timer autoTextTimer;

   bool fresh = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevealText()
    {   
         if(textRevealTween != null && !fresh)
            {
                if(textRevealTween.IsPlaying())
                {
                    return;
                }
            }
            

        if(textsQueue.Any())
        {
            
            loopTexts = textsQueue.Count > 1;
            string nextText = textsQueue.Dequeue();
            tipsCheck = textsQueue.ToList();
            if(nextText == currentText) return;
            currentText = nextText;
            
            text.maxVisibleCharacters = 0;
           
            textRevealTween = DOTween.To(()=> text.maxVisibleCharacters, x=> text.maxVisibleCharacters = x, currentText.Length, revealDuration).SetEase(Ease.Linear).OnKill(()=> textRevealTween = null);
            text.text = currentText;
             if(textsQueue.Any())
             {
                 textRevealTween.onComplete+= () =>
                 {
                     autoTextTimer = Timer.Register(textsInterval, () => {RevealText();
                     autoTextTimer.Cancel();
                     
                     },null,true);
                 };
                  
             }
           
           

            if(loopTexts && textsQueue.Any()) textsQueue.Enqueue(nextText);
            tipsCheck = textsQueue.ToList();

            fresh = false;
        }
    }

    public void StopRevealing()
    {
            if(autoTextTimer != null)
            autoTextTimer.Cancel();

            fresh = true;
        
    }

    public void ClearTexts()
    {
        StopRevealing();
        textsQueue = new Queue<string>();
        if(textRevealTween != null)
        {
            textRevealTween.Kill();
        }
        currentText = "";
        text.text = "";

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerIsDown = true;
        //print("pressed down at" + eventData.pressPosition + " / " + backgroundImage.transform.position);
        //print("mouse pos" + Input.mousePosition);

        /*

        if(UtilityTools.isMousePointInsideRectTransform(text.rectTransform,eventData.pressPosition))
        {
             print("insiiide");
            if(textRevealTween != null)
            {
                if(textRevealTween.IsPlaying())
                {
                    print("accelerating");
                    textRevealTween.Goto(textRevealTween.Elapsed() + revealDuration * 0.3f);
                }
                else
                {
                    autoTextTimer.Cancel();
                    RevealText();
                }
            }
            else
            {
                RevealText();
            }
        }
        */

    }

    public void OnPointerUp(PointerEventData eventData)
    {
       pointerIsDown = false;
    }

    public void SetQueue(Queue<string>queue)
    {
        textsQueue = queue;
    }
}
