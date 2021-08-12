using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance { get { return instance; } }
    static GameManager instance;

    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;

    void Start()
    {
        instance = this;

        if (PlayerManager.LocalPlayerInstance == null)
        {
            Vector2 playerSpawnPosition = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
            //playerSpawnPosition = Vector2.zero;
            PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPosition, Quaternion.identity);
        }
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void LoadArena()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }

    //public override void OnPlayerEnteredRoom(Player other)
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {

    //        if (PhotonNetwork.CurrentRoom.PlayerCount >= 1)
    //        {
    //            LoadArena();
    //        }
    //    }
    //}

    //public override void OnPlayerLeftRoom(Player other)
    //{

    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        LoadArena();
    //    }
    //}
}
