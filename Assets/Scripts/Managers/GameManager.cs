﻿using System.Collections;
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

    private int score;
    
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
    
    private void Start()
    {
        var turret = FindObjectOfType<MainGun>();
        for (int i = 0; i < turret.health; i++)
        {
           Instantiate(healthIconPrefab, healthBar.transform);
        }
    }
}
