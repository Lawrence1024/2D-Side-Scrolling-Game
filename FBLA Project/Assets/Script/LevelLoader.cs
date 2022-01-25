using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{

    public GameObject mainCamera;
    private AllObjects allObject;
    private Animator transition;
    private float waitTime = 1f;
    private GameObject trophyCanvas;
    private GameObject gCanvas;
    private GameObject titleCanvas;
    private GameObject statusCanvas;
    private GameObject qCanvas;
    private GameObject player;
    private GameObject player2;
    private int currentLevel=1;
    private List<GameObject> backgroundObjs;
    private List<Sprite> backgroundImgSprites;
    private List<GameObject> qActivators;
    private GameObject endLevelCanvas;
    private Vector3 cameraPosition;
    private Vector3 playerPosition;
    private List<List<Sprite>> allQActivatorsImg=new List<List<Sprite>>();
    private List<GameObject> bombs;
    private AudioManager audioManager;
    private List<GameObject> soundObjs;
    // Start is called before the first frame update
    void Start()
    {
        updateGlobalVariables();   
    }
    private void updateGlobalVariables()
    {
        allObject = mainCamera.GetComponent<AllObjects>();
        transition = allObject.levelLoader.GetComponent<Animator>();
        trophyCanvas = allObject.TrophyCanvas;
        gCanvas = allObject.gCanvas;
        titleCanvas = allObject.levelTitleCanvas;
        statusCanvas = allObject.notEnoughCanvas;
        qCanvas = allObject.qCanvas;
        player = allObject.playerGirl;
        player2 = allObject.playerBoy;
        backgroundObjs=allObject.backgroundObjs;
        backgroundImgSprites = allObject.backgroundImgSprites;
        qActivators = allObject.qActivatorObjs;
        allQActivatorsImg = allObject.allQActivatorImgSprites;
        endLevelCanvas = allObject.EndLevelCanvas;
        cameraPosition = allObject.cameraPosition;
        playerPosition = allObject.playerPosition;
        bombs = allObject.backgroundObjs;
        audioManager = allObject.audioManager.GetComponent<AudioManager>();
        soundObjs = allObject.soundObjs;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadNextLevel() {
        currentLevel++;
        if (currentLevel <= 4)
        {
            StartCoroutine(ILoadNextLevel());
        }
        else
        {
            trophyCanvas.SetActive(true);
            gCanvas.SetActive(false);
            titleCanvas.SetActive(false);
            statusCanvas.SetActive(false);
            trophyCanvas.GetComponentInChildren<TMP_Text>().text = LoadQuestions.overallPoints+" Points!";
            Debug.Log("This is last layer");
        }
    }
    IEnumerator ILoadNextLevel()
    {
        transition.SetBool("beDark", true);
        yield return new WaitForSeconds(waitTime);
        foreach (GameObject g in backgroundObjs)
        {
            g.GetComponent<Image>().sprite = backgroundImgSprites[currentLevel - 1];
        }
        if (currentLevel == 1)
        {
            titleCanvas.GetComponentInChildren<TMP_Text>().text = "Level 1-Feuture";
            soundObjs[0].GetComponentInChildren<AudioSource>().Pause();
            soundObjs[0].SetActive(false);
            soundObjs[1].SetActive(true);
        }
        else if (currentLevel == 2)
        {
            titleCanvas.GetComponentInChildren<TMP_Text>().text = "Level 2-Business";
            soundObjs[1].GetComponentInChildren<AudioSource>().Pause();
            soundObjs[1].SetActive(false);
            soundObjs[2].SetActive(true);
        }
        else if (currentLevel == 3)
        {
            titleCanvas.GetComponentInChildren<TMP_Text>().text = "Level 3-Leader";
            soundObjs[2].GetComponentInChildren<AudioSource>().Pause();
            soundObjs[2].SetActive(false);
            soundObjs[3].SetActive(true);
        }
        else if (currentLevel == 4)
        {
            titleCanvas.GetComponentInChildren<TMP_Text>().text = "Level 4-America";
            soundObjs[3].GetComponentInChildren<AudioSource>().Pause();
            soundObjs[3].SetActive(false);
            soundObjs[4].SetActive(true);
        }

        for (int i = 0; i < qActivators.Count; i++)
        {
            qActivators[i].GetComponent<Image>().sprite = allQActivatorsImg[currentLevel - 1][i];
            qActivators[i].SetActive(true);
            RectTransform tempRectTran = qActivators[i].GetComponent<RectTransform>();
            Vector3 pos = tempRectTran.position;
            pos.x = gCanvas.GetComponent<qActivatorPos>().positions[i][0];
            pos.y = gCanvas.GetComponent<qActivatorPos>().positions[i][1];
            tempRectTran.localPosition = pos;
        }
        foreach (GameObject g in bombs)
        {
            g.SetActive(true);
        }
        gCanvas.SetActive(true);
        endLevelCanvas.SetActive(false);
        mainCamera.GetComponent<Transform>().position = cameraPosition;
        statusCanvas.GetComponentInChildren<TMP_Text>().text = "Collected " + 0 + "/8";
        //LoadQuestions.currentQuestionNum++;
        transition.SetBool("beDark", false);
    }
    public void endGame(int score)
    {
        trophyCanvas.GetComponentInChildren<TMP_Text>().text = score+" Points!";
    }
}
