using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEvent
{
    public static IEvent e = new IEvent();
    public Queue<GenericEventMethod> twoParamList = new Queue<GenericEventMethod>();

    public void PlayEvent()
    {
        //make an array of object, type Event
        //[] [] [] [] []
        //when first method is done. pop first element
        // *pop*[] [] [] []
      while(twoParamList.Count > 0)
      {
            Debug.Log("play event still running");
            twoParamList.Dequeue().playGenericMethod();
      }
    }
    public void pushEvents(GenericEventMethod[] gs)
    {
        foreach(GenericEventMethod single in gs)
        {
            twoParamList.Enqueue(single);
        }
    }
  
}
public class FiveParameterMethod<K, V, K2, V2, K3> : GenericEventMethod
{
    //when "TwoParameterMethod" is invoked it instantly gets added to the queue.

    public K Param1 { get; set; }
    public V Param2 { get; set; }
    public K2 Param3 { get; set; }
    public V2 Param4 { get; set; }
    public K3 Param5 { get; set; }
    public Action<K, V, K2, V2, K3> action;
    public FiveParameterMethod(Action<K, V, K2, V2,K3> action, K k, V v, K2 k1, V2 v2, K3 k3)
    {
        this.action = action;
        Param1 = k;
        Param2 = v;
        Param3 = k1;
        Param4 = v2;
        Param5 = k3;
        // IEvent.e.twoParamList.Enqueue(this);
//        Debug.Log(Param1 + " " + Param2);
    }

    public void playGenericMethod()
    {
        action(Param1, Param2, Param3, Param4, Param5);

    }
}

public class FourParameterMethod<K, V, K2, V2> : GenericEventMethod
{
    //when "TwoParameterMethod" is invoked it instantly gets added to the queue.

    public K Param1 { get; set; }
    public V Param2 { get; set; }
    public K2 Param3 { get; set; }
    public V2 Param4 { get; set; }
    public Action<K,V,K2,V2> action;
    public FourParameterMethod(Action<K, V, K2, V2> action, K k, V v,K2 k1, V2 v2)
    {
        this.action = action;
        Param1 = k;
        Param2 = v;
        Param3 = k1;
        Param4 = v2;
        // IEvent.e.twoParamList.Enqueue(this);
       // Debug.Log(Param1 + " " + Param2);
    }

    public void playGenericMethod()
    {
        action(Param1, Param2,Param3,Param4);

    }
}
public class ThreeParameterMethod<K, V, K2> : GenericEventMethod
{
    //when "TwoParameterMethod" is invoked it instantly gets added to the queue.

    public K Param1 { get; set; }
    public V Param2 { get; set; }
    public K2 Param3 { get; set; }
    public Action<K, V, K2> action;
    public ThreeParameterMethod(Action<K, V, K2> action, K k, V v, K2 k2)
    {
        this.action = action;
        Param1 = k;
        Param2 = v;
        Param3 = k2;
        // IEvent.e.twoParamList.Enqueue(this);
//        Debug.Log(Param1 + " " + Param2);
    }

    public void playGenericMethod()
    {
        action(Param1, Param2,Param3);

    }
}
public class TwoParameterMethod<K, V> : GenericEventMethod
{
    //when "TwoParameterMethod" is invoked it instantly gets added to the queue.

    public K Param1 { get; set; }
    public V Param2 { get; set; }
    public Action<K, V> action;
    public TwoParameterMethod(Action<K, V> action, K k, V v)
    {
        this.action = action;
        Param1 = k;
        Param2 = v;
        // IEvent.e.twoParamList.Enqueue(this);
       // Debug.Log(Param1 + " " + Param2);
    }

    public void playGenericMethod()
    {
        action(Param1, Param2);

    }
}
public class SingleParameterMethod<T> : GenericEventMethod
{
    public T Param1 { get; set; }
    public Action<T> action1;
    public SingleParameterMethod(Action<T> action, T t)
    {
        this.Param1 = t;
        this.action1 = action;
      //  IEvent.e.twoParamList.Enqueue(this);
    }
    public void playGenericMethod()
    {
        action1(Param1); 
    }

}
public class NoParameterMethod : GenericEventMethod
{
    public Action action;
    public NoParameterMethod(Action action)
    {
        this.action = action;
    }
    public void playGenericMethod()
    {
        action();
    }
}
public interface GenericEventMethod
{
    public void playGenericMethod();
}

