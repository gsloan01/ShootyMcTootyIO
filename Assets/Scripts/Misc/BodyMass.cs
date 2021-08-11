using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
[RequireComponent(typeof(CircleCollider2D))]
public class BodyMass : MonoBehaviour
{
    //Health Values
    public float baseHealth = 20;
    private float health;
    public float Health
    {
        get { return health; }
        set
        {
            health = value;
            UpdateScale();
        }
    }
    public float HealthRatio { get { return health / baseHealth; } }

    //Color
    public Color baseColor;

    //The scale the object takes when health = 0
    public float minScale = .3f;

    public GameObject fragment;

    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth;
        GetComponent<SpriteRenderer>().color = baseColor;
    }

    private void Update()
    {
        //Maybe Die before health hits 0??
        if (health <= 0)
        {

        }
    }

    private void UpdateScale()
    {
        float scale = 1.0f;

        if (health > baseHealth)
        {
            //SLOW GROWTH
            scale = Mathf.Sqrt((health / baseHealth));
            //QUICK GROWTH
            //scale = Mathf.Sqrt(health * .5f) - (Mathf.Sqrt(baseHealth * .5f) - 1);
        } else
        {
            scale = Mathf.Lerp(minScale, 1.0f, health / baseHealth);
        }

        transform.localScale = new Vector3(scale, scale, scale);
    }


    public void Fragment(float damage, GameObject target)
    {
        for (int i = 0; i < damage; i++)
        {
            GameObject frag = Instantiate(fragment, transform.position, Quaternion.identity);
            frag.GetComponent<Fragment>().SetTarget(target);
            frag.GetComponent<Rigidbody2D>().AddForceAtPosition(Vector2.one, transform.position);
        }
        //TODO Create a number of health fragments based on the damage dealt, and the target they will fly to
    }
}
