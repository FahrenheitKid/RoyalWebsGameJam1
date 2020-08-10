using System.Collections;
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
        if(monster)
        {
            if(monster.useFade == false) return;
            
             if(monster.wasAsked)
            monster.ShowBasicInfo(true);
            else
            {
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
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if(monster)
        {
              if(monster.useFade == false) return;
            if(monster.wasAsked)
            monster.ShowBasicInfo(false);

            
                monster.ShowQuestionMark(false);

                
                monster.ShowTalkBubble(false);
            
           
        }
    }

}
