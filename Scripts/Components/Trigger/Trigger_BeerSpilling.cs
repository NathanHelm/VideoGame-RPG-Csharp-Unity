using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_BeerSpilling : MonoBehaviour
{
    ParticleSystem p;
    private void OnEnable()
    {
        p = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(playBeerSpilling());
    }

    public IEnumerator playBeerSpilling()
    {
        p.Play();
        yield return new WaitForSeconds(2f);
        p.Stop();
    }
   
}
