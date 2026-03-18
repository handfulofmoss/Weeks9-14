using System.Collections;
using UnityEngine;

public class Grower : MonoBehaviour
{
    public Transform tree;
    public Transform apple;

    Coroutine doTheGrowingCoroutine;
    Coroutine growTheTreeCoroutine;
    Coroutine growTheAppleCoroutine;
    void Start()
    {
        tree.localScale = Vector2.zero;
        apple.localScale = Vector2.zero;

    }

    public void StartTreeGrowing()
    {
        //checks if each individual coroutine is running and cancels it if so so it dosnt repeat weirldly
        if(doTheGrowingCoroutine != null)
        {
            StopCoroutine(doTheGrowingCoroutine);
        }
        if(growTheTreeCoroutine != null)
        {
            StopCoroutine(growTheTreeCoroutine);
        }
        if(growTheAppleCoroutine != null)
        {
            StopCoroutine(growTheAppleCoroutine);
        }
        doTheGrowingCoroutine = StartCoroutine(DoTheGrowing());
    }

    IEnumerator DoTheGrowing()
    {
        //will run GrowTree, and once completed will them run GrowApple
        yield return growTheTreeCoroutine = StartCoroutine(GrowTree());
        yield return growTheAppleCoroutine = StartCoroutine(GrowApple());
    }

    IEnumerator GrowTree()
    {
        Debug.Log("Started Growing Tree");
        tree.localScale = Vector2.zero;
        apple.localScale = Vector2.zero;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            //makes the local scale of tree the same as t
            tree.localScale = Vector2.one * t;
            yield return null;
        }
        Debug.Log("Finished Growing Tree");
    }

    IEnumerator GrowApple()
    {
        Debug.Log("Started Growing Apple");
        apple.localScale = Vector2.zero;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            //makes the local scale of tree the same as t
            apple.localScale = Vector2.one * t;
            yield return null;
        }
        Debug.Log("Finished Growing Apple");
    }
}
