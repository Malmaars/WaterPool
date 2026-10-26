using UnityEngine;

public class FisherObject : MonoBehaviour
{
    public Fisher myData;
    public FisherHighlighter myHighlighter;

    private void Start()
    {
        Initialize(new Fisher(this.gameObject));
    }
    public void Initialize(Fisher newData)
    {
        if (newData == null)
            myData = new Fisher(this.gameObject);

        myData = newData;
        myHighlighter.myData = myData;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            //catch the ball
            myData.ReelIn(other.gameObject.GetComponent<PhysicsBall>());
        }
    }
}
