using TMPro;
using UnityEngine;

public class EconomyManager : Singleton<EconomyManager>
{
    TMP_Text goldText;

    const string COIN_AMOUNT_TEXT = "Gold Amount Text";
    int currentGoldAmount = 0;

    public void UpdateCurrentGold()
    {
        currentGoldAmount += 1;
        if (goldText == null)
        {
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }
        goldText.text = currentGoldAmount.ToString("D3");
    }
}
