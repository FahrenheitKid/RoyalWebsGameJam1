﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using Common.Enums;

public class MonsterButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        print("entrei from monster" + eventData.pointerEnter.name);
        if(monster)
        {
            monster.game_ref.HideOtherPopUps(monster);
            if(monster.useFade == false) return;
            
             if(monster.wasAsked)
             {
                  if(monster.isDead == false)
                    monster.ShowBasicInfo(true);

                    
             }
            
                if((monster.wasInterrogated == false && monster.isDead) || ( monster.isDead == false && monster.wasAsked == false))
                {
                    if(monster.isQuestionPopUpOn == false)
                    monster.ShowQuestionMark(true);
                }
            

            if(monster.game_ref.isSacrificeMode == false)
            {
                
            }
            else
            {

            }

            if(monster.isSacrifice)
            {
                monster.ShowTalkBubble(true);
            }
            else
            {
                if(monster.isDead && monster.wasInterrogated)
                {
                    monster.ShowTalkBubbleAnswer(true);
                }
            }
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if(monster)
        {
              if(monster.useFade == false) return;
            
            if((monster.isDead && monster.isQuestionPopUpOn == true) == false)
            monster.HideAllPopUps(monster.isSacrifice);
            
           
        }
    }

}
