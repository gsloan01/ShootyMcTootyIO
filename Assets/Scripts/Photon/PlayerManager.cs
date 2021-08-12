using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    public TMP_Text nameUI;


    void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }
        nameUI.text = PhotonNetwork.NickName;

        DontDestroyOnLoad(this.gameObject);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //stream.SendNext(health);
        }
        else
        {
            //this.health = (float)stream.ReceiveNext();
        }
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
    }

}