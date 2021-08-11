using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            float health = collision.gameObject.GetComponent<BodyMass>().baseHealth;
            if (health-5 <= 0)
            {
                Destroy(collision.gameObject);
                Debug.Log("ded");
            }
            else
            {
                health -= 5;
                collision.gameObject.GetComponent<BodyMass>().baseHealth = health;
            }
            Debug.Log("test");


        }
    }
}
