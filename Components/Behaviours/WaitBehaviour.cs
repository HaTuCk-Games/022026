using DefaultNamespace.Components.Interfaces;
using UnityEngine;

public class WaitBehaviour : MonoBehaviour, IBehaviour
{
    public void Behave()
    {
        Debug.Log("Wait");
    }

    public float Evaluate()
    {
        return 0.5f;
    }
}
