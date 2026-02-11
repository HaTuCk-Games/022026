using DefaultNamespace.Components.Interfaces;
using System.Collections;
using UnityEngine;

public class DashAbility : MonoBehaviour, IAbility
{
    public float dashDelay;
    private float _dashTime = float.MinValue;
    public float stepInterval = 0.01f;
    public float dashSpeed = 20f;

    public void Execute()
    {
        if (Time.time < _dashTime + dashDelay) return;
        _dashTime = Time.time;
        StartCoroutine(Accelerate());
    }
    IEnumerator Accelerate()
    {
        float elapsedTime = 0f;  
        while (elapsedTime < 0.5f)  
        {
            transform.position += transform.forward * dashSpeed * stepInterval;
            elapsedTime += stepInterval;
            yield return new WaitForSeconds(stepInterval);
        }
    }

}

