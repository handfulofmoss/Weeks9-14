using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dig : MonoBehaviour
{
    //assigns the UI button
    public Button digButton;
    //bool to check if player is currently digging or not
    public bool isDigging = false;

    //assigns the player and their current position so holes can be spawned based on the players location
    public SpriteRenderer player;
    public Vector2 playerPos;
    //prefab for dug holes
    public GameObject holePrefab;
    public GameObject spawnHole;

    //counter for treasure found
    public TextMeshProUGUI counter;
    public float numTreasuresFound;
    //the chance for a treasure to be found when digging, set to zero so sound wont play by accident
    public float treasureChance = 0;

    //UnityEvents to play sound effects accordingly
    public UnityEvent onDig;
    public UnityEvent onTreasureFound;

    //coroutine for when dig animation is happening
    Coroutine doTheDigCoroutine;
    Coroutine digCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void StartDigging()
    {
        //checks if each individual coroutine is running and cancels it if so so it dosnt repeat weirldly
        if (doTheDigCoroutine != null)
        {
            StopCoroutine(doTheDigCoroutine);
        }
        if (digCoroutine != null)
        {
            StopCoroutine(digCoroutine);
        }
        doTheDigCoroutine = StartCoroutine(DoTheDigging());
    }
    IEnumerator DoTheDigging()
    {
        //will run Dig
        isDigging = true;
        yield return digCoroutine = StartCoroutine(Digging());
        isDigging = false;
    }

    IEnumerator Digging()
    {
        Debug.Log("Started Digging");
        float t = 0;
        //spawns a hole where player is standing
        spawnHole = Instantiate(holePrefab, playerPos, Quaternion.identity);
        //Invokes UnityEvent for sound effect to play for digging
        onDig.Invoke();

        //timer for how long the coroutine will run
        while (t < 1)
        {
            t += Time.deltaTime;
            yield return null;
        }
        //when digging, there is a 1 in 3 chance to find treasure
        treasureChance = Random.Range(0, 3);
        if (treasureChance >= 2)
        {
            //Invokes UnityEvent for sound effect to play for when a treasure is found
            onTreasureFound.Invoke();
            //increases the counter for number of treasures found by 1
            numTreasuresFound += 1;
        }
        Debug.Log("Finished Digging");
    }
    void Update()
    {
        //finds the players current position & adjusts the position to be where the player is standing rather then the middle of the sprite
        playerPos.x = (float)(player.transform.position.x + 0.2);
        playerPos.y = (float)(player.transform.position.y - 1.5);

        //makes the counter display the number of treasure found
        counter.text = numTreasuresFound.ToString();

        //checks if digging is happening, and disables the button temporarily until finished
        if (isDigging == true)
        {
            digButton.interactable = false;
        }
        if (isDigging == false)
        {
            digButton.interactable = true;
        }
    }
}