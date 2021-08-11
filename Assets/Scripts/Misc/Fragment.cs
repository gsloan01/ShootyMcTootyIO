using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    GameObject target;
    Rigidbody2D rb;
    public float speed = 2.0f;
    public float actionTime = 1.0f;
    float timer = 0;
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(timer < actionTime)
        {
            timer += Time.deltaTime;

        }
        if (timer >= actionTime)
        {
            Vector2 direction = target.transform.position - transform.position;
            rb.velocity += direction.normalized * speed * Time.deltaTime;
            Vector2.ClampMagnitude(rb.velocity, 5);
            if (direction.magnitude < .1f)
            {
                target.GetComponent<BodyMass>().Health += 1;
                Destroy(gameObject);
            }
        }
    }
}
