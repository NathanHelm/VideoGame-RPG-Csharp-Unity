using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IAct
{
    public IEnumerator PlayAct(bool stopOrPlayAct);
}
