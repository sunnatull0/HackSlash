using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public static PlayerHealth Instance { get; private set; }

    [SerializeField] private float Health = 3f;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    
    
    public void CheckHealth()
    {
        if (Health <= 0f)
        {
            Die();
        }
    }

    
    private void Die()
    {
        Debug.Log("Died!");
    }
    
}