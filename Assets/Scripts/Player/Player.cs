using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("Stats")]
    public float baseSpeed = 2.5f;
    //Speed will be multiplied by the scale
    public float Speed { get { return baseSpeed * transform.localScale.x; } }
    public float damagePerHealth = 2.0f;
    public float shotPercentCost = 0.1f;


    [Header("Components")]
    public GameObject aimingReticle;
    public BodyMass bodyMass;


    private float testTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMove();




        //Size increment test
        /*testTimer += Time.deltaTime;
        if (testTimer > 2.0f)
        {
            testTimer = 0;
            bodyMass.Health += 4;
            Debug.Log(bodyMass.Health);
        }*/
        
    }


    private void ProcessMove()
    {
        float xMov = Input.GetAxis("Horizontal");
        float yMov = Input.GetAxis("Vertical");

        Vector3 velVector = new Vector3(xMov, yMov, 0);
        velVector *= Speed * Time.deltaTime;

        transform.Translate(velVector);
    }
}
