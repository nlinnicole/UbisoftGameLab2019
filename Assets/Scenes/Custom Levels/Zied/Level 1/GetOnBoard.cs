using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnBoard : MonoBehaviour
{
    public GameObject MasterController;
    public GameObject Vehicle;
    public GameObject Player;
    public GameObject PlayerBackup;
    private bool inVehicle = false;
    Vehicle vehicleScript;
    GameObject guiObj;

    // Start is called before the first frame update
    void Start()
    {
        vehicleScript = GetComponent<Vehicle>();
        vehicleScript.enabled = false;
        PlayerBackup.SetActive(false);
        guiObj = GameObject.Find("PressE");
        guiObj.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && inVehicle == false && Input.GetKey(KeyCode.E))
        {
            guiObj.SetActive(true);
        }
        if(other.gameObject.tag == "Player" && inVehicle == false && Input.GetKey(KeyCode.E))
        {
            guiObj.SetActive(false);
            PlayerBackup.SetActive(true);
            Player.SetActive(false);
            Player.transform.parent = Vehicle.transform;
            vehicleScript.enabled = true;
            inVehicle = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            guiObj.SetActive(false);
        }
    }

    private void Update()
    {
        if(inVehicle == true && Input.GetKey(KeyCode.F))
        {
            Player.SetActive(true);
            Player.transform.SetParent(MasterController.transform);
            PlayerBackup.SetActive(false);
            vehicleScript.enabled = false;
            inVehicle = false;
        }
    }
}
