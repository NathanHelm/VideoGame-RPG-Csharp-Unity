using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Page : MonoBehaviour
{
    private string translation;
    float speed = 300;
    private Sprite sprite;
    RectTransform rect;
    Image img;
    TextMeshProUGUI textUI;

    public void SetUp(PageData p)
    {
        img = GetComponentInChildren<Image>();
        this.sprite = p.getSprite();
        this.translation = p.getTranslationText();
        img.sprite = sprite;

    }

    private void OnEnable()
    {
        rect = GetComponent<RectTransform>();
    }
    public void FlipRight()
    {
        StopAllCoroutines();
        StartCoroutine(FlipPageRightCoroutine(speed));
    }
    public void FlipLeft()
    {
        StopAllCoroutines();
        StartCoroutine(FlipPageLeftCoroutine(speed));
    }
    private IEnumerator FlipPageLeftCoroutine(float senstivity)
    {
        //preprocess the rotation
       
            Debug.Log("rect rot left" + transform.localEulerAngles.y);
            if (transform.eulerAngles.y == 359)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            while (transform.rotation.eulerAngles.y <= 180)
            {
                transform.Rotate(Vector3.up, Time.fixedDeltaTime * senstivity);
                yield return new WaitForFixedUpdate();
            }
       
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    private IEnumerator FlipPageRightCoroutine(float senstivity)
    {
        //preprocess the rotation
        Debug.Log("t.r.e.y" + transform.rotation.eulerAngles.y);
      
        while (Mathf.Round(transform.rotation.eulerAngles.y) <= 330)
        {
            Debug.Log("t.r.e.y going" + transform.rotation.eulerAngles.y);
            transform.Rotate(Vector3.up, Time.fixedDeltaTime * senstivity);
            yield return new WaitForFixedUpdate();
        }
      
        transform.rotation = Quaternion.Euler(0, 359, 0);
    }
    public string getTranslation()
    {
        return translation;
    }

}
