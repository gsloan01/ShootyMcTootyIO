using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainPanel;

    //public GameObject customizationPanel;
    public ParticleSystem mouseFX;

    void Update()
    {
        mouseFX.transform.position = new Vector3(Camera.main.ScreenToWorldPoint( Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0); 
        if(Input.GetMouseButton(0) && !mouseFX.isEmitting)
        {
            mouseFX.Play();
        }
        else if (mouseFX.isEmitting && !Input.GetMouseButton(0))
        {
            mouseFX.Stop();
        }
        
    }
    public void OnPlayClick()
    {
        Debug.Log("Button works");
    }
}
