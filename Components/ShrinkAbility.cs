using DefaultNamespace.Components.Interfaces;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
public class ShrinkAbility : MonoBehaviour, IAbility
{
    public float scaleFactor = 0.2f;

    private Vector3 startScale;
    private bool started = false;
    
    void Start()
    {
        startScale = transform.localScale;
    }
    public void Execute()
    {
        if (started) return;
        started = true;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(startScale * scaleFactor, 0.3f));
        sequence.Append(transform.DOScale(startScale, 0.3f));
    }
   
}
