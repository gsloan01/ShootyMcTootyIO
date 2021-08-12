using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    public int damage;
    public GameObject owner;
    public Vector3 velocity;

    float baseScale;

    private Rigidbody2D rb;

    public Vector3 Velocity
    {
        get { return velocity * transform.localScale.x * 30; }
    }


    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale.x;
        float scale = 1.0f;

        //SLOW GROWTH
        scale = Mathf.Sqrt((damage));
        //QUICK GROWTH
        //scale = Mathf.Sqrt(health * .5f) - (Mathf.Sqrt(baseHealth * .5f) - 1);

        transform.localScale = new Vector3(scale, scale, scale) * baseScale;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + Velocity * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (owner == collision.gameObject) return;

        BodyMass mass = collision.GetComponent<BodyMass>();

        if (mass)
        {
            mass.Fragment(damage, owner?.gameObject);
            mass.Health -= damage;
            Destroy(gameObject);
        }
    }

    void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;

        damage = (int) instantiationData[0];
        owner = (PhotonView.Find((int) instantiationData[1])).gameObject;
        velocity = (Vector3) instantiationData[2];

        rb = GetComponent<Rigidbody2D>();
    }

}
