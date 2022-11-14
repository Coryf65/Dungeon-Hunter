using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Settings")]
    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI healthValuetext;

    private float _currentHealth;
    private float _maxHealth;

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        _currentHealth = currentHealth;
        _maxHealth = maxHealth;
    }

    private void UpdateUI()
    {
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, _currentHealth / _maxHealth, 10f * Time.deltaTime);
    }
}