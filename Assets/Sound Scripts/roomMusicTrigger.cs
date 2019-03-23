using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomMusicTrigger : MonoBehaviour
{
    public BGMchanges bgmManager;
    bool checkFirstRoom = true;
    int currentRoom = 0;
    int currentRoomNumber = 0;
    int roomsPerTrack = 2;
    bool roomsBGMstarted = false;

    
    void Start()
    {
        bgmManager = gameObject.GetComponent<BGMchanges>();
        //---------------- start first music track -------------//

        AkSoundEngine.PostEvent("startBGMlobby", gameObject);

    }

  
    void Update()
    {
        /*
        //---------------- listen for room changes , trigger new room bgm -------------//

        checkFirstRoom = gameObject.GetComponent<RoomGenerator>().team1InFirstRoom;
        currentRoom = gameObject.GetComponent<RoomGenerator>().Team1RoomNumber;

        // NOTE TO SELF ASK SCOTT TO MAKE team1InFirstRoom PUBLIC
        if (!checkFirstRoom && !roomsBGMstarted)
        {
            roomsBGMstarted = true;
            bgmManager.triggerRoomBGM();
        }

        if (roomsBGMstarted && currentRoom != currentRoomNumber )
        {
            
            currentRoomNumber = currentRoom;

            if (currentRoomNumber/2 % roomsPerTrack == 0)
            {
                bgmManager.triggerRoomBGM();
            }
            else
            {
                bgmManager.SetAllToRandomState();
            }
        }
        */
    }

    
}
