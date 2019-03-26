using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMchanges : MonoBehaviour
{

    System.Random rnd;
    public int roomCounter = 0;

    float counter = 0;
    float bgmStarted = 0;
    int loops;
    int[] voicing = new int[1];



    int[] lobbyCues = new int[5] { 0, 24, 48, 72, 96 };
    int[][] lobbyVoicings = {

        new int[] { 0, 2 },
        new int[] { 0, 2, 7 },
        new int[] { 0, 2, 1 },
        new int[] { 0, 2, 7, 1 }
    };

    int[] room1Cues = new int[11] { 0, 6, 24, 36, 48, 60, 72, 96, 120, 132, 144 };
    int[][] room1Voicings = {

        new int[] { 0 },
        new int[] { 0, 1  },
        new int[] { 0, 1, 5, 7 },
        new int[] { 0, 1  },
        new int[] { 0, 1, 5, 7 },
        new int[] { 0, 1, 5 },
        new int[] { 0, 5, 7 },
        new int[] { 0, 1, 5, 7 },
        new int[] { 0, 1, 5 },
        new int[] { 0, 5 }
    };

    int[] room2Cues = new int[7] { 0, 21, 41, 62, 83, 104, 126 };
    int[][] room2Voicings = {

        new int[] { 2 },
        new int[] { 0, 1, 2 },
        new int[] { 1, 2, 6 },
        new int[] { 2, 6, 7  },
        new int[] { 0, 1, 2, 7 },
        new int[] { 0, 1, 2, 6, 7 }
    };

    int[] room3Cues = new int[8] { 0, 17, 51, 68, 85, 102, 119, 154 };
    int[][] room3Voicings = {

        new int[] { 0 },
        new int[] { 0, 1, 3, 7 },
        new int[] { 1, 6 },
        new int[] { 1, 6, 2  },
        new int[] { 1, 2, 3, 7 },
        new int[] { 0, 1, 3 },
        new int[] { 0, 3, 6 }
    };

    // 0 bass, 1 piano, 2 short, 3 short1, 4 med, 5 med1, 6 long, 7 long1

    int[] stressCues = new int[12] { 0, 12, 23, 45, 57, 69, 92, 104, 119, 126, 134, 138 };
    int[][] stressVoicings = {
        new int[] { 2, 5 },
        new int[] { 0, 1, 2, 5 },
        new int[] { 0, 1, 2, 4, 5 },
        new int[] { 0, 1, 2, 5 },
        new int[] { 0, 1, 2, 7 },
        new int[] { 0, 1, 2, 4, 7 },
        new int[] { 0, 1, 2, 7 },
        new int[] { 0, 1, 2, 5, 7 },
        new int[] { 0, 1, 2, 5 },
        new int[] { 0, 2, 5 },
        new int[] { 0, 2 },
    };

    int[] deathCues = new int[] { 0, 6, 12, 23, 46, 69, 91, 104 };
    int[][] deathVoicings = {
        new int[] { 7 },
        new int[] { 0, 7 },
        new int[] { 0, 5, 6, 7 },
        new int[] { 0, 3, 5, 6, 7 },
        new int[] { 0, 2, 3, 5, 6, 7 },
        new int[] { 0, 3, 5, 6, 7 },
        new int[] { 5, 7 },
    };

    int[] bs1Cues = new int[] { 0, 43 };
    int[][] bs1Voicings = {
        new int[] { 0, 4, 6, 7 }
    };

    int[] bs2Cues = new int[] { 0, 12, 36, 59, 84, 96 };
    int[][] bs2Voicings = {
        new int[] { 0, 6, 7 },
        new int[] { 0, 7 },
        new int[] { 0, 6, 7 },
        new int[] { 0, 7 },
        new int[] { 0, 6},
    };


    int[] lastStateChosen = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };


    void Start()
    {
        rnd = new System.Random();
    }

    void Update()
    {
    }

    public void SetRandomVoiceState()
    {

        rnd = new System.Random();

        AkSoundEngine.GetState("BGM", out uint result);
        Debug.Log("this room's state ID is " + result);

        switch (result)
        {
            case 290285391: setRandomVoice(lobbyCues, lobbyVoicings); break;
            case 1359360136: setRandomVoice(room1Cues, room1Voicings); break;
            case 1359360138: setRandomVoice(room2Cues, room2Voicings); break;
            case 1359360140: setRandomVoice(room3Cues, room3Voicings); break;
            case 3840192365: setRandomVoice(stressCues, stressVoicings); break;
            case 378875112: setRandomVoice(deathCues, deathVoicings); break;
            case 748276845: setRandomVoice(bs1Cues, bs1Voicings); break;
            case 748276846: setRandomVoice(bs2Cues, bs2Voicings); break;
        }
    }

    void setRandomVoice(int[] cues, int[][] voicings)
    {

        if (roomCounter == 0)
        {
            counter = Time.time;
        }
        else
        {
            counter = Time.time - (3.2f);
        }

        // Debug.Log("time "+Time.time);
        // Debug.Log("counter "+(counter-bgmStarted));

        while (counter - bgmStarted > cues[cues.Length - 1])
        {
            counter -= cues[cues.Length - 1];
        }

        for (int i = 0; i < cues.Length - 1; i++)
        {

            if (counter - bgmStarted > cues[i] && counter - bgmStarted <= cues[i + 1])
            {
                voicing = voicings[i];
            }
        }

        int randomPick = rnd.Next(0, voicing.Length);
        SetRandomState(voicing[randomPick]);
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

        for (int i = 0; i < lastStateChosen.Length; i++)
        {
            lastStateChosen[i] = state;
        }
    }

    void SetRandomState(int voice)
    {

        // Debug.Log("pick " + voice);
        int state3 = rnd.Next(1, 4);
        int state4 = rnd.Next(1, 5);

        while (state3 == lastStateChosen[voice] || state4 == lastStateChosen[voice])
        {
            state3 = rnd.Next(1, 4);
            state4 = rnd.Next(1, 5);
        }

        switch (voice)
        {
            case 0:
                AkSoundEngine.SetState("guitar", "guit" + state4);
                lastStateChosen[0] = state4;
                break;
            case 1:
                AkSoundEngine.SetState("keys", "state" + state3);
                lastStateChosen[1] = state3;
                break;
            case 2:
                AkSoundEngine.SetState("shortSounds", "state" + state3);
                lastStateChosen[2] = state3;
                break;
            case 3:
                AkSoundEngine.SetState("shortSounds_01", "state" + state3);
                lastStateChosen[3] = state3;
                break;
            case 4:
                AkSoundEngine.SetState("mediumSounds", "state" + state4);
                lastStateChosen[4] = state4;
                break;
            case 5:
                AkSoundEngine.SetState("mediumSounds_01", "state" + state4);
                lastStateChosen[5] = state4;
                break;
            case 6:
                AkSoundEngine.SetState("longSounds", "state" + state3);
                lastStateChosen[6] = state3;
                break;
            case 7:
                AkSoundEngine.SetState("longSounds_01", "state" + state3);
                lastStateChosen[7] = state4;
                break;
        }
    }


    public void SetAllToRandomState()
    {
        for (int i = 0; i < 8; i++)
        {
            SetRandomState(i);
        }

    }

    public void triggerRoomBGM()
    {
        // ------------- trigger next room bgm ---------------//

        if (roomCounter < 3)
        {
            AkSoundEngine.PostEvent("startBGM" + roomCounter, gameObject);
        }

        else if (roomCounter == 3)
        {
            AkSoundEngine.PostEvent("startBSTrack2", gameObject);
        }
        else if (roomCounter == 4)
        {
            AkSoundEngine.PostEvent("startStressTrack", gameObject);
        }

        bgmStarted = Time.time;
        roomCounter += 1;

        // set instrument states
        SetAllStates(roomCounter);
    }
}


