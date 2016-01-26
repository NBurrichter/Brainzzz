using UnityEngine;
using System.Collections;

public class DamageIndicator : MonoBehaviour
{

    public float maxHealth;
    private float health;
    public GameObject renderObject;
    public GameObject hair1;
    public GameObject hair2;
    public Renderer[] rend;

    void Start()
    {
        health = maxHealth;
        rend = renderObject.GetComponentsInChildren<Renderer>();

    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (health > 0)
        {
            health--;
        }
        foreach (Renderer rednder in rend)
        {
            rednder.material.color = new Color((health / maxHealth), (health / maxHealth), (health / maxHealth));
        }

    }

    public float GetHealth()
    {
        return health;
    }
}