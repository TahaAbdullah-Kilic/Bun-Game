using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject SettingsPlane;
    [SerializeField] GameObject ActiveWeapon;
    bool MenuActivated = false;
    void Start()
    {
        SettingsPlane.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !MenuActivated)
        {
            Time.timeScale = 0;
            ActiveWeapon.GetComponent<ActiveWeapon>().enabled = false;
            SettingsPlane.SetActive(true);
            MenuActivated = true;
            SettingsPlane.transform.GetChild(0).gameObject.SetActive(true);
            SettingsPlane.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && MenuActivated)
        {
            Time.timeScale = 1;
            ActiveWeapon.GetComponent<ActiveWeapon>().enabled = true;
            SettingsPlane.SetActive(false);
            MenuActivated = false;
            SettingsPlane.transform.GetChild(0).gameObject.SetActive(true);
            SettingsPlane.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void ResumeButton()
    {
        Time.timeScale = 1;
        ActiveWeapon.GetComponent<ActiveWeapon>().enabled = true;
        SettingsPlane.SetActive(false);
        MenuActivated = false;
        SettingsPlane.transform.GetChild(0).gameObject.SetActive(true);
        SettingsPlane.transform.GetChild(1).gameObject.SetActive(false);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
