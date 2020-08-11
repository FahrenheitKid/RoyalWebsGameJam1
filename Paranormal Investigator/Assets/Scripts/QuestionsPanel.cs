using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using Common.Enums;

public class QuestionsPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   [SerializeField]
   Monster monster;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if(monster)
        {
              if(monster.useFade == false) return;
            
            
            if(monster.isQuestionPopUpOn)
            {
                print("saí do button" + eventData.pointerEnter.name);
                monster.HideAllPopUps(monster.isSacrifice);
            }
            
            
           
        }
    }

}
