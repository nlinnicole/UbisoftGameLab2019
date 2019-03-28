using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomMusicTrigger : MonoBehaviour
{
    public BGMchanges bgmManager;
    bool checkFirstRoom = true;
    int checkRoom;
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

        //---------------- listen for room changes , trigger new room bgm -------------//

        checkRoom = gameObject.GetComponent<RoomGenerator>().team2CurrentRoom;

        if (checkRoom == 0 && !roomsBGMstarted)
        {
            roomsBGMstarted = true;
            bgmManager.triggerRoomBGM();
        }

        if (roomsBGMstarted && checkRoom != currentRoomNumber)
        {

            currentRoomNumber = checkRoom;

            if (currentRoomNumber / 2 % roomsPerTrack == 0)
            {
                bgmManager.triggerRoomBGM();
            }
            else
            {
                bgmManager.SetAllToRandomState();
            }
        }

    }


}
