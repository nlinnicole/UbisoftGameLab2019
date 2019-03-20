using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


namespace Concordia1.Gamelab
{
    public class GameManager : MonoBehaviourPunCallbacks
    {

        bool ready = false;
        public GameObject playerPrefab;
 

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
                    GameObject sam = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(50f, 5f, 10f), Quaternion.identity, 0);
                    sam.GetComponentInChildren<Camera>().enabled = true;
            }
        }
    }
}