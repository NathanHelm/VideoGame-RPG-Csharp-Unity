using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Camera : MonoBehaviour
{
    [SerializeField]
    int cameraIndex;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraManager.Instance.CameraChanger(0, cameraIndex);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraManager.Instance.CameraChanger(cameraIndex, 0);
        }
    }
}
