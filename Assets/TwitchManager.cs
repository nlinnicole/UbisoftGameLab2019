using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.IO;
using UnityEngine.UI;

public class TwitchManager : MonoBehaviour
{
    bool team1twitchmode = false;
    bool team2twitchmode = false;

    public bool chatmode = false;

    public int votecounter = 5;

    public string currentmsg = "";

    public bool eventrunning = false;


    public Slider VoteSlider;


    public GameObject ChatMessage;
    public GameObject Canvas, Team1Canvas, Team2Canvas;
    
    public Transform[] ChatMessageSpawn;

    int spawncounter;

    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    public int buffindex = 0;



    public string username, password, channelName; //Get the password from https://twitchapps.com/tmi

    public Text chatBox;
    public Text buffTextBox;
    public Text buffTextTimer;

    public int speed;
    public float eventtimer = 20f;


    void Start()
    {
        Connect();
        currentmsg = "";

        eventrunning = false;
        InvokeRepeating("RunningEvent", 20, 60);
    }

    void Update()
    {
        buffTextBox.text = currentmsg;

        if (!eventrunning)
        {
            buffTextTimer.text = "";

        }else if (eventrunning)
        {
            buffTextTimer.text = Math.Round(eventtimer, 0).ToString();
        }
        VoteSlider.value = votecounter;

        if (!twitchClient.Connected)
        {
            Connect();
        }

        ReadChat();

        if (eventrunning)
        {
            eventtimer -= Time.deltaTime;
            if(eventtimer < 0)
            {
                ResetEvent();
                Debug.Log("Event reset");
            }

        }

    }

    private void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        writer.WriteLine("PASS " + password);
        writer.WriteLine("NICK " + username);
        writer.WriteLine("USER " + username + " 8 * :" + username);
        writer.WriteLine("JOIN #" + channelName);
        writer.Flush();
    }

    void RunningEvent()
    {
        team1twitchmode = false;
        team2twitchmode = false;
        chatmode = false;
        votecounter = 5;
        eventrunning = true;

        if(buffindex == 0)
        {
            currentmsg = "DOUBLE VALUE GEMS";
            //Increment the buff here
            buffindex++;
        }else if
        (buffindex == 1)
        {
            currentmsg = "TWITCH CHAT MODE?";
            buffindex++;
        }else if(buffindex == 2)
        {
            currentmsg = "THIRD MODE";
            buffindex++;
        }
    }

    void ResetEvent()
    {
        //buff indexes are scuffed
        if(votecounter < 5)
        {
            //Team1
            if (buffindex == 1)
            {
                currentmsg = "Team 1 Know how to invest their gems";
                buffTextTimer.text = ""; 
            }
            if(buffindex == 2)
            {
                chatmode = true;
                currentmsg = "Team 1 get to know how twitch chat feels about them";
                team1twitchmode = true;
                
            }
            if (buffindex == 3)
            {
                chatmode = true;
            }

        }
        else if(votecounter > 5)
        {
            //Team2 Wins
            if (buffindex == 1)
            {
                //Gems worth * 2
                currentmsg = "Team 2 Know how to invest their gems";
                buffTextTimer.text = "";
            }
            if (buffindex == 2)
            {
                chatmode = true;
                currentmsg = "Team 2 get to know how twitch chat feels about them";
                team2twitchmode = true;
                
            }
            if (buffindex == 3)
            {
                
            }

        }
        else
        {
            currentmsg = "Looks like no one won, guess neither of you has that many fans eh...";
            chatmode = true;
        }


        eventtimer = 20f;
        eventrunning = false;
    }
    



    private void ReadChat()
    {
        if (twitchClient.Available > 0)
        {
            var message = reader.ReadLine(); //Read in the current message

            if (message.Contains("PRIVMSG"))
            {
                //Get the users name by splitting it from the string
                var splitPoint = message.IndexOf("!", 1);
                var chatName = message.Substring(0, splitPoint);
                chatName = chatName.Substring(1);

                //Get the users message by splitting it from the string
                splitPoint = message.IndexOf(":", 1);
                message = message.Substring(splitPoint + 1);
                //print(String.Format("{0}: {1}", chatName, message));

                ChatMessage.GetComponent<Text>().text = String.Format("{0}: {1}", chatName, message);
                if (chatmode)
                {
                    if (team1twitchmode)
                    {
                        GameObject chatmessage = Instantiate(ChatMessage, ChatMessageSpawn[spawncounter].position, Quaternion.identity);
                        chatmessage.transform.parent = Team1Canvas.transform;
                        spawncounter++;
                        if (spawncounter > 2)
                        {
                            spawncounter = 0;
                        }
                        chatBox.text = " " + String.Format("{0}: {1}", chatName, message) + chatBox.text;
                    }else if (team2twitchmode)
                    {
                        GameObject chatmessage = Instantiate(ChatMessage, ChatMessageSpawn[spawncounter].position, Quaternion.identity);
                        chatmessage.transform.parent = Team2Canvas.transform;
                        spawncounter++;
                        if (spawncounter > 2)
                        {
                            spawncounter = 0;
                        }
                        chatBox.text = " " + String.Format("{0}: {1}", chatName, message) + chatBox.text;
                    }else if(!team2twitchmode && !team1twitchmode)
                    {
                        GameObject chatmessage = Instantiate(ChatMessage, ChatMessageSpawn[spawncounter].position, Quaternion.identity);
                        chatmessage.transform.parent = Canvas.transform;
                        spawncounter++;
                        if (spawncounter > 2)
                        {
                            spawncounter = 0;
                        }
                        chatBox.text = " " + String.Format("{0}: {1}", chatName, message) + chatBox.text;
                    }

                    
                }
                

                //Run the instructions to control the game!
                GameInputs(message);
            }
        }
    }

    
    private void GameInputs(string ChatInputs)
    {
        if (eventrunning)
        {
            if (ChatInputs.ToLower() == "red")
            {
                votecounter++;
            }
            if (ChatInputs.ToLower() == "blue")
            {
                votecounter--;
            }
        }
        
    }
    
}

public class Buff
{
    public string Message { get; set; }
    public Buff(string message)
    {
        Message = Message;
    }
}


