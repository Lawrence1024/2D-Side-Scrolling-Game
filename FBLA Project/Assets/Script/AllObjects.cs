using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class AllObjects : MonoBehaviour
{
    public GameObject mainCamera;
    public Vector3 cameraPosition;
    public GameObject levelLoader;
    public GameObject gCanvas;
    public List<Sprite> backgroundImgSprites=new List<Sprite>();
    public List<GameObject> backgroundObjs=new List<GameObject>();
    public List<Sprite> qActivatorImgSprites1=new List<Sprite>();
    public List<Sprite> qActivatorImgSprites2=new List<Sprite>();
    public List<Sprite> qActivatorImgSprites3=new List<Sprite>();
    public List<Sprite> qActivatorImgSprites4=new List<Sprite>();
    public List<List<Sprite>> allQActivatorImgSprites=new List<List<Sprite>>();
    public List<GameObject> qActivatorObjs=new List<GameObject>();
    public List<GameObject> Floors=new List<GameObject>();
    public List<GameObject> Bombs=new List<GameObject>();
    public GameObject ground;
    public GameObject playerGirl;
    public GameObject playerBoy;
    public string chosenPlayer = "girl";
    public Vector3 playerPosition;
    public GameObject portal;
    public GameObject notEnoughCanvas;
    public GameObject levelTitleCanvas;
    public GameObject minusPointCanvas;
    public GameObject qCanvas;
    public GameObject question;
    public GameObject scoreDisplay;
    public List<GameObject> choices=new List<GameObject>();
    public GameObject submitBtn;
    public GameObject correctDisplay;
    public GameObject responseArea;
    public GameObject responseAreaText;
    public GameObject EndLevelCanvas;
    public GameObject endLevelPoints;
    public GameObject TrophyCanvas;
    public GameObject menuCanvas;
    public GameObject whatIsThisCanvas;
    public GameObject howToCanvas;
    public GameObject settingCanvas;
    public GameObject audioManager;
    public List<GameObject> soundObjs;
    public GameObject notEnoughTextCanvas;
    public List<GameObject> allGameObjects=new List<GameObject>();
    public List<List<Sprite>> allImages=new List<List<Sprite>>();
    // Start is called before the first frame update
    void Start()
    {
        gCanvas.SetActive(true);
        allGameObjects.Add(mainCamera);
        cameraPosition = GetComponent<Transform>().position;
        allGameObjects.Add(levelLoader);
        allGameObjects.Add(gCanvas);
        allGameObjects.Add(ground);
        allGameObjects.Add(playerGirl);
        allGameObjects.Add(playerBoy);
        playerPosition = playerGirl.GetComponent<RectTransform>().localPosition;
        allGameObjects.Add(portal);
        allGameObjects.Add(notEnoughCanvas);
        allGameObjects.Add(levelTitleCanvas); 
        allGameObjects.Add(qCanvas);
        allGameObjects.Add(question);
        allGameObjects.Add(scoreDisplay);
        allGameObjects.Add(submitBtn);
        allGameObjects.Add(correctDisplay);
        allGameObjects.Add(responseArea);
        allGameObjects.Add(responseAreaText);
        allGameObjects.Add(EndLevelCanvas);
        allGameObjects.Add(TrophyCanvas);
        allGameObjects.Add(menuCanvas);
        allGameObjects.Add(whatIsThisCanvas);
        allGameObjects.Add(howToCanvas);
        allGameObjects.Add(settingCanvas);
        allGameObjects.Add(audioManager);
        allGameObjects.Add(notEnoughTextCanvas);
        allGameObjects.AddRange(backgroundObjs);
        allGameObjects.AddRange(qActivatorObjs);
        allGameObjects.AddRange(Floors);
        allGameObjects.AddRange(Bombs);
        allGameObjects.AddRange(choices);
        allGameObjects.AddRange(soundObjs);
        allQActivatorImgSprites.Add(qActivatorImgSprites1);
        allQActivatorImgSprites.Add(qActivatorImgSprites2);
        allQActivatorImgSprites.Add(qActivatorImgSprites3);
        allQActivatorImgSprites.Add(qActivatorImgSprites4);
        allImages.Add(backgroundImgSprites);
        allImages.AddRange(allQActivatorImgSprites);
        setObjectsActive();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitProgram();
        }
    }
    public void quitProgram()
    {
        Application.Quit();
    }
    private void setObjectsActive()
    {
        foreach (GameObject g in allGameObjects)
        {
            g.SetActive(false);
        }
        mainCamera.SetActive(true);
        menuCanvas.SetActive(true);
        gCanvas.SetActive(true);
        audioManager.SetActive(true);
        soundObjs[0].SetActive(true);
    }
    public void startGame()
    {
        foreach (GameObject g in allGameObjects)
        {
            g.SetActive(true);
        }
        StartCoroutine(hideCanvases());
        playerBoy.SetActive(false);
        playerGirl.SetActive(false);
        if (chosenPlayer.Equals("boy"))
        {
            playerBoy.SetActive(true);
        }else
        {
            playerGirl.SetActive(true);
        }
        foreach (GameObject g in soundObjs)
        {
            g.SetActive(false);
        }
        soundObjs[0].SetActive(false);
        soundObjs[1].SetActive(true);
    }
    IEnumerator hideCanvases()
    {
        yield return new WaitForEndOfFrame();
        qCanvas.SetActive(false);
        EndLevelCanvas.SetActive(false);
        TrophyCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        whatIsThisCanvas.SetActive(false);
        howToCanvas.SetActive(false);
        settingCanvas.SetActive(false);
        notEnoughTextCanvas.SetActive(false);
    }
    public void selectCharacter(string name)
    {
        chosenPlayer = name;
    }
}
