using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Player Info UI")][Space]
    [Header("Health Settings")]
    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI _healthValueText;
    [Header("Shield Settings")]
    [SerializeField] private Image _shieldBar;
    [SerializeField] private TextMeshProUGUI _shieldValueText;
    [Header("Weapon")]
    [SerializeField] private TextMeshProUGUI _currentAmmoText;

    private float _currentHealth;
    private float _maxHealth;
    private float _currentShield;
    private float _maxShield;
    private float _currentAmmo;
    private float _maxAmmo;

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateHealth(float currentHealth, float maxHealth, float currentShield, float maxShield)
    {
        _currentShield = currentShield;
        _maxShield = maxShield;
        _currentHealth = currentHealth;
        _maxHealth = maxHealth;
    }

    public void UpdateAmmo(int currentAmmo, int maxAmmo)
    {
        _currentAmmo = currentAmmo;
        _maxAmmo = maxAmmo;
    }

    private void UpdateUI()
    {
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, _currentHealth / _maxHealth, 10f * Time.deltaTime);
        // display health like 10/10
        _healthValueText.text = $"{_currentHealth} / {_maxHealth}";

        _shieldBar.fillAmount = Mathf.Lerp(_shieldBar.fillAmount, _currentShield / _maxShield, 10f * Time.deltaTime);
        _shieldValueText.text = $"{_currentShield} / {_maxShield}";

        _currentAmmoText.text = $"{_currentAmmo} / {_maxAmmo}";
    }
}