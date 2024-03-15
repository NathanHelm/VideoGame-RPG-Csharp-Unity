using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance;
    //particle system 
    public void PlayParticleSystem(CanvasComp canvasComp)
    {
        ParticleSystem par = canvasComp.GetComponent<ParticleSystem>();
        try
        {
            if (par == null)
            {
                throw new NullReferenceException();
            }
        }
        catch(NullReferenceException e)
        {
            Debug.LogException(e);
            Debug.LogError(e + "CANNOT FIND PARTICLE_SYSTEM COMPONENT");
        }
        par.Play();

    }
    public void StopParticleSystem(CanvasComp canvasComp)
    {
        ParticleSystem par = canvasComp.GetComponent<ParticleSystem>();
        try
        {
            if (par == null)
            {
                throw new NullReferenceException();
            }
        }
        catch (NullReferenceException e)
        {
            Debug.LogException(e);
            Debug.LogError(e + "CANNOT FIND PARTICLE_SYSTEM COMPONENT");
        }
        par.Stop();
    }
    private void OnEnable()
    {
        Instance = FindObjectOfType<UI_Manager>();
    }
}
