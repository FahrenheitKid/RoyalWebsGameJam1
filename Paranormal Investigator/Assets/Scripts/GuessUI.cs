using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Common.Enums;
using DG.Tweening;
using TMPro;

public class GuessUI : PanelUI
{

   

    [SerializeField]
    FlexibleGridLayout monsterGroup;

     [SerializeField]
    FlexibleGridLayout weaponGroup;
    [SerializeField]
    GameObject weaponPrefab;
    [SerializeField]
    GameObject monsterPrefab;
    [SerializeField]
    public MonsterBlueprint chosenWeapon;
     [SerializeField]
    public MonsterBlueprint chosenMonster;

     [SerializeField]
    Monster answerMonster;
    [SerializeField]
    MonsterBlueprint answerWeapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup()
    {
           for(int i = weaponGroup.transform.childCount - 1; i >= 0 ; i--)
        {
            Destroy( weaponGroup.transform.GetChild(i).gameObject);
        }

         for(int i = monsterGroup.transform.childCount - 1; i >= 0 ; i--)
        {
            Destroy( monsterGroup.transform.GetChild(i).gameObject);
        }


            foreach (Weapon wp in System.Enum.GetValues(typeof(Weapon)))
            {
                MonsterBlueprint go = Instantiate(weaponPrefab).transform.GetComponentInChildren<MonsterBlueprint>();
                if (go)
                {
                    GameObject parent = go.transform.parent.gameObject;
                    parent.transform.SetParent(weaponGroup.transform);
                    //need to reset this because of unity upgrade bug
                    go.transform.parent.localScale = Vector3.one;
                    go.transform.parent.DOLocalMoveZ(0,0);

                    go.Setup(wp);

                }
            }

              foreach (Monster ms in game_ref.monsters)
            {
                MonsterBlueprint go = Instantiate(monsterPrefab).transform.GetComponentInChildren<MonsterBlueprint>();
                if (go)
                {
                    GameObject parent = go.transform.parent.gameObject;
                    parent.transform.SetParent(monsterGroup.transform);
                    //need to reset this because of unity upgrade bug
                    go.transform.parent.localScale = Vector3.one;
                    go.transform.parent.DOLocalMoveZ(0,0);

                    go.Setup(ms.monsterData.monster,ms.monsterName);

                }
            }
    }

     public override void TogglePanel()
    {
        base.TogglePanel();


            foreach(MonsterBlueprint gr in weaponGroup.GetComponentsInChildren<MonsterBlueprint>(true))
            {
                
                if( gr.GetComponentInParent<GraphicRaycaster>())
                gr.GetComponentInParent<GraphicRaycaster>().enabled = isOn;

                if( gr.nameTitle.GetComponentInParent<GraphicRaycaster>())
                 gr.nameTitle.GetComponentInParent<GraphicRaycaster>().enabled = false;
            }

             foreach(MonsterBlueprint gr in monsterGroup.GetComponentsInChildren<MonsterBlueprint>(true))
            {
                 if( gr.GetComponentInParent<GraphicRaycaster>())
                gr.GetComponentInParent<GraphicRaycaster>().enabled = isOn;

                if( gr.nameTitle.GetComponentInParent<GraphicRaycaster>())
                 gr.nameTitle.GetComponentInParent<GraphicRaycaster>().enabled = false;
            }

            if(isOn)
            {
                 game_ref.SetTitleTexts(TextsHolder.EnqueueInOrder( game_ref.textsObject.guessInstructions ));
            }
        
    }

    public void SetWeapon(MonsterBlueprint weapon_)
    {

        if(!weapon_ || weapon_ == chosenWeapon) 
        {
             if(chosenWeapon)
        {
            chosenWeapon.ShowExclamationMark(false);
        }
        chosenWeapon =null;
            return;
        }
        if(!weapon_.isWeapon) return;

        if(chosenWeapon)
        {
            chosenWeapon.ShowExclamationMark(false);
        }

        chosenWeapon = weapon_;
        chosenWeapon.ShowExclamationMark(true);

        AudioPlayer.Instance()?.Play(UISFXs.selectGuess);

        
        if(answerWeapon)
        {
            answerWeapon.Setup(chosenWeapon.weapon);
        }


    }

     public void SetMonster(MonsterBlueprint monster_)
    {
        if(!monster_ || monster_ == chosenMonster)
        {
              if(chosenMonster)
        {
            chosenMonster.ShowExclamationMark(false);
        }
            chosenMonster = null;
            return;
        } 
        if(monster_.isWeapon) return;

        if(chosenMonster)
        {
            chosenMonster.ShowExclamationMark(false);
        }

        chosenMonster = monster_;
        chosenMonster.ShowExclamationMark(true);

        AudioPlayer.Instance()?.Play(UISFXs.selectGuess);

         if(answerMonster)
        {
            Monster m = game_ref.monsters.Find(x => x.monsterName == chosenMonster.monsterName && x.monsterData.monster == chosenMonster.monster);
            answerMonster.BuildCharacter(game_ref.monsterDataObject.GetMonsterData(m.monsterData.monster),m.weapons,m.places,m.monsterName);
        }


    }

    


}
