using UnityEngine;

public class Ball : DataHolder
{
    public PhysicsBall physicsBall;

    public Ball(PhysicsBall _physicsBall)
    {
        physicsBall = _physicsBall;
    }
}
