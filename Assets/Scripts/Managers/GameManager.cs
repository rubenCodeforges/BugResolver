using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject healthIconPrefab;
    public GameObject scoreText;
    public GameObject gameOverText;
    public bool gameOver;
    public GameObject[] powerUps;
    
    private int score;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.GetComponent<TextMeshProUGUI>().SetText("Score: " + score);
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
        gameOver = true;
    }

    public void SpawnPowerUp(Transform location, int index)
    {
        var powerUp = Instantiate(powerUps[index], location);
        powerUp.transform.position = location.position;
    }
    
    private void Start()
    {
        var turret = FindObjectOfType<MainGun>();
        for (int i = 0; i < turret.health; i++)
        {
           Instantiate(healthIconPrefab, healthBar.transform);
        }
    }
}
