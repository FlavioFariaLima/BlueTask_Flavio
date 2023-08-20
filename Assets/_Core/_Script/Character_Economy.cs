using UnityEngine;
using TMPro;

public class Character_Economy : MonoBehaviour
{
    public int coins = 10;

    [Header("UI References")]
    public TextMeshProUGUI coinText;

    private void Start()
    {
        UpdateCoinText();
    }

    public bool CanAfford(int amount) => coins >= amount;

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinText();
    }

    public void DeductCoins(int amount)
    {
        if (CanAfford(amount))
        {
            coins -= amount;
            UpdateCoinText();
        }
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = coins.ToString();
        }
    }
}
