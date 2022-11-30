using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    private readonly string COINS_KEY = "COINS_AMOUNT";

    public int Coins { get; set; }

    private void Start()
    {
        LoadCoins();
    }

    /// <summary>
    /// Load players coin amount
    /// </summary>
    private void LoadCoins()
    {
        Coins = PlayerPrefs.GetInt(COINS_KEY);
    }

    /// <summary>
    /// Save Coin amount to file
    /// </summary>
    private void SaveCoins()
    {
        PlayerPrefs.SetInt(COINS_KEY, Coins);
    }

    /// <summary>
    /// Gives the player x amount of coins
    /// </summary>
    /// <param name="amount"></param>
    public void AddCoins(int amount)
    {
        Coins += amount;
        SaveCoins();
    }

    /// <summary>
    /// Remove coins from the player.
    /// </summary>
    /// <param name="amount">the total coins to be removed</param>
    public void RemoveCoins(int amount)
    {
        Coins -= amount;
        SaveCoins();
    }
}