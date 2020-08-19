using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScaleItself : MonoBehaviour
{

     [SerializeField]
    Transform startPosition;
    Vector3 startPositionValue;
    Vector3 startScaleValue;
    Vector3 startRotationValue;

    [Header("Scale Itself")]
    [SerializeField]
    bool scaleItself;

    [SerializeField]
    Vector3 endScaleValue;

    [SerializeField]
    Tween scaleTween;

    [SerializeField]
    Ease scaleEaseFunction;

    [SerializeField]
    float scaleCycleDuration;

    [Header("Rotate Itself")]
    [SerializeField]
    bool rotateItself;

    [SerializeField]
    float rotatecycleDuration;

    Tween rotationTween;
    [SerializeField]
    bool rotateRight;
    [SerializeField]
    bool maintainChildOrientation;

    [Header("Move Itself")]
    [SerializeField]
    bool moveItself;

   
    [SerializeField]
    Vector3 distanceToMove;
    [SerializeField]
    float moveDuration;
    [SerializeField]
    Tween movementTween;
    [SerializeField]
    Ease movementEaseFunction;
    [SerializeField]
    int loopCount = -1;
    [SerializeField]
    bool rotateAround;

    // Start is called before the first frame update
    void Start()
    {

        startPositionValue = startPosition.localPosition;
        startRotationValue = startPosition.localRotation.eulerAngles;
        startScaleValue = startPosition.localScale;

        if (rotateItself)
        {
            rotationTween = transform.DORotate(new Vector3(0, 0, 360) * (rotateRight ? 1 : -1), rotatecycleDuration, RotateMode.FastBeyond360).SetRelative(true)
                .SetEase(Ease.Linear).SetLoops(-1).OnUpdate(() =>
                {
                    if (maintainChildOrientation)
                    {
                        if (transform.GetChild(0))
                        {
                            transform.GetChild(0).rotation = Quaternion.identity;
                        }
                    }
                });
        }

        if (scaleItself)
        {
            scaleTween = transform.DOScale(endScaleValue, scaleCycleDuration).SetEase(scaleEaseFunction).SetLoops(-1, LoopType.Yoyo);
        }

        if (moveItself)
        {

            if (rotateAround)
            {
                //movementTween = transform.dortoatear(startPositionValue + distanceToMove, moveDuration).
                //SetEase(easeFunction).SetLoops(loopCount,LoopType.Yoyo);
            }
            else
            {

                movementTween = transform.DOLocalMove(startPositionValue + distanceToMove, moveDuration).
                SetEase(movementEaseFunction).SetLoops(loopCount, LoopType.Yoyo);
            }

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}