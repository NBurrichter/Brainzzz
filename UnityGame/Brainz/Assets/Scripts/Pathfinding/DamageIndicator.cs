﻿using UnityEngine;
using System.Collections;

public class DamageIndicator : MonoBehaviour
{

    public float maxHealth;
    private float health;
    private Renderer rend;

    void Start()
    {
        health = maxHealth;
        rend = GetComponent<Renderer>();
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        health--;
        rend.material.color = new Color((health/maxHealth),(health / maxHealth),(health / maxHealth));
    }
}