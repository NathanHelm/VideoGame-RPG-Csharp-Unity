using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class Change_Position_Manager : MonoBehaviour
{
    [SerializeField]
    Transform playerPos;
    public static Change_Position_Manager Instance;
    private Black_Image black_Image;
    public void MovePlayer(float x, float y)
    {

        // Debug.Log("Move Speed 1" + prev);
        black_Image.BlackToTrans(0.0001f);
        //a little zoom out
        playerPos.position = new Vector2(x, y);
        CameraManager.Instance.ZoomInPriority(CameraManager.Instance.getPriorityCam().m_Lens.OrthographicSize, 2, 2f);

    }
    public void OnEnable()
    {
        Instance = this;
        playerPos = FindObjectOfType<PlayerMovement>().transform;
        black_Image = FindObjectOfType<Black_Image>();
       

    }
    //add change to a new scene() here
}