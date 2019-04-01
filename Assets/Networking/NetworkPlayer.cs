using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviourPunCallbacks
{

    public RopeGenerator rope;
    public int Gemzzzzzz = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            Destroy(this.gameObject.transform.GetChild(0).gameObject);
            Destroy(this.gameObject.transform.GetChild(1).gameObject);
            //Destroy(this.gameObject.transform.GetChild(2).gameObject);
            Destroy(this.gameObject.transform.GetChild(3).gameObject);
            Destroy(this.gameObject.transform.GetChild(4).gameObject.transform.GetChild(0).gameObject);
            //Destroy(this.gameObject.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject);
            Destroy(this.gameObject.transform.GetChild(4).gameObject.transform.GetChild(4).gameObject);
            Destroy(this.gameObject.transform.GetChild(4).gameObject.transform.GetChild(5).gameObject);
            Destroy(this.gameObject.transform.GetChild(4).gameObject.transform.GetChild(6).gameObject);
            Destroy(this.gameObject.transform.GetChild(5).gameObject);
            Destroy(this.gameObject.transform.GetChild(6).gameObject);
            Destroy(this.gameObject.transform.GetChild(7).gameObject.transform.GetChild(0).gameObject);
            //Destroy(this.gameObject.transform.GetChild(7).gameObject.transform.GetChild(1).gameObject);
            Destroy(this.gameObject.transform.GetChild(7).gameObject.transform.GetChild(4).gameObject);
            Destroy(this.gameObject.transform.GetChild(7).gameObject.transform.GetChild(5).gameObject);
            Destroy(this.gameObject.transform.GetChild(8).gameObject);


            StartCoroutine(Wait());

        }
    }

    public void addGemz()
    {
        photonView.RPC("SetGemz", RpcTarget.All);
    }

    [PunRPC]
    void SetGemz()
    {
        Gemzzzzzz++;
    }


    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        foreach (GameObject joint in rope.GetComponent<RopeGenerator>().ropeJoints)
        {
            joint.GetComponent<CapsuleCollider>().enabled = false;
            //joint.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}

