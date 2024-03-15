using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CardUICounter : MonoBehaviour
{
    int cardAmount = 0;
    TextMeshProUGUI textMeshProUGUI;

    public void IncreaseCount()
    {
        ++cardAmount;
        Display(cardAmount.ToString());
    }
    public void DecreaseCount()
    {
        --cardAmount;
        Display(cardAmount.ToString());
    }
    public void Display(string s)
    {
        textMeshProUGUI.text = s;
    }
    public void ResetCounter()
    {
        cardAmount = 0;
        Display(cardAmount.ToString());
    }
    public void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        MovementManager.Instance.Wobble(transform, true, Time.deltaTime, 0.5f);
        transform.eulerAngles = new Vector3(0, Time.deltaTime, 0);
        
    }
}