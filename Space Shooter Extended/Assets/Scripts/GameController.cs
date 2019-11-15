using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text RestartText;
    public Text EndText;

    private int score;
    private bool restart;
    private bool gameOver;

    void Start()
    {
        restart = false;
        gameOver = false;
        RestartText.text = "";
        EndText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());     
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                SceneManager.LoadScene("Space Shooter");
            }
        }
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        { 
            for (int i=0; i< hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                RestartText.text = "Press 'P' to Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            EndText.text = "You win! Game created by Yi Chen";
            gameOver = true;
            restart = true;
        }
    }

    public void Gameover ()
    {
        EndText.text = "Game Over!";
        gameOver = true;
    }
}
