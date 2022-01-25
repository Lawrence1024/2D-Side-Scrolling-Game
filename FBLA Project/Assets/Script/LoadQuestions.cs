using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class LoadQuestions : MonoBehaviour
{
    public static int currentQuestionNum=0;
    public static int overallPoints=0;
    public GameObject mainCamera;
    private AllObjects allObjects;
    private GameObject title;
    private GameObject ChoiceA;
    private GameObject ChoiceB;
    private GameObject ChoiceC;
    private GameObject ChoiceD;
    private GameObject ChoiceE;
    private GameObject ChoiceF;
    private List<Question> allQuestions;
    public int score = 0;
    private GameObject scoreObj;
    private GameObject endPageObj;
    private List<GameObject> allChoices= new List<GameObject>();
    private List<bool> allResponseStatus=new List<bool> {false,false,false,false,false,false};
    private GameObject slider;
    private GameObject textArea;
    private GameObject responseAreaText;
    public Color defaultButton;
    private Color defaultButtonLight;
    private Color defaultButtonDark;
    private LevelLoader levelLoader;
    private GameObject qCanvas;
    private GameObject gCanvas;
    private float waitTime = 1f;
    private Animator transition;
    private List<GameObject> qActivators;
    public int qActivatorCount = 0;
    private bool positionToggle = false;
    public GameObject currentQActivator;
    private GameObject correctDisplay;
    private GameObject titleCanvas;
    private GameObject statusCanvas;
    private GameObject player;
    private GameObject player2;
    // Start is called before the first frame update
    void Start()
    {
        setAllGlobalVariables();
        updateColors();
        loadQuestions();
        addChoices();
        updateQuestion();
        modifyChoicesSize();
    }
    public void setAllGlobalVariables()
    {
        allObjects = mainCamera.GetComponent<AllObjects>();
        title=allObjects.question;
        ChoiceA=allObjects.choices[0];
        ChoiceB=allObjects.choices[1];
        ChoiceC=allObjects.choices[2];
        ChoiceD=allObjects.choices[3];
        ChoiceE=allObjects.choices[4];
        ChoiceF=allObjects.choices[5];
        scoreObj=allObjects.scoreDisplay;
        endPageObj=allObjects.EndLevelCanvas;
        allChoices= allObjects.choices;
        textArea=allObjects.responseArea;
        responseAreaText= allObjects.responseAreaText;
        levelLoader= allObjects.levelLoader.GetComponent<LevelLoader>();
        qCanvas=allObjects.qCanvas;
        gCanvas=allObjects.gCanvas;
        transition=allObjects.levelLoader.GetComponent<Animator>();
        qActivators= allObjects.qActivatorObjs;
        correctDisplay=allObjects.correctDisplay;
        titleCanvas=allObjects.levelTitleCanvas;
        statusCanvas=allObjects.notEnoughCanvas;
        player=allObjects.playerGirl;
        player2=allObjects.playerBoy;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //public methods
    public void checkAnswer()
    {
        RectTransform tempRectTran = currentQActivator.GetComponent<RectTransform>();
        Vector2 pos = tempRectTran.position;
        pos.x = pos.x - 300f;
        tempRectTran.position = pos;
        currentQActivator.SetActive(false);
        bool correct = findIfRight();
        if (correct)
        {
            StartCoroutine(answerCorrected());
            
        }
        else {
            StartCoroutine(answerWrong());
        }
        correctDisplay.SetActive(true);
    }
    private bool findIfRight() {
        if (currentQuestionNum - 1 < allQuestions.Count)
        {
            Question cQ = allQuestions[currentQuestionNum - 1];
            bool correct = true;
            if (cQ.category.Equals("MCM") || cQ.category.Equals("MCO"))
            {
                correct = checkAnswerForMC();
            }
            else if (cQ.category.Equals("FRQ"))
            {
                correct = checkAnswerForFRQ();
            }
            return correct;
        }
        return false;
    }
    IEnumerator answerCorrected()
    {
        title.GetComponent<TMP_Text>().text = "Correct!";
        correctDisplay.GetComponent<TMP_Text>().text = "O";
        correctRection1();
        yield return new WaitForSeconds(waitTime);
        correctRection2();
        updateQuestion();
    }
    private void correctRection1()
    {
        transition.SetBool("beDark", true);
        score += allQuestions[currentQuestionNum - 1].reward;
        overallPoints+= allQuestions[currentQuestionNum - 1].reward;
        scoreObj.GetComponent<TMP_Text>().text = "Current Score: " + score;
        if (currentQuestionNum == 0)
        {
            statusCanvas.GetComponentInChildren<TMP_Text>().text = "Collected " + 0 + "/8";
        }else if (currentQuestionNum % 8 == 0)
        {
            statusCanvas.GetComponentInChildren<TMP_Text>().text = "Collected " + 8 + "/8";
        }
        else
        {
            statusCanvas.GetComponentInChildren<TMP_Text>().text = "Collected " + (currentQuestionNum) % 8 + "/8";
        }
        


    }
    private void correctRection2()
    {
        transition.SetBool("beDark", false);
        qCanvas.SetActive(false);
        gCanvas.SetActive(true);
        titleCanvas.SetActive(true);
        allResponseStatus = new List<bool> { false, false, false, false, false, false };
        positionToggle = false;
        qActivatorCount++;
    }
    IEnumerator answerWrong()
    {
        allQuestions[currentQuestionNum-1].reward--;
        correctDisplay.GetComponent<TMP_Text>().text = "X";
        if (allQuestions[currentQuestionNum - 1].reward == 0)
        {
            title.GetComponent<TMP_Text>().text = "You have reach your 5 tires limit";
            yield return new WaitForSeconds(1f);
            displayAnswer();
            yield return new WaitForSeconds(4f);
            correctRection1();
            yield return new WaitForSeconds(waitTime);
            correctRection2();
            updateQuestion();
        }
        else
        {
            title.GetComponent<TMP_Text>().text = "Try Again Later";
            transition.SetBool("beDark", true);
            yield return new WaitForSeconds(waitTime);
            transition.SetBool("beDark", false);
            qCanvas.SetActive(false);
            gCanvas.SetActive(true);
            titleCanvas.SetActive(true);
            scoreObj.GetComponent<TMP_Text>().text = "Current Score: " + score;
            allResponseStatus = new List<bool> { false, false, false, false, false, false };
            RectTransform tempRectTran = currentQActivator.GetComponent<RectTransform>();
            Vector3 pos = tempRectTran.position;
            if (!positionToggle)
            {
                positionToggle = true;
                pos.x = gCanvas.GetComponent<qActivatorPos>().positions[qActivatorCount % 8][0] + 30f;
                pos.y = gCanvas.GetComponent<qActivatorPos>().positions[qActivatorCount % 8][1];
            }
            else
            {
                positionToggle = false;
                pos.x = gCanvas.GetComponent<qActivatorPos>().positions[qActivatorCount % 8][0] - 130f;
                pos.y = gCanvas.GetComponent<qActivatorPos>().positions[qActivatorCount % 8][1];
            }
            tempRectTran.localPosition = pos;
            correctDisplay.SetActive(false);
            currentQActivator.SetActive(true);
            title.GetComponent<TMP_Text>().text = allQuestions[currentQuestionNum - 1].question;
            foreach (GameObject obj in allChoices)
            {
                Button tempBtn = obj.GetComponent<Button>();
                ColorBlock colors = tempBtn.colors;
                colors.normalColor = defaultButtonLight;
                colors.highlightedColor = defaultButtonLight;
                colors.pressedColor = defaultButtonDark;
                tempBtn.colors = colors;
            }
        }
    }
    public void displayAnswer()
    {
        Question cQ = allQuestions[currentQuestionNum - 1];
        if (cQ.category.Equals("MCM") || cQ.category.Equals("MCO"))
        {
             StartCoroutine(displayMCAnswer());
        }
        else if (cQ.category.Equals("FRQ"))
        {
            StartCoroutine(displayFRQAnswer());
        }
    }
    IEnumerator displayMCAnswer()
    {
        string answer = allQuestions[currentQuestionNum - 1].answer;
        string[] answers = answer.Split(new char[] { '-' });
        title.GetComponent<TMP_Text>().text = "Answer: ";
        List<int> answerIndexes = new List<int>();
        foreach (string ans in answers)
        {
            int tempNum = convertAnsToNum(ans);
            if (tempNum >= 0 && tempNum <= 5)
            {
                answerIndexes.Add(tempNum);
            }
        }
        for(int i = 0; i < answerIndexes.Count; i++)
        {
            if (i == answerIndexes.Count - 1)
            {
                title.GetComponent<TMP_Text>().text += allChoices[answerIndexes[i]].GetComponentInChildren<TMP_Text>().text;
            }
            else
            {
                title.GetComponent<TMP_Text>().text += allChoices[answerIndexes[i]].GetComponentInChildren<TMP_Text>().text + ", ";
            }
            //title.GetComponent<TMP_Text>().text += allChoices[answerIndexes[i]].GetComponentInChildren<TMP_Text>().text + " ";
        }
        for (int i = 0; i < 6; i++)
        {
            foreach (int index in answerIndexes)
            {
                if (i==0)
                {
                    if (allResponseStatus[index] == false)
                    {
                        click(index);
                    }
                }
                else
                {
                    click(index);
                }
                
            }
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    IEnumerator displayFRQAnswer()
    {
        string myAnswer = textArea.GetComponent<TMP_InputField>().text;
        string answer = allQuestions[currentQuestionNum - 1].answer;
        string[] answers = answer.Split(new char[] { '-' });
        List<string> answerList = new List<string>();
        title.GetComponent<TMP_Text>().text = "Answer: ";
        foreach (string ans in answers)
        {
            answerList.Add(ans);
        }
        for(int i = 0; i < answerList.Count; i++)
        {
            if (i == answerList.Count - 1)
            {
                title.GetComponent<TMP_Text>().text += answerList[i];
            }
            else
            {
                title.GetComponent<TMP_Text>().text += answerList[i]+" or ";
            }
        }
        yield return new WaitForSeconds(2f);
    }
    public void click(int answerChoiceIndex) {
        allResponseStatus[answerChoiceIndex]=!allResponseStatus[answerChoiceIndex];
        Button tempBtn = allChoices[answerChoiceIndex].GetComponent<Button>();
        ColorBlock colors = tempBtn.colors;
        if (allResponseStatus[answerChoiceIndex] == true)
        {
            colors.normalColor = defaultButtonDark;
            colors.highlightedColor = defaultButtonDark;
            colors.pressedColor = defaultButtonLight;
        }
        else {
            colors.normalColor = defaultButtonLight;
            colors.highlightedColor = defaultButtonLight;
            colors.pressedColor = defaultButtonDark;
        }
        tempBtn.colors = colors;
    }

    //private methods
    private void loadQuestions()
    {
        allQuestions = new List<Question>();
        TextAsset questionData = Resources.Load<TextAsset>("QuestionData");

        string allData = questionData.text;
        string[] rows = allData.Split(new char[] { '\n' });
        for (int i = 1; i < rows.Length - 1; i++)
        {
            string[] cRow = rows[i].Split(new char[] { ',' });
            Question tempQ = new Question();
            if (cRow[0] != "")
            {
                tempQ.question = cRow[0];
                tempQ.choiceA = cRow[1];
                tempQ.choiceB = cRow[2];
                tempQ.choiceC = cRow[3];
                tempQ.choiceD = cRow[4];
                tempQ.choiceE = cRow[5];
                tempQ.choiceF = cRow[6];
                tempQ.answer = cRow[7];
                int.TryParse(cRow[8], out tempQ.reward);
                int.TryParse(cRow[9], out tempQ.level);
                tempQ.category = cRow[10];
                allQuestions.Add(tempQ);
            }
        }
        
    }
    private void updateQuestion()
    {
        currentQuestionNum++;
        if (currentQuestionNum == 1)
        {
            titleCanvas.SetActive(false);
        }
        correctDisplay.SetActive(false);
        if (currentQuestionNum < allQuestions.Count)
        {
            Question cQ = allQuestions[currentQuestionNum - 1];
            if (cQ.category.Equals("MCM") || cQ.category.Equals("MCO"))
            {
                setMCText(cQ);
            }
            else if (cQ.category.Equals("FRQ"))
            {
                setFRQText(cQ);
            }
            
        }
        else
        {

            levelLoader.GetComponent<LevelLoader>().endGame(score);
            //endPageObj.GetComponent<EndPageScript>().endGame(score);
        }
    }
    private void addChoices() {
        allChoices.Add(ChoiceA);
        allChoices.Add(ChoiceB);
        allChoices.Add(ChoiceC);
        allChoices.Add(ChoiceD);
        allChoices.Add(ChoiceE);
        allChoices.Add(ChoiceF);
    }
    private void modifyChoicesSize() { 
        foreach(GameObject g in allChoices){
            RectTransform tempRT = g.GetComponent<RectTransform>();
            RectTransform pannelRT = GetComponent<RectTransform>();
            tempRT.sizeDelta = new Vector2(pannelRT.rect.width*0.4125f, pannelRT.rect.height * 0.1285f);
        }
    }
    private int convertAnsToNum(string ans) {
        switch (ans)
        {
            case "A":
                return 0;
            case "B":
                return 1;
            case "C":
                return 2;
            case "D":
                return 3;
            case "E":
                return 4;
            case "F":
                return 5;

            default:
                Debug.Log("Error in Load Question in convertAnsToNum() "+ans);
                return -1;
        }
    }
    //Present options onto MC problem questions
    private void setMCText(Question cQ) {
        textArea.SetActive(false);
        responseAreaText.SetActive(false);
        ChoiceA.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = cQ.choiceA;
        ChoiceB.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = cQ.choiceB;
        ChoiceC.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = cQ.choiceC;
        ChoiceD.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = cQ.choiceD;
        ChoiceE.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = cQ.choiceE;
        ChoiceF.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = cQ.choiceF;
        title.GetComponent<TMP_Text>().text = cQ.question;
        foreach (GameObject obj in allChoices)
        {
            Button tempBtn = obj.GetComponent<Button>();
            ColorBlock colors = tempBtn.colors;
            colors.normalColor = defaultButtonLight;
            colors.highlightedColor = defaultButtonLight;
            colors.pressedColor = defaultButtonDark;
            tempBtn.colors = colors;
            if (obj.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text != "")
            {
                obj.SetActive(true);
            }
            else {
                obj.SetActive(false);
            }
        }
    }
    //Present the textArea for FRQ responses
    private void setFRQText(Question cQ) {
        textArea.SetActive(true);
        responseAreaText.SetActive(true);
        textArea.GetComponent<TMP_InputField>().text = "0";
        foreach (GameObject obj in allChoices) {
            obj.SetActive(false);
        }
        title.GetComponent<TMP_Text>().text = cQ.question;
    }
    private bool checkAnswerForMC() {
        string answer = allQuestions[currentQuestionNum - 1].answer;
        string[] answers = answer.Split(new char[] { '-' });
        List<int> answerIndexes = new List<int>();
        bool[] answerBooleans = new bool[] { false, false, false, false, false, false};
        foreach (string ans in answers)
        {
            int tempNum = convertAnsToNum(ans);
            if (tempNum >= 0 && tempNum <= 5)
            {
                answerIndexes.Add(tempNum);
                answerBooleans[tempNum] = true;
            }
        }
        for(int i = 0; i < answerBooleans.Length; i++)
        {
            if (answerBooleans[i] != allResponseStatus[i])
            {
                return false;
            }
        }
        return true;
    }
    private bool checkAnswerForFRQ() {
        string myAnswer = textArea.GetComponent<TMP_InputField>().text;
        string answer = allQuestions[currentQuestionNum - 1].answer;
        string[] answers = answer.Split(new char[] { '-' });
        foreach (string ans in answers)
        {
            if (myAnswer.Equals(ans))
            {
                return true;
            }
        }
        return false;
    }
    private bool stringEqual(string s1, string s2) {
        if (s1.Length != s2.Length) {
            return false; 
        }
        for(int i = 0; i < s1.Length; i++)
        {
            if (!(s1[i] == s2[i]))
            {
                return false;
            }
        }
        return true;
    }
    private void updateColors() {
        defaultButton.a -= 0.15f;
        defaultButtonLight= defaultButton;
        defaultButton.a += 0.3f;
        defaultButtonDark= defaultButton;
        defaultButton.a -= 0.15f;
    }
    public void updateScoreDisplay()
    {
        scoreObj.GetComponent<TMP_Text>().text = "Current Score: " + score;
    }
    public void performTransit()
    {
        StartCoroutine(crossFade());
    }
    IEnumerator crossFade()
    {
        transition.SetBool("beDark", true);
        yield return new WaitForSeconds(1f);
        transition.SetBool("beDark", false);
    }
    public void intoGame()
    {
        levelLoader.LoadNextLevel();
    }
}
