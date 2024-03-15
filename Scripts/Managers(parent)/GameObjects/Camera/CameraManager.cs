using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraManager : MonoBehaviour
{
    private CinemachineVirtualCamera priorityCam;
    [Header("Enter all cameras in the scene + their blend settings")]
    [SerializeField]
    private List<CinemachineVirtualCamera> vcams;
    public static CameraManager Instance;
    private CinemachineVirtualCamera PriorityCam;
    private IEnumerator zoomPriority;
    public int priCamIndx;
    private Dictionary<string, int> nameToIndex = new Dictionary<string, int>();
    int prevCam = 0;

    public CinemachineVirtualCamera AddCameraToGame(float x, float y)
    {
        CinemachineVirtualCamera camVC;
        GameObject cam = Resources.Load<GameObject>("Prefab/Camera/CinemachineCamera");
        Transform t = GameObject.FindGameObjectWithTag("Camera").transform;
      
        camVC = Instantiate(cam, t).GetComponent<CinemachineVirtualCamera>();
        camVC.gameObject.transform.position = new Vector3(x, y, -10);
        nameToIndex.TryAdd(camVC.gameObject.name, getCamListSize());
        return camVC;
    }
    public void RemoveCam(string c)
    {
        try
        {
            CameraChanger(nameToIndex[c], 1);
            Destroy(vcams[nameToIndex[c]].gameObject);
            vcams.RemoveAt(nameToIndex[c]);
        }
        catch(Exception e)
        {
            Debug.LogError("cant find cam");
        }
    }
    private void OnEnable()
    {
        Instance = this; //static class
        //vcams = FindObjectsOfType<CinemachineVirtualCamera>();
        SetCameras(vcams);
        if (vcams.Count > 1)
        {
            CameraChanger(1, 0);
        }

    }
    public void AddVCam(CinemachineVirtualCamera v)
    {
        vcams.Add(v);
    }
    public void ZoomIn(float sensitivity, float minZoom, float maxZoom)
    {
        //camera zooms in to priority cam
        try
        {
            PriorityCam.m_Lens.OrthographicSize = Mathf.Lerp(minZoom, maxZoom, Time.deltaTime * sensitivity);
        }
        catch (UnassignedReferenceException e)
        {
            Debug.LogError(e.Message + " camera is null, check the camera's priority.");
        }
    }
    public void ZoomInPriority(float max, float sub, float sensitivity)
    {
        //sub can be negative to make lens larger
        if(zoomPriority != null)
        {
            StopCoroutine(zoomPriority);
        }
        StartCoroutine(zoomPriority = ZoomInPriorityCoroutine(max,sub,sensitivity));
    }
    public IEnumerator ZoomInPriorityCoroutine(float max, float sub, float sensitivity)
    {
        PriorityCam.m_Lens.OrthographicSize = max;
        float difference = max - (sub);
        if(sub == 0)
        {
            PriorityCam.m_Lens.OrthographicSize = max;
            yield return null;
        }
        while (PriorityCam.m_Lens.OrthographicSize > difference)
        {
            PriorityCam.m_Lens.OrthographicSize -= sensitivity * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    public void setPriorityCam(CinemachineVirtualCamera vCam)
    {
        //gets the camera that has priority index 1
        this.PriorityCam = vCam;
    }
    public CinemachineVirtualCamera getPriorityCam()
    {
        return PriorityCam;
    }
   
    public void CameraChanger(int changeCamZero, int changeCamPositive)
    {
        //when game starts. cam 0 will be first cam displaying
        //Debug.Log(vcams.GetHashCode() + Vcams.GetHashCode());
        prevCam = changeCamZero;
        priCamIndx = changeCamPositive;
        vcams[changeCamZero].Priority = 0;
        vcams[changeCamPositive].Priority = 1;
        setPriorityCam(vcams[changeCamPositive]);
    }
    public void SetCameras(List<CinemachineVirtualCamera> vcams)
    {
        //gets all cinemachine cameras, then sets its priority to zero. 
        for( int i = 0; i < vcams.Count; i++)
        {
            nameToIndex.TryAdd(vcams[i].gameObject.name, i);
            vcams[i].Priority = 0;
        }
    }
    public int getIndex(string name)
    {
        return nameToIndex[name];
    }
    public int getPriorityCamIndex()
    {
        return priCamIndx;
    }
    public int getPrevCam()
    {
        return prevCam;
    }
    public int getCamListSize()
    {
        return vcams.Count;
    }
    public List<CinemachineVirtualCamera> getCinemachineVirtualCameras()
    {
        return vcams;
    }

  


    //interface

}
