using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelUI : MonoBehaviour
{

     [SerializeField]
    protected Game game_ref;
    [SerializeField]
    protected CanvasGroup uiGroup;
    [SerializeField]
    protected bool isOn;
    [SerializeField]
    Vector2 onAndOffY = new Vector2(0, -200f);
    [SerializeField]
    Sequence sequence;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsOn()
    {
        return isOn;
    }

    public virtual void TogglePanel()
    {

        if(sequence != null) 
        {
            sequence.Kill();
        }
        
         if(!isOn && game_ref)
            {
                game_ref.CloseOpenPanels();
            }
        isOn = !isOn;
        sequence = DOTween.Sequence();

            sequence.Append(uiGroup.DOFade(isOn ? 1 : 0, 0.5f));
            sequence.Join(uiGroup.transform.GetChild(0).DOLocalMoveY(isOn ? onAndOffY.x : onAndOffY.y, 0.5f));
            sequence.OnKill(() => sequence = null);
            

           
    }

}
