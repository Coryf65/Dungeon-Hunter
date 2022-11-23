using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    private readonly string COINS_KEY = "COINS_AMOUNT";

    public int Coins { get; set; }

    private void Start()
    {
        LoadCoins();
    }

    private void LoadCoins()
    {
        Coins = PlayerPrefs.GetInt(COINS_KEY);
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt(COINS_KEY, Coins);
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        SaveCoins();
    }

    public void RemoveCoins(int amount)
    {
        Coins -= amount;
        SaveCoins();
    }
}