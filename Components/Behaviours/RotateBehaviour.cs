using DefaultNamespace.Components.Interfaces;
using UnityEngine;

public class RotateBehaviour : MonoBehaviour, IBehaviour
{
    public CharacterHealth character;
    private void Start()
    {
        character = FindObjectOfType<CharacterHealth>();
    }
    public void Behave()
    {
        transform.Rotate(Vector3.up, 10);
    }

    public float Evaluate()
    {
        if (character == null) return 0;
        return 1 / (this.gameObject.transform.position - character.transform.position).magnitude;
    }
}
