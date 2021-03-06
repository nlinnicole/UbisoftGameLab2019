﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


namespace Concordia1.Gamelab
{
    public class GameManager : MonoBehaviourPunCallbacks
    {

        public GameObject startPos;
        bool ready = false;
        public GameObject playerPrefab;
        public GameObject playerPrefab2;


        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }


        public override void OnPlayerEnteredRoom(Player other)
        {
           
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }


        void Start()
        {
            if (playerPrefab != null)
            {
                if (PhotonNetwork.IsMasterClient)
                    PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(22f, 5f, 10f), Quaternion.identity, 0);
                else
                    PhotonNetwork.Instantiate(this.playerPrefab2.name, new Vector3(-22f, 5f, 10f), Quaternion.identity, 0);
            }

        }
    }
}