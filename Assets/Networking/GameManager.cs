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
            if (PhotonNetwork.IsMasterClient)
            {

            }
        }


        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


            if (PhotonNetwork.IsMasterClient)
            {
                LoadMenu();
            }
        }




        #region Public Methods


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }


        #endregion

        #region Private Methods


        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Lobby");
        }

        void LoadMenu()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load menu but we are not the master Client");
            }
            Debug.LogFormat("PhotonNetwork : Loading Menu", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Launcher");
        }


        #endregion
        void Start()
        {
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    GameObject sam = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(50f, 5f, 10f), Quaternion.identity, 0);
                    sam.GetComponentInChildren<Camera>().enabled = true;
            }
        }

        void Update()
        {
            if (!ready)
            {
                //if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
                //{
                 //   ready = true;
                 //   Debug.Log("ready to go");
                //}
            }

        }
    }
}