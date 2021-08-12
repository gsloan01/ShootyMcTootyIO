using Photon.Pun;
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
            UpdateTopMass();
        }
    }
    public float HealthRatio { get { return health / baseHealth; } }

    //Color
    public Color baseColor;

    //The scale the object takes when health = 0
    public float minScale = .3f;
    public GameObject fragment;

    public static BodyMass biggestPlayer;
    private void UpdateTopMass()
    {
        if (!biggestPlayer)
        {
            biggestPlayer = this;
            return;
        }

        BodyMass bigMass = biggestPlayer.GetComponent<BodyMass>();
        if (bigMass.Health < this.Health)
        {
            biggestPlayer = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth;
        GetComponent<SpriteRenderer>().color = baseColor;
        UpdateTopMass();
    }

    private void Update()
    {
        Debug.Log("Mass Name: " + GetComponent<PhotonView>().Owner.UserId);
    }
    void Die()
    {
        GameManager.Instance.LeaveRoom();
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
            frag.GetComponent<Fragment>().InstantiateFragment(target, baseColor);
            //frag.GetComponent<Rigidbody2D>().AddForceAtPosition(Vector2.one, transform.position);

            float xRand = Random.Range(-.99f, .99f);
            float yRand = Random.Range(-.99f, .99f);
            Vector2 velocity = new Vector2(xRand, yRand) * 3.0f;

            frag.GetComponent<Rigidbody2D>().velocity = velocity;
        }
        if(health <= 0)
        {

            GameManager.Instance.LeaveRoom();
            int frags = Random.Range(5, 10);
            for (int i = 0; i < frags; i++)
            {
                GameObject frag = Instantiate(fragment, transform.position, Quaternion.identity);
                frag.GetComponent<Fragment>().InstantiateFragment(target, baseColor);
                //frag.GetComponent<Rigidbody2D>().AddForceAtPosition(Vector2.one, transform.position);

                float xRand = Random.Range(-.99f, .99f);
                float yRand = Random.Range(-.99f, .99f);
                Vector2 velocity = new Vector2(xRand, yRand) * 3.0f;

                frag.GetComponent<Rigidbody2D>().velocity = velocity;
            }
        }
    }
}
