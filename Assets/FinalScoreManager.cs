using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FinalScoreManager : MonoBehaviour
{
    public int Team1Gems;
    public int Team2Gems;

    public Text WinnerText;
    public Text ImportantText;
    public Transform GemCanvas;

    public bool showscores = true;

    public GameObject GemImagePrefab;
    public Transform[] GemSpawnPointsTeam1;
    public Transform[] GemSpawnPointsTeam2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowScores());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowScores()
    {
        for(int i = 0; i < Team1Gems; i++)
        {
            GameObject Gem = Instantiate(GemImagePrefab, GemSpawnPointsTeam1[i].transform.position, Quaternion.identity);
            Gem.transform.SetParent(GemCanvas);
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < Team2Gems; i++)
        {
            GameObject Gem = Instantiate(GemImagePrefab, GemSpawnPointsTeam2[i].transform.position, Quaternion.identity);
            Gem.transform.SetParent(GemCanvas);
            yield return new WaitForSeconds(0.1f);
        }

        if(Team1Gems > Team2Gems)
        {
            WinnerText.gameObject.SetActive(true);
            ImportantText.gameObject.SetActive(true);
            WinnerText.text = "TEAM 1 WINS!";

        }else if (Team1Gems < Team2Gems)
        {
            WinnerText.gameObject.SetActive(true);

            ImportantText.gameObject.SetActive(true);
            WinnerText.text = "TEAM 2 WINS!";
        }
        else
        {
            WinnerText.gameObject.SetActive(true);

            ImportantText.gameObject.SetActive(true);
            WinnerText.text = "WELL, IT'S A TIE. MIGHT AS WELL TRY AGAIN?";
        }

    }
}
