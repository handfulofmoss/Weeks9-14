using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class MoveDuck : MonoBehaviour
{
   public Transform duck;
    public Button moveButton;

    Coroutine doTheMovingCoroutine;
    Coroutine moveTheDuckieCoroutine;

    public bool isMoving = false;
    public AnimationCurve curve;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        duck.transform.position = Vector2.zero;
    }

    public void StartDuckieMoving()
    {
        //checks if each individual coroutine is running and cancels it if so so it dosnt repeat weirldly
        if (doTheMovingCoroutine != null)
        {
            StopCoroutine(doTheMovingCoroutine);
        }
        if (moveTheDuckieCoroutine != null)
        {
            StopCoroutine (moveTheDuckieCoroutine);
        }
        doTheMovingCoroutine = StartCoroutine(DoTheMoving());
    }

        IEnumerator DoTheMoving()
    {
        isMoving = true;
        yield return moveTheDuckieCoroutine = StartCoroutine(MoveDuckie());
        isMoving = false;
    }

    IEnumerator MoveDuckie()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            //makes the local scale of tree the same as t
            duck.transform.position = new Vector2(8,2) * curve.Evaluate(t);
            yield return null;
        }
    }
     void Update()
    {
        if (isMoving == true)
        {
            moveButton.interactable = false;
        }
        if (isMoving == false)
        {
            moveButton.interactable = true;
        }
    }
}
