using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public class WeaponInfo
    {
        public float damagePerHealth = 2.0f;
        public float shotPercentCost = 0.1f;

        public float fireRate = 1f;
        public float fireNum = 1;

        public WeaponInfo()
        {

        }

        public WeaponInfo(float newDamage, float newShotPercent, float newFireRate, float newFireNum)
        {
            damagePerHealth = newDamage;
            shotPercentCost = newShotPercent;
            fireRate = newFireRate;
            fireNum = newFireNum;
        }
    }


    [Header("Stats")]
    public float baseSpeed = 2.5f;
    //Speed will be multiplied by the scale
    public float Speed { get { return baseSpeed * transform.localScale.x; } }
    public WeaponInfo weapon = new WeaponInfo();
    private float weaponRate = 0;
    

    private float aimAngle;

    [Header("Components")]
    public GameObject aimingReticle;
    public BodyMass bodyMass;
    public GameObject projectile;

    public GameObject healthFragment;


    private float testTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMove();
        ProcessAim();
        ProcessShoot();

        //Size increment test
        testTimer += Time.deltaTime;
        if (testTimer > 2.0f)
        {
            testTimer = 0;
            bodyMass.Health += 4;
            Debug.Log(bodyMass.Health);
        }
        
    }


    #region Control Functions
    private void ProcessMove()
    {
        float xMov = Input.GetAxis("Horizontal");
        float yMov = Input.GetAxis("Vertical");

        Vector3 velVector = new Vector3(xMov, yMov, 0);
        velVector *= Speed * Time.deltaTime;

        transform.Translate(velVector);
    }

    private void ProcessAim()
    {
        Vector3 mouseDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        mouseDir = mouseDir.normalized;

        aimAngle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;

        aimingReticle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, aimAngle));
    }

    private void ProcessShoot()
    {
        weaponRate += Time.deltaTime;
        if (Input.GetButton("Fire1") && weaponRate > weapon.fireRate)
        {
            weaponRate = 0;
            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

            int damage = TakeShotDamage();

            //Base speed
            float speed = 3;
            float xSpeed = Mathf.Cos(Mathf.Deg2Rad * aimAngle) * speed;
            float ySpeed = Mathf.Sin(Mathf.Deg2Rad * aimAngle) * speed;
            Vector3 shotVelocity = new Vector3(xSpeed, ySpeed, 0);

            newProjectile.GetComponent<Projectile>().InstantiateProjectile(damage, this, shotVelocity);
        }
    }


    private int TakeShotDamage()
    {
        int damage = (int) Mathf.Ceil(bodyMass.Health * weapon.shotPercentCost * weapon.damagePerHealth);

        bodyMass.Health -= Mathf.Ceil(weapon.shotPercentCost * bodyMass.Health);

        return damage;
    }

    #endregion
}
