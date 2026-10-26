using System.Collections;
using System.Timers;
using TMPro;
using UnityEngine;

public class ScorePointEffect : Effect
{
    TextMeshPro myText;
    Camera theCamera;
    float fadeSpeed = 5;
    float viewTime = 1;

    public ScorePointEffect() : base()
    {
        myText = myBody.GetComponent<TextMeshPro>();
        theCamera = Camera.main;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        myBody.transform.forward = (myBody.transform.position - theCamera.transform.position).normalized;
        myBody.transform.up = theCamera.transform.up;
    }

    IEnumerator RunEffect()
    {
        while (myText.color.a < 1)
        {
            myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, myText.color.a + Time.deltaTime * fadeSpeed);
            yield return new WaitForEndOfFrame();
        }

        float viewTimer = viewTime;

        while(viewTimer > 0)
        {
            viewTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        while (myText.color.a > 0)
        {
            myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, myText.color.a - Time.deltaTime * fadeSpeed);
            yield return new WaitForEndOfFrame();
        }

        EffectsManager.Instance.ReturnEffect<ScorePointEffect>(this);
    }

    public override void OnEnableObject()
    {
        base.OnEnableObject();
        //reset the values
        myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, 0);
        CoroutineManager.Instance.StartCoroutine(RunEffect());
    }
}
