using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dig : MonoBehaviour
{
    public Button digButton;
    public bool isDigging = false;

    public SpriteRenderer player;
    public Vector2 playerPos;
    public GameObject holePrefab;
    public GameObject spawnHole;

    public TextMeshProUGUI counter;
    public float numTreasuresFound;
    public float treasureChance = 0;

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

        treasureChance = Random.Range(0, 3);
        if (treasureChance >= 2)
        {
            numTreasuresFound += 1;
        }

        while (t < 1)
        {
            t += Time.deltaTime;
            yield return null;
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