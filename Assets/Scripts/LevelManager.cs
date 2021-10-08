
/*
 * ------------------------------------------
 * -- Project: First Person Shooter ---------
 * -- Author: Rubén Rodríguez Estebban ------
 * -- Date: 8/10/2021 -----------------------
 * ------------------------------------------
 */

using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Script that controls the level status
 */

public class LevelManager : MonoBehaviour
{

    // Static instance of the class
    public static LevelManager Instance;

    // Time to complete the level
    public float levelTimer = 10f;

    // Name of the scene to play
    public string levelName = "SciFi_Industrial";

    // Score to beat of the level
    public int levelScore;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the context of the level
        Instance = this;
        levelScore = 0;
        Debug.Log("Destroy the 13 barrels before the time runs out!");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if all the explosive barrels are destroyed
        if (levelScore < 13)
        {
            // Check if the time has finished
            if (levelTimer > 0f)
            {
                // Decrement the available time
                levelTimer -= Time.deltaTime;
            }
            else
            {
                // Game over because the time is zero
                SceneManager.LoadScene(levelName);
                Debug.Log("**** GAME OVER! ****");
            }
        }
        else
        {
            // Victory because all the barrels are destroyed in time
            Debug.Log("**** YOU WIN! ****");
        }
    }
}
