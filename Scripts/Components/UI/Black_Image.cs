using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class Black_Image : MonoBehaviour
{
    [SerializeField]
    Image image;
    [SerializeField]
    PlayerMovement p;
    float prev = 0;
    private CinemachineBrain cinemachineBrain;
    private byte[] RGBA = new byte[3];
    public void BlackToTrans(float speed)
    {
        StartCoroutine(BlackToTransAnimation(speed));
    }
    public IEnumerator BlackToTransAnimation(float speed)
    {
        cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        p = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>(); 
        prev = p.getMoveSpeed();
        p.setMoveSpeed(0);
        byte alpha = 255;
        while (alpha > 0)
        {
        //    Debug.Log("less alpha" + alpha);
           
            alpha -= 1;
            image.color = new Color32(RGBA[0], RGBA[1], RGBA[2], alpha);
            yield return new WaitForSeconds(speed);

        }
        p.setMoveSpeed(prev);
        cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
        yield return null;
    }
    public IEnumerator OpagueToTransparent()
    {
        byte alpha = 255;
        while(alpha > 0)
        {
            alpha -= 1;
            image.color = new Color32(RGBA[0], RGBA[1], RGBA[2], alpha);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
    public void setOpague()
    {
        image.color = new Color(RGBA[0], RGBA[1], RGBA[2], 255);
    }


    private void OnEnable()
    {
        cinemachineBrain = FindObjectOfType<CinemachineBrain>().GetComponent<CinemachineBrain>();
        image = gameObject.GetComponent<Image>().GetComponent<Image>();
        RGBA[0] = ((byte)GetComponent<Image>().color.r);
        RGBA[1] = ((byte)GetComponent<Image>().color.g);
        RGBA[2] = ((byte)GetComponent<Image>().color.b);
    }
}
