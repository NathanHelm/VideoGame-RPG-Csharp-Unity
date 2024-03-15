using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameStage_Environment : Gameplay_Object
{
    int prevcam = 0;
    CinemachineVirtualCamera cinemachineVirtualCamera = null;
    public void OnEnable()
    {
        CameraManager.Instance.RemoveCam("CinemachineCamera(Clone)");
        transform.position = new Vector3(0, 0, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        cinemachineVirtualCamera = CameraManager.Instance.AddCameraToGame(transform.position.x,transform.position.y);
        cinemachineVirtualCamera.Priority = 0;
        cinemachineVirtualCamera.m_Lens.OrthographicSize = 130;
        CameraManager.Instance.AddVCam(cinemachineVirtualCamera);
        
      
    }
    public void DisplayCam()
    {
        prevcam = CameraManager.Instance.getPrevCam();

        CameraManager.Instance.CameraChanger(CameraManager.Instance.getPriorityCamIndex(), CameraManager.Instance.getCamListSize() - 1);
        cinemachineVirtualCamera.Priority = 10;
    }
    public void DontDisplayCam()
    {
        CameraManager.Instance.SetCameras(CameraManager.Instance.getCinemachineVirtualCameras());
        CameraManager.Instance.CameraChanger(CameraManager.Instance.getPriorityCamIndex(), CameraManager.Instance.getPrevCam());

    }


}
