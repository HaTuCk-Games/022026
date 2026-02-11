using UnityEngine;
using DG.Tweening;

public class FallingBlock : MonoBehaviour
{
    public Vector3 moveOffset = new Vector3(0f, 3f, 0f);  
    public float moveDuration = 2f;                                                            
    private Vector3 startPosition;
    private Tweener tween;

    void Start()
    {
        startPosition = transform.position;
        StartAnimation();
    }

    void StartAnimation()
    {
        tween = transform.DOMove(
                startPosition + moveOffset,
                moveDuration
            )
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => {
                transform.DOMove(
                    startPosition,
                    moveDuration
                )
                .SetEase(Ease.InOutQuad)
                .OnComplete(StartAnimation);  
            });
    }
}
