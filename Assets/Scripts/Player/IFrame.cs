using System.Collections;
using UnityEngine;

public class IFrame : MonoBehaviour
{
    [SerializeField] float IframeTime = 1f;

    public bool canTakeDamage {get; set;}
    void Start()
    {
        canTakeDamage = true;
    }
    public IEnumerator TakeDamageRoutine()
    {
        yield return new WaitForSeconds(IframeTime);
        canTakeDamage = true;
    }
    public bool GetcanTakeDamage()
    {
        return canTakeDamage;
    }
    public void SetcanTakeDamage(bool value)
    {
        canTakeDamage = value;
    }
}
