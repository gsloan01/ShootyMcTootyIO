using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public Player owner;
    public Vector3 velocity;

    public Vector3 Velocity
    {
        get { return velocity * transform.parent.localScale.x; }
    }


    // Start is called before the first frame update
    void Start()
    {
        float scale = 1.0f;

        //SLOW GROWTH
        scale = Mathf.Sqrt((damage));
        //QUICK GROWTH
        //scale = Mathf.Sqrt(health * .5f) - (Mathf.Sqrt(baseHealth * .5f) - 1);

        transform.parent.localScale = new Vector3(scale, scale, scale);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Velocity * Time.deltaTime);
    }


    public void InstantiateProjectile(int newDamage, Player newOwner, Vector3 newVelocity)
    {
        damage = newDamage;
        owner = newOwner;
        velocity = newVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Not Player");
        Player player = collision.collider.GetComponent<Player>();
        if (player)
        {
            if (player == owner) return;
        }



        BodyMass mass = collision.collider.GetComponent<BodyMass>();

        if (mass)
        {
            mass.Fragment(damage, owner.gameObject);
            mass.Health -= damage;
            Destroy(gameObject);
        }
    }

}
