using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ScrollButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private enum ScrollButtonDirection { UP, DOWN }

    [SerializeField] private ScrollButtonDirection direction;
    [SerializeField] private float stepSize;
    [SerializeField] private float scrollFrequency;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private ScrollElementsContainer scrollElementsContainer;
    [SerializeField]
    public bool firstClick = false;

    private float signedStepSize;
    private Button button;

    [SerializeField]
    ScrollButton buttonPair;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        signedStepSize = direction == ScrollButtonDirection.DOWN ? stepSize * -1 : stepSize;
        scrollbar.onValueChanged.AddListener(val => HandleScrollViewChanged());
        scrollElementsContainer.OnContainerChildrenChanged += HandleScrollViewChanged;
    }

    /// <summary>
    /// Handle if the scroll view child count is changed.
    /// </summary>
    private void HandleScrollViewChanged()
    {
        //if(firstClick == false && buttonPair.firstClick == false) return;

        if(Mathf.Approximately(scrollbar.size, 1f))
        {
            SetButtonState(false);
        }
        else
        {
            HandleScrollValueChanged(scrollbar.value);
        }
    }

    /// <summary>
    /// Handle the scroll bar value changed.
    /// </summary>
    /// <param name="value"></param>
    private void HandleScrollValueChanged(float value)
    {

        value = value.RoundDecimalPlaces(2);
        if(direction == ScrollButtonDirection.DOWN && Mathf.Approximately(value, 0f))
        {
            SetButtonState(false);
        }
        else if(direction == ScrollButtonDirection.UP && Mathf.Approximately(value, 1f))
        {
            SetButtonState(false);
        }
        else
        {
            SetButtonState(true);
        }
    }

    /// <summary>
    /// Set the scroll button interactable state.
    /// </summary>
    /// <param name="enabled"></param>
    private void SetButtonState(bool enabled)
    {

        button.interactable = enabled;
        if(!enabled)
        {
            CancelInvoke("ScrollContent");
            StopAllCoroutines();
        }
    }

    /// <summary>
    /// Keep on scrolling as long as the user holds down the button.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if(button.IsInteractable())
        {
            if(firstClick == false) firstClick = true;
            InvokeRepeating("ScrollContent", 0f, scrollFrequency);
        }
    }

    /// <summary>
    /// Immediately stop the scrolling as the user leaves the button.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("ScrollContent");
        StopAllCoroutines();
    }

    /// <summary>
    /// Start scrolling the content.
    /// </summary>
    private void ScrollContent()
    {
        StopAllCoroutines();
        StartCoroutine(SmoothScrolling(scrollbar.value,
            Mathf.Clamp01(scrollbar.value + (signedStepSize * scrollbar.size.RoundDecimalPlaces(2))),
            scrollFrequency));
    }

    /// <summary>
    /// Smoothly scroll the content.
    /// </summary>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <param name="totalTime"></param>
    /// <returns></returns>
    private IEnumerator SmoothScrolling(float minValue, float maxValue, float totalTime)
    {
        float time = 0f;
        float normalizedTime = 0f;
        totalTime = Mathf.Abs(maxValue - minValue) * totalTime;
        while(time <= totalTime)
        {
            normalizedTime = time / totalTime;
            scrollbar.value = Mathf.Lerp(minValue, maxValue, normalizedTime);
            time += totalTime * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnDestroy()
    {
        scrollbar.onValueChanged.RemoveAllListeners();
        scrollElementsContainer.OnContainerChildrenChanged -= HandleScrollViewChanged;
    }
}
/* 

https://github.com/jinincarnate/unity-scrollbar-buttons
unity-scrollbar-buttons

A naive approach to implement smooth scrolling on Unity's Scroll Rect with buttons as unity doesn't provide buttons on its scroll bar
Features

    Should work with all unity versions.
    You can add buttons to scroll the contents of a unity scroll rect instead of just using mouse scroll or dragging the scroll bar or the content itself.
    Buttons will get disabled when you reach the end of the content or basically when you can not scroll anymore either up or down.
    Updates the button's state (that is enabling or disabling them) on runtime even if the content is added or removed dynamically.

Caveats

    While holding the button the smooth scrolling logic sometimes makes the scroll bar to slow down considerably when its about to reach the end.

How to use

    Create a new unity scroll in the scene.

    Add 'ScrollElementsContainer' component to the Scroll Rect's Content transform.

    Add two new buttons (that will be used for scrolling), most likely under the scroll rect(but doesn't matter)

    Add 'ScrollButton' component to both the buttons and assign all the exposed fields.
        choose Direction as UP or one button and DOWN for other
        assign step size as 1(tested with 1 you can experiment)
        assign scroll frequency as 1(basically if you hold the button how frequently it should take the scroll input)
        assign the scroll bar value by dragging and dropping the scroll bar for which you are adding the buttons.
        assign the Scroll elements container value by dragging and dropping the Scroll Rect's Content transform.(which already should have the 'ScrollElementsContainer' component)

    Note: A Sample scene is included in the scenes folder, please refer for the default values that must be assigned in case the 'How to use' doesn't make sense.


*/