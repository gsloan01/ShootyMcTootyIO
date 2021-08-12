using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderPoint : MonoBehaviour
{
    SpriteRenderer arrowRender;

    private void Start()
    {
        arrowRender = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = BodyMass.biggestPlayer.transform.position - transform.position;

        float scale = Mathf.Lerp(0, 1f, direction.magnitude / 5.0f);
        transform.localScale = new Vector3(scale, scale, scale);

        direction = direction.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        arrowRender.color = BodyMass.biggestPlayer.baseColor;
    }
}
