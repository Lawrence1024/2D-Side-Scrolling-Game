using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProtalScript : MonoBehaviour
{
    public GameObject mainCamera;
    private AllObjects allObject;
    private LevelLoader levelLoader;
    private Animator transition;
    private GameObject gCanvas;
    private GameObject endLevelCanvas;
    private GameObject elPoints;
    private LoadQuestions lQ;
    private GameObject player;
    private GameObject player2;
    private string chosenPlayer;
    private Vector3 playerPosition;
    private GameObject portal;
    private Vector3 portalPosition;

    // Start is called before the first frame update
    void Start()
    {
        allObject = mainCamera.GetComponent<AllObjects>();
        levelLoader = allObject.levelLoader.GetComponent<LevelLoader>();
        transition = allObject.levelLoader.GetComponent<Animator>();
        gCanvas = allObject.gCanvas;
        endLevelCanvas = allObject.EndLevelCanvas;
        elPoints = allObject.endLevelPoints;
        lQ = allObject.qCanvas.GetComponentInChildren<LoadQuestions>();
        player = allObject.playerGirl;
        player2 = allObject.playerBoy;
        chosenPlayer = allObject.chosenPlayer;
        playerPosition = allObject.playerPosition;
        portal = allObject.portal;
        portalPosition = portal.GetComponent<RectTransform>().localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadEndLevelPage()
    {
        StartCoroutine(crossFade());
        elPoints.GetComponent<TMP_Text>().text = ""+lQ.score;
        if (chosenPlayer.Equals("boy"))
        {
            player2.GetComponent<RectTransform>().localPosition = playerPosition;
            player2.GetComponent<Animator>().SetBool("Appear", true);
        }
        else if (chosenPlayer.Equals("girl"))
        {
            player.GetComponent<RectTransform>().localPosition = playerPosition;
            player.GetComponent<Animator>().SetBool("Appear", true);
        }
        portal.GetComponent<RectTransform>().localPosition += new Vector3(0f,1000f,0f);
        GetComponent<Animator>().SetBool("Appear", true);
    }
    IEnumerator crossFade()
    {
        transition.SetBool("beDark", true);
        yield return new WaitForSeconds(1f);
        gCanvas.SetActive(false);
        endLevelCanvas.SetActive(true);
        transition.SetBool("beDark", false);
    }
}
