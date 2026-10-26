
using UnityEngine;

public class Effect : IPoolable
{
    public GameObject myBody;
    public bool active { get; set; }

    public Effect() { LoadBody(); }

    public virtual void OnUpdate()
    {

    }
    public virtual void OnEnableObject() 
    {
        EffectsManager.Instance.UpdateRunner += OnUpdate;
    }
    public virtual void OnDisableObject()
    {
        EffectsManager.Instance.UpdateRunner -= OnUpdate;
    }

    public virtual void LoadBody()
    {
        myBody = Object.Instantiate(Resources.Load<GameObject>("Effects/" + this.GetType().ToString()));
    }
}
