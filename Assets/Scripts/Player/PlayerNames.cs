using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerNames : MonoBehaviourPunCallbacks, IPunObservable
{
    public bool DisableOnOwnObjects;

    public TMP_Text nameUI;

    void Update()
    {
        bool showInfo = !this.DisableOnOwnObjects || this.photonView.isMine;

        if (!showInfo)
        {
            return;
        }
        Photon.Realtime.Player owner = this.photonView.Owner;
        if (owner != null)
        {
            nameUI.text = (string.IsNullOrEmpty(owner.NickName)) ? "player" + owner.UserId : owner.NickName;
        }
        else if (this.photonView.IsRoomView)
        {
            nameUI.text = "scn";
        }
        else
        {
            nameUI.text = "n/a";
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}