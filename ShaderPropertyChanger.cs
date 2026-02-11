using UnityEngine;

public class ShaderPropertyChanger : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer1;
    [SerializeField] private Renderer targetRenderer2;
    [SerializeField] private Renderer targetRenderer3;

    [SerializeField] private Material[] materials;

    private int currentIndex = 0;

    public void SwitchMaterial()
    {
        if (materials.Length == 0) return;
        currentIndex = (currentIndex + 1) % materials.Length;
        targetRenderer1.material = materials[currentIndex];
        targetRenderer2.material = materials[currentIndex];
        targetRenderer3.material = materials[currentIndex];
    }
}
