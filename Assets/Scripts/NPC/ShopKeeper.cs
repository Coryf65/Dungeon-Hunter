using System.Linq;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    // show popup with shop ui
    [Header("UI")]
    [SerializeField] private GameObject _popUpPanel;
    [SerializeField] private GameObject _shopPanel;

    [Header("Items to sell")]
    [SerializeField] private ShopItems[] _shopItems;

    private bool _canShop = false;
    private CharacterWeapon _heldWeapon;

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
            _heldWeapon = collision.GetComponent<CharacterWeapon>();
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
            _heldWeapon = null;
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
        ShopItems item = (ShopItems)(from storeItem in _shopItems
                                     where storeItem.ItemName == itemName
                                     select storeItem).FirstOrDefault();
        if (item == null)
        {
            Debug.Log($"Item '{itemName}' not found in shop items");
            return;
        }

        if (CoinManager.Instance.Coins >= item.Cost)
        {
            // Special case for weapons
            if (itemName == "HandCannon")
            {
                _heldWeapon.SecondaryWeapon = item.WeaponToSell;
                return;
            }
            // all other items that apply

            // Health

            // Shield

        }
    }
}