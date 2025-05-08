using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] GameObject GoldCoin;
    [SerializeField] GameObject HeartPickup;
    [SerializeField] GameObject StaminaPickup;
    public void DropItems()
    {
        int randomNumber = Random.Range(1,5);
        
        switch (randomNumber)
        {
            case 1:
                int randomCoin = Random.Range(1,4);
                for (int i = 0; i < randomCoin; i++) Instantiate(GoldCoin, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(HeartPickup, transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(StaminaPickup, transform.position, Quaternion.identity);
                break; 
        }
    }
}
