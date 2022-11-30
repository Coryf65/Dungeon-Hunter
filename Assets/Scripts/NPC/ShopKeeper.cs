using System.Linq;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _popUpPanel;
    [SerializeField] private GameObject _shopPanel;

    [Header("Items to sell")]
    [SerializeField] private ShopItems[] _shopItems;

    [Header("refs")]
    [SerializeField] private Potion _potion;
    [SerializeField] private Shield _shield;

    private bool _canShop = false;
    private CharacterWeapon _characterWeapon;
    private Character _character;

    // Update is called once per frame
    void Update()
    {
        if (_canShop)
        {
            // player pressed key
            if (Input.GetKeyDown(KeyCode.J))
            {
                _shopPanel.SetActive(true);
                _popUpPanel.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Player walked into the shopkeeper store
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _characterWeapon = collision.GetComponent<CharacterWeapon>();
            _character = collision.GetComponent<Character>();
            _canShop = true;
            _popUpPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Hide Shop UI when the player leaves
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _characterWeapon = null;
            _canShop = false;
            _shopPanel.SetActive(false);
            _popUpPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Buy an item based on the SO_ShopItem
    /// </summary>
    /// <param name="itemName"></param>
    public void PurchaseItem(string itemName)
    {
        Debug.Log($"PurchaseItem '{itemName}'");

        ShopItems item = (from storeItem in _shopItems
                          where storeItem.ItemName == itemName
                          select storeItem).FirstOrDefault();
        if (item == null)
        {
            Debug.Log($"Item '{itemName}' not found in shop items");
            return;
        }

        if (CoinManager.Instance.Coins < item.Cost)
        {
            Debug.Log($"not enough coins '{item.name}' costs '{item.Cost}' you only have '{CoinManager.Instance.Coins}'");
            return;
        }

        // Special case for weapons
        if (itemName == "HandCannon")
        {
            _characterWeapon.SecondaryWeapon = item.WeaponToSell;
            CoinManager.Instance.RemoveCoins(item.Cost);
            return;
        }

        if (itemName == "Health")
        {
            _potion.AddHealth(_character);
            CoinManager.Instance.RemoveCoins(item.Cost);
        }

        if (itemName == "Shield")
        {
            _shield.AddShield(_character);
            CoinManager.Instance.RemoveCoins(item.Cost);
        }
    }
}