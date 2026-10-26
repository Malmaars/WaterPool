using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Pool;

public class EffectsManager : Singleton<EffectsManager>
{
    List<Type> allCurrentPools;
    Dictionary<Type, object> allEffectPools;

    public delegate void MyDelegate();
    public MyDelegate UpdateRunner;
    
    private void Awake()
    {
        allCurrentPools = new List<Type>();
        allEffectPools = new Dictionary<Type, object>();
    }

    private void Update()
    {
        UpdateRunner?.Invoke();
    }


    void CheckIfPoolExists<T>() where T : IPoolable
    {
        if (!allEffectPools.TryGetValue(typeof(T), out var pool))
        {
            pool = new ObjectPool<T>();
            allEffectPools[typeof(T)] = pool;
            allCurrentPools.Add(typeof(T));
        }
    }

    public T RequestEffect<T>() where T : IPoolable
    {
        CheckIfPoolExists<T>();

        return ((ObjectPool<T>)allEffectPools[typeof(T)]).RequestItem();
    }
    public void ReturnEffect<T>(T _toReturn) where T : IPoolable
    {
        CheckIfPoolExists<T>();

        ((ObjectPool<T>)allEffectPools[typeof(T)]).ReturnObjectToPool(_toReturn);
    }
}
