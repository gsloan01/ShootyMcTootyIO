using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    Color color;
    GameObject target;
    Rigidbody2D rb;
    public float speed = 12.0f;
    public float actionTime = 1.0f;
    float timer = 0;
    public void InstantiateFragment(GameObject target, Color newColor)
    {
        this.target = target;
        color = newColor;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = color;
        TrailRenderer trail = GetComponent<TrailRenderer>();
        trail.startColor = color;
        trail.endColor = new Color(color.r, color.g, color.b, 0);
    }

    void Update()
    {
        //Velocity drag
        rb.velocity -= (rb.velocity * .60f) * Time.deltaTime;

        if(timer < actionTime)
        {
            timer += Time.deltaTime;

        }
        if (timer >= actionTime)
        {

            Vector2 direction = target.transform.position - transform.position;
            rb.velocity += direction.normalized * speed * Time.deltaTime;
            Vector2.ClampMagnitude(rb.velocity, 5);
            if (direction.magnitude < (target.transform.localScale.x * .5f))
            {
                target.GetComponent<BodyMass>().Health += 1;
                Destroy(gameObject);
            }
        }
    }
}
