using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private Settings settings;
    [SerializeField] private ShootAbility shootAbility;

    [Header("Health Settings")]
    [SerializeField, Range(1, int.MaxValue)] private int maxHealth = 100;
    [SerializeField] private bool destroyOnDeath = true;

    private int currentHealth;
    private ViewModel viewModel;
    private bool isDead;

    public int Health => currentHealth;
    public bool IsDead => isDead;

    private void Awake()
    {
        InitializeHealth();
        CacheViewModel();
    }

    private void InitializeHealth()
    {
        if (settings != null)
            currentHealth = settings.HeroHealth;
        else
            currentHealth = maxHealth;

        isDead = false;
    }

    private void CacheViewModel()
    {
        viewModel = FindObjectOfType<ViewModel>();
        if (viewModel != null)
            UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isDead || damage <= 0) return;

        currentHealth = Mathf.Max(0, currentHealth - damage);
        UpdateHealthUI();
        CheckDeath();
    }

    public void Heal(int amount)
    {
        if (isDead || amount <= 0) return;

        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (viewModel != null)
            viewModel.Health = currentHealth.ToString();
    }

    private void CheckDeath()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        if (destroyOnDeath)
        {
            Destroy(gameObject);
        }
        else
        {
            
        }
    }
}