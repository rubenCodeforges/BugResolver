using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject healthIconPrefab;
    
    private void Start()
    {
        var turret = FindObjectOfType<MainGun>();
        for (int i = 0; i < turret.health; i++)
        {
           Instantiate(healthIconPrefab, healthBar.transform);
        }
    }
}
