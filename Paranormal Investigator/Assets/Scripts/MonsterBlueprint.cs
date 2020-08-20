using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MonsterBlueprint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    [SerializeField]
    GuessUI guessUI_ref;
    [SerializeField]public MonsterCharacter monster;
    [SerializeField]
    public string monsterName;

    [SerializeField]
    Image image;
    [SerializeField]
    Animator animator;

    [SerializeField]
    public TextMeshProUGUI nameTitle;

     [SerializeField]
    CanvasGroup exclamationMark;
     [SerializeField]
    CanvasGroup infoGroup;

    [SerializeField]
    public Weapon? weapon = null;
    [SerializeField]
    SpritesHolder spritesHolder;

    public static float infoShowDuration = 0.5f;
    bool isMarked = false;
    public bool isWeapon;

    // Start is called before the first frame update
    void Start()
    {

        if(!guessUI_ref)
        {
            guessUI_ref = GameObject.FindGameObjectWithTag("GuessUI").GetComponent<GuessUI>();
        }
        /*

        if(weapon != null)
        {

            Setup(weapon);
           
        }
        else
        {

            Setup(monster, "oi gente");
        }
       */
           
              
           
       
    }

    public void Setup(Weapon? weapon_)
    {
        if(isWeapon)
          {
              weapon = weapon_;

               monsterName = Monster.GetWeaponName((Weapon)weapon_);
           if(spritesHolder)
           {
               image.sprite = spritesHolder.GetSprite((Weapon)weapon_);
           }
           nameTitle.text = monsterName;
          // ResizeSprite();
       }
       else return;
    }

    public void Setup(MonsterCharacter character,string name_)
    {
        if(isWeapon) return;
         if(spritesHolder)
         image.sprite = spritesHolder.GetMonsterSpriteAndAnimator(character).sprite;
               if(animator)
                  animator.runtimeAnimatorController = spritesHolder.GetMonsterSpriteAndAnimator(character).animator;

                  monsterName = name_;
                  nameTitle.text = monsterName;
                  monster = character;
                  ResizeSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void ShowExclamationMark(bool on)
    {
        exclamationMark.DOFade(on ? 1 : 0, infoShowDuration);
        exclamationMark.transform.DOLocalMoveY(on ? 0 : -50f, infoShowDuration);
    }

     public void ShowBasicInfo(bool on)
    {
        infoGroup.DOFade(on ? 1 : 0, infoShowDuration);
        infoGroup.transform.DOLocalMoveY(on ? 0 : -50f, infoShowDuration);
    }

     public void ResizeSprite()
    {
        image.rectTransform.sizeDelta = new Vector2(image.sprite.rect.width, image.sprite.rect.height) * Game.monsterSpriteSizeMultiplier / 2;

    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        
        ShowBasicInfo(true);
        AudioPlayer.Instance()?.Play(UISFXs.mouseOverButton);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
         ShowBasicInfo(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       // isMarked = !isMarked;
        //ShowExclamationMark(isMarked);

        if(guessUI_ref)
        {
            if(isWeapon)
            guessUI_ref.SetWeapon(this);
            else
            {
                  guessUI_ref.SetMonster(this);
            }
        } 
         
    }
    
}
