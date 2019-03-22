using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMchanges : MonoBehaviour
{

    System.Random rnd;
    public int roomCounter = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRandomVoiceState()
    {

        rnd = new System.Random();

        AkSoundEngine.GetState("BGM", out uint result);
        //Debug.Log("this room's state ID is "+result);

        switch (result)
        {
            case 1359360136: SetRandomRoom1Voice(); break;
            case 1359360138: SetRandomRoom2Voice(); break;
            case 1359360140: SetRandomRoom3Voice(); break;
            case 3840192365: SetRandomStressTrackVoice(); break;

        }
    }

    void SetRandomStressTrackVoice()
    {
        int voice = rnd.Next(1, 7);
        int state3 = rnd.Next(1, 4);
        int state4 = rnd.Next(1, 5);

        switch (voice)
        {
            case 1:
                AkSoundEngine.SetState("guitar", "guit" + state4);
                break;
            case 2:
                AkSoundEngine.SetState("keys", "state" + state3);
                break;
            case 3:
                AkSoundEngine.SetState("mediumSounds", "state" + state4);
                break;
            case 4:
                AkSoundEngine.SetState("shortSounds", "state" + state3);
                break;
            case 5:
                AkSoundEngine.SetState("mediumSounds_01", "state" + state4);
                break;
            case 6:
                AkSoundEngine.SetState("longSounds_01", "state" + state3);
                break;
        }
    }

    void SetRandomRoom3Voice()
    {
        int voice = rnd.Next(1, 7);
        int state3 = rnd.Next(1, 4);
        int state4 = rnd.Next(1, 5);

        switch (voice)
        {
            case 1:
                AkSoundEngine.SetState("guitar", "guit" + state4);
                break;
            case 2:
                AkSoundEngine.SetState("keys", "state" + state3);
                break;
            case 3:
                AkSoundEngine.SetState("longSounds", "state" + state3);
                break;
            case 4:
                AkSoundEngine.SetState("shortSounds", "state" + state3);
                break;
            case 5:
                AkSoundEngine.SetState("longSounds_01", "state" + state3);
                break;
            case 6:
                AkSoundEngine.SetState("shortSounds_01", "state" + state3);
                break;
        }
    }

    void SetRandomRoom2Voice()
    {
        int voice = rnd.Next(1, 6);
        int state3 = rnd.Next(1, 4);
        int state4 = rnd.Next(1, 5);

        switch (voice)
        {
            case 1:
                AkSoundEngine.SetState("guitar", "guit" + state4);
                break;
            case 2:
                AkSoundEngine.SetState("keys", "state" + state3);
                break;
            case 3:
                AkSoundEngine.SetState("longSounds", "state" + state3);
                break;
            case 4:
                AkSoundEngine.SetState("shortSounds", "state" + state3);
                break;
            case 5:
                AkSoundEngine.SetState("longSounds_01", "state" + state3);
                break;
        }
    }

    void SetRandomRoom1Voice()
    {
        int voice = rnd.Next(1, 5);
        int state3 = rnd.Next(1, 4);
        int state4 = rnd.Next(1, 5);

        switch (voice)
        {
            case 1:
                AkSoundEngine.SetState("guitar", "guit" + state4);
                break;
            case 2:
                AkSoundEngine.SetState("keys", "state" + state3);
                break;
            case 3:
                AkSoundEngine.SetState("longSounds_01", "state" + state3);
                break;
            case 4:
                AkSoundEngine.SetState("mediumSounds_01", "state" + state4);
                break;
        }
    }

    public void SetAllStates(int state)
    {

        AkSoundEngine.SetState("guitar", "guit" + state);
        AkSoundEngine.SetState("keys", "state" + state);
        AkSoundEngine.SetState("longSounds", "state" + state);
        AkSoundEngine.SetState("longSounds_01", "state" + state);
        AkSoundEngine.SetState("mediumSounds", "state" + state);
        AkSoundEngine.SetState("mediumSounds_01", "state" + state);
        AkSoundEngine.SetState("shortSounds", "state" + state);
        AkSoundEngine.SetState("shortSounds_01", "state" + state);
    }

    void SetRandomState(int voice)
    {

        int state3 = rnd.Next(1, 4);
        int state4 = rnd.Next(1, 5);

        switch (voice)
        {
            case 1:
                AkSoundEngine.SetState("guitar", "guit" + state4);
                break;
            case 2:
                AkSoundEngine.SetState("keys", "state" + state3);
                break;
            case 3:
                AkSoundEngine.SetState("longSounds", "state" + state3);
                break;
            case 4:
                AkSoundEngine.SetState("longSounds_01", "state" + state3);
                break;
            case 5:
                AkSoundEngine.SetState("mediumSounds", "state" + state4);
                break;
            case 6:
                AkSoundEngine.SetState("mediumSounds_01", "state" + state4);
                break;
            case 7:
                AkSoundEngine.SetState("shortSounds", "state" + state3);
                break;
            case 8:
                AkSoundEngine.SetState("shortSounds_01", "state" + state3);
                break;
        }
    }


public void SetAllToRandomState()
{
        rnd = new System.Random();
        int state3 = rnd.Next(1, 4);
        int state4 = rnd.Next(1, 5);

    
            AkSoundEngine.SetState("guitar", "guit" + state4);
            AkSoundEngine.SetState("keys", "state" + state3);
        state3 = rnd.Next(1, 4);
        AkSoundEngine.SetState("longSounds", "state" + state3);
        state3 = rnd.Next(1, 4);
        AkSoundEngine.SetState("longSounds_01", "state" + state3);
        state4 = rnd.Next(1, 5);
        AkSoundEngine.SetState("mediumSounds", "state" + state4);
        state4 = rnd.Next(1, 5);
        AkSoundEngine.SetState("mediumSounds_01", "state" + state4);
        state3 = rnd.Next(1, 4);
        AkSoundEngine.SetState("shortSounds", "state" + state3);
        state3 = rnd.Next(1, 4);
        AkSoundEngine.SetState("shortSounds_01", "state" + state3);
         
}

    public void triggerRoomBGM()
    {
        // ------------- trigger next room bgm ---------------//

        AkSoundEngine.PostEvent("startBGM" + roomCounter, gameObject);
        roomCounter += 1;

        // set instrument states
        SetAllStates(roomCounter);
    }
}
