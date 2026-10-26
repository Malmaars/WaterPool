using System;
using UnityEngine;

[Serializable]
public class Fisher : DataHolder
{
    GameObject body;
    public Fisher(GameObject myBody) { body = myBody; }
    public virtual void ReelIn(PhysicsBall toReel)
    {
        if(toReel.caughtFish != null && toReel.caughtFish.visual != null && toReel.caughtFish.name != null)
        {
            //catch the fish and calculate points?
            FishManager.Instance.CatchFish(toReel.caughtFish);
            GameManager.Instance.EditPoints(1);

            //spawn an effect
            ScorePointEffect pointVisual = EffectsManager.Instance.RequestEffect<ScorePointEffect>();
            pointVisual.myBody.transform.position = body.transform.position + Vector3.up * 3;
            OnReelIn();
            toReel.ThrowBack();
        }
    }

    public virtual void OnReelIn()
    {

    }
}
