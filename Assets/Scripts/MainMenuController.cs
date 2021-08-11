using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainPanel;
    public List<GameObject> mainMenuObjects;
    public TMP_Text loadingTxt;
    //public List<GameObject> customizationPanel;
    public ParticleSystem mouseFX;
    float loadTimer;
    int totaldots = 1;
    void Update()
    {
        loadTimer += Time.deltaTime;
        if(loadTimer >= 1.25f)
        {
            totaldots++;
            if(totaldots == 4)
            {
                totaldots = 1;
            }
            loadTimer = 0;
        }
        MouseFX();
        string loadtxt = "loading arena";
        for (int i = 0; i < totaldots; i++)
        {
            loadtxt += ".";
        }
        loadingTxt.text = loadtxt;
    }
    public void OnPlayClick()
    {
        Debug.Log("Button works");
        foreach(GameObject g in mainMenuObjects)
        {
            g.SetActive(false);
        }
        loadingTxt.gameObject.SetActive(true);
    }
    void MouseFX()
    {
        mouseFX.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        if (Input.GetMouseButton(0) && !mouseFX.isEmitting)
        {
            mouseFX.Play();
        }
        else if (mouseFX.isEmitting && !Input.GetMouseButton(0))
        {
            mouseFX.Stop();
        }
    }

}
