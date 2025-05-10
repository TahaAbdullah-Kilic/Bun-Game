using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singleton<Stamina>
{
    public int CurrentStamina { get; private set; }
    [SerializeField] int MaxStamina = 3;
    [SerializeField] Sprite FullStaminaImage, EmptyStaminaImage;
    [SerializeField] int TimeBetweenStaminaRefresh = 3;
    int startingStamina;
    Transform staminaContainer;
    const string STAMINA_CONTAINER_TEXT = "Stamina Container";

    protected override void Awake()
    {
        base.Awake();

        startingStamina = MaxStamina;
        CurrentStamina = MaxStamina;
    }
    void Start()
    {
        staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }
    void Update()
    {
        if(CurrentStamina < MaxStamina && !PlayerHealth.Instance.IsDead)
        {
            StartCoroutine(RefreshStaminaRoutine());
        }
    }
    public void UseStamina()
    {
        CurrentStamina--;
        UpdateStaminaImages();
    }
    public void RefreshStamina()
    {
        if(CurrentStamina < MaxStamina)
        {
            CurrentStamina++;
            UpdateStaminaImages();
        }

    }
    IEnumerator RefreshStaminaRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(TimeBetweenStaminaRefresh);
            RefreshStamina();
            StopAllCoroutines();
        }
    }
    public void ReplenishStaminaOnDeath()
    {
        CurrentStamina = MaxStamina;
        UpdateStaminaImages();
    }
    void UpdateStaminaImages()
    {
        for(int i=0; i<MaxStamina; i++)
        {
            Transform child = staminaContainer.GetChild(i);
            Image image = child?.GetComponent<Image>();
            if(i <= CurrentStamina - 1) image.sprite = FullStaminaImage;
            else image.sprite = EmptyStaminaImage;
        }
    }
}
