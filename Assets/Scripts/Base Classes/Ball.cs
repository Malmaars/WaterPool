using UnityEngine;

public class Ball : DataHolder
{
    public PhysicsBall physicsBall;

    public Ball(PhysicsBall _physicsBall)
    {
        physicsBall = _physicsBall;
    }

    public virtual void OnCatch() { }

    public virtual void OnAnyContact() { }

    public virtual void OnFishingHoleContact() { }

    public virtual void OnBallContact() { }
}
