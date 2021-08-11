using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworking : MonoBehaviour
{
    public MonoBehaviour[] scriptsToIgnore;
    public GameObject[] objectsToIgnore;

    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            foreach(MonoBehaviour script in scriptsToIgnore)
            {
                script.enabled = false;
            }
            foreach(GameObject obj in objectsToIgnore)
            {
                obj?.SetActive(false);
            }
        }
    }
}
