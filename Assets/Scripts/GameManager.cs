using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton part
    private static GameManager _instance = null;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public static GameManager instance()
    {
        return _instance;
    }

    //Rest of the class

    public GameObject pipePrefab;
    public PlayerController player;
    public float spawnRange = 3f; //sparation
    public float timeBetweenSpanws = 3f; //delay between spawns
    private float spawnTime = 0f; //timer for spawns

    public bool dead { get; set; }

    public bool moving { get; set; }

    State gameState = new Play();


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        gameState = gameState.process();
    }

    public void gameOver()
    {
        player.gameObject.SetActive(false);
        //TODO do something else...like show gameOver screen, switch scenes, whatever
    }

    public void death()
    {
        dead = true;
        moving = false;
        gameOver();
        //TODO play a particle effect, or a sound...whatever
    }

    public void reset()
    {
        this.dead = false;
        this.moving = true;
        player.gameObject.SetActive(true);
        player.gameObject.transform.position = Vector3.zero;
    }

    //called every frame
    public void spawnPipe()
    {
        if (spawnTime <= 0)
        {
            //do all the spawning
            // |    |
            // |
            //      |
            //      |
            // |    |
            // |    |

            float yTop = UnityEngine.Random.Range(-spawnRange, spawnRange);
            float xTop = 30f; //spawn somewhere to the right of the player, off the screen
            Instantiate(pipePrefab, new Vector3(xTop, yTop, 0f), Quaternion.identity);

            float yBot = yTop - 2f - Random.Range(0f, 2f);
            Instantiate(pipePrefab, new Vector3(xTop, yBot, 0f), Quaternion.Euler(180f, 90f, 0f));

            spawnTime = timeBetweenSpanws;
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }


    }

}
