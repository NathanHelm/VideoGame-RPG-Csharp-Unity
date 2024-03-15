using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NPC_Manager: MonoBehaviour
{
    public static NPC_Manager Instance;
    IEnumerator moveNPC;
    NPC prev;
    public void MoveNPC(NPC nPC,float x, float y, float moveSpeed, GenericEventMethod[] g)
    {
        nPC = setNPCToDialogueNPC(nPC);
        if (prev == null) //is the previous null? [there is no previous movement]
        {
            prev = nPC;  //then set the previous to the current. 
        }
        else if(prev.Equals(nPC)) //is previous npc moving == to the npc we are going to move now?
        {
            Debug.Log("stop moving bozo");
            StopCoroutine(moveNPC); //stop current movement so we can move it else where. 
        }
        prev = nPC;
        StartCoroutine(moveNPC = MoveNPCCoroutine(nPC, x, y, moveSpeed,g));
    }
    public void ChangeDialogue(NPC nPC,string newKey, GenericEventMethod[] g)
    {
        nPC = setNPCToDialogueNPC(nPC);
        nPC.setDialogueKey(newKey);

        if (g != null)
        {
            IEvent.e.pushEvents(g);
            IEvent.e.PlayEvent();
        }
    }
    private NPC setNPCToDialogueNPC(NPC nPC)
    {
        Debug.Log("catching " + NPC_Dictionary.Instance.getDialogueNPC());
        if (nPC == null)
            {
            return NPC_Dictionary.Instance.getDialogueNPC();
            }
            else
            {
           
            return nPC;
            }
            
            
    }
    public void StopDialogue(NPC nPC)
    {
        nPC = setNPCToDialogueNPC(nPC);
        nPC.setDialogueKey("");
    }
    public void StopTrigger(TriggerEvent nPC)
    {
        nPC.setKey("");
    }
    public void DestroyNpc(NPC nPC)
    {

        nPC = setNPCToDialogueNPC(nPC);
        Destroy(nPC.gameObject);
    }    
    public void TriggerDialog(NPC nPC)
    {
        nPC = setNPCToDialogueNPC(nPC);
        nPC.RunTriggerDialogue();
    }
    public void ChangeTriggerEvent(NPC npc, string s)
    {
        setNPCToDialogueNPC(npc);
        npc.setKey(s);
    }
  
    public void PlayAnimation(NPC nPC, bool loop, string animationName, GenericEventMethod[] g)
    {
        nPC = setNPCToDialogueNPC(nPC);
        Debug.Log("animation has started..."); 
        Animator animator = GetAnimator(nPC);
       // int animatorStateInfo = Animator.StringToHash(animationName);
        animator.Play(animationName);
        StartCoroutine(DelayedAnimation(animator, g));
        //todo

    }
    public IEnumerator DelayedAnimation(Animator anim, GenericEventMethod[] g)
    {
        while(!anim.IsInTransition(0))
        {
//            Debug.Log("delayed_Animation");
            yield return new WaitForFixedUpdate();
        }
        if (g != null)
        {
            IEvent.e.pushEvents(g);
            IEvent.e.PlayEvent();
        }
        yield return null;
    }
    public void StopAnimation(NPC npc, GenericEventMethod[] g)
    {
        npc = setNPCToDialogueNPC(npc);
        Debug.Log("animation has stopped");
        Animator animator = GetAnimator(npc);
        StartCoroutine(DelayedAnimation(animator,g));
        animator.Play("Idle");
    }
    public void ChangeDialogueAndTriggerDialogue(NPC nPC,string newKey, GenericEventMethod[] gs)
    {
        nPC = setNPCToDialogueNPC(nPC);
        ChangeDialogue(nPC, newKey, gs);
        TriggerDialog(nPC);
       
    }
    public void ChangeSprite(NPC nPC, string spritePath)
    {
        nPC = setNPCToDialogueNPC(nPC);
        Sprite s = Resources.Load<Sprite>("NPCSprite/" + spritePath);
        nPC.setNPCSprite(s);
    }
    public void ChangeGravityScale(NPC nPC, float gravityScale, GenericEventMethod[] genericEventMethods)
    {
        nPC = setNPCToDialogueNPC(nPC);
        StartCoroutine(ChangeGravityScaleEnumerator(nPC,gravityScale,genericEventMethods));  
    }
    public void ScaleNPC(NPC nPC, float scale, float maxScale)
    {
        nPC = setNPCToDialogueNPC(nPC);
        StartCoroutine(ScaleAmount(nPC, scale, maxScale));
    }
    public void BlackToTrans(float time)
    {
        float timetemp = time / 256;
        Black_Image img = FindObjectOfType<Black_Image>().GetComponent<Black_Image>();
        if(img == null)
        {
            throw new NullReferenceException();
        }
        img.BlackToTrans(timetemp);
        
    }
    Animator GetAnimator(NPC nPC)
    {
        try
        {
            Animator animator = nPC.gameObject.GetComponent<Animator>();
            if (animator == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return animator;
            }
        }
        catch (NullReferenceException e)
        {
            Debug.LogException(e);
        }
        return null;
    }

    private IEnumerator ScaleAmount(NPC npc, float scale,float maxScale)
    {
        float transformScaleX = npc.transform.localScale.x * maxScale;
        float transformScaleY = npc.transform.localScale.y * maxScale;
        float sizeX = 0, sizeY = 0;
        while (npc.transform.localScale.y <= transformScaleY && npc.transform.localScale.x <= transformScaleX)
        {
            sizeX += scale * Time.deltaTime;
            sizeY += scale * Time.deltaTime;
            npc.transform.localScale = new Vector3(npc.transform.localScale.x + scale, npc.transform.localScale.x + scale);
            yield return new WaitForFixedUpdate();
        }
    }
    public IEnumerator AnimationEnumerator()
    {
        yield return null;
    }
    public void lowerLighting(NPC nPC, float brightnessAmount)
    {
        Light2D light = nPC.GetComponent<Light2D>();
        light.intensity = brightnessAmount;
    }

    public IEnumerator ChangeGravityScaleEnumerator(NPC npc, float gravityScale, GenericEventMethod[] genericEventMethods)
    {
        npc.setTrigger(false);
        Rigidbody2D rigidbody2D = npc.GetComponent<Rigidbody2D>();
        try
        {
            if (rigidbody2D == null)
            {
                throw new NullReferenceException();
            }
        }
        catch (NullReferenceException e)
        {
            yield break;
            Debug.Log("rigidbody not found" + e);
        }
        rigidbody2D.mass = 20;
        rigidbody2D.gravityScale += gravityScale;
    
        Debug.Log("GRAVITY SCALE GOING");
        Debug.Log("(5)");
        yield return new WaitForSeconds(8f);
        //Destroy(npc.gameObject);
    }

    public IEnumerator MoveNPCCoroutine(NPC nPC, float x, float y, float moveSpeed, GenericEventMethod[] g)
    {
        Debug.Log("npc moving");
        nPC.gameObject.layer = LayerMask.NameToLayer("NoHit");
        Vector3 destination = new Vector3(x, y, 0);
        float speed = Time.deltaTime * moveSpeed;
        while (Vector3.Distance(nPC.transform.position, destination) > 0.1f)
        {
            Debug.Log("really quick: " + x + " " + y + " " + destination);
            nPC.transform.position = Vector3.MoveTowards(nPC.transform.position, destination, speed);
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("made it");
        nPC.gameObject.layer = LayerMask.NameToLayer("Default");
        if (g != null)
        {
            IEvent.e.pushEvents(g);
            IEvent.e.PlayEvent();
        }
        yield return null;
    }

    private void OnEnable()
    {
        Instance = FindObjectOfType<NPC_Manager>();
    }


}
