using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mainCamera;
    private AllObjects allObject;
    private BoxCollider2D bC;
    private Rigidbody2D rig;
    private RectTransform rT;
    private float maxXV=190f;
    private float xV;
    private float yV;
    private bool onFloor;
    public Animator animator;
    private LevelLoader levelLoader;
    private Animator transition;
    private float waitTime = 1f;
    private GameObject qCanvas;
    private GameObject gCanvas;
    private GameObject qActivator;
    private GameObject background;
    private Animator portalAnimator;
    private GameObject minusPointCanvas;
    private LoadQuestions lQ;
    private GameObject titleCanvas;
    private float[] backgroundDimensions;
    private GameObject player;
    private GameObject player2;
    private GameObject audioManager;
    private bool canMove = true;
    private GameObject notEnoughTextCanvas;
    void Start()
    {
        updateGlobalVariables();
        lQ = qCanvas.GetComponentInChildren<LoadQuestions>();
        bC = GetComponent<BoxCollider2D >();
        rig = GetComponent<Rigidbody2D>();
        rT=GetComponent<RectTransform>();
        //rT.sizeDelta = new Vector2(100f,100f);
        backgroundDimensions = new float[] { background.GetComponent<RectTransform>().rect.width, background.GetComponent<RectTransform>().rect.height };
    }
    private void updateGlobalVariables()
    {
        allObject = mainCamera.GetComponent<AllObjects>();
        levelLoader = allObject.levelLoader.GetComponent<LevelLoader>();
        transition = allObject.levelLoader.GetComponent<Animator>();
        qCanvas = allObject.qCanvas;
        gCanvas = allObject.gCanvas;
        qActivator = allObject.qActivatorObjs[0];
        background = allObject.backgroundObjs[0];
        portalAnimator = allObject.portal.GetComponent<Animator>();
        minusPointCanvas=allObject.minusPointCanvas;
        notEnoughTextCanvas = allObject.notEnoughTextCanvas;
        titleCanvas = allObject.levelTitleCanvas;
        player = allObject.playerGirl;
        player2 = allObject.playerBoy;
        audioManager = allObject.audioManager;
    }
    // Update is called once per frame
    void Update()
    {
        moveCharacter();
    }
    private void moveCharacter()
    {
        
        xV = rig.velocity.x;
        yV = rig.velocity.y;
        animator.SetFloat("Speed", Mathf.Abs(xV));
        animator.SetBool("IsJumping",!onFloor);
        if (allObject.chosenPlayer.Equals("boy"))
        {
            if (player2.GetComponent<Animator>().GetBool("Turn"))
            {
                canMove = false;
            }
            if (player2.GetComponent<Animator>().GetBool("Appear"))
            {
                canMove = true;
            }
        }
        else
        {
            if (player.GetComponent<Animator>().GetBool("Turn"))
            {
                canMove = false;
            }
            if (player.GetComponent<Animator>().GetBool("Appear"))
            {
                canMove = true;
            }
        }
        if (canMove)
        {
            if (Input.GetKey("left"))
            {
                xV -= 30f;
                xV = xV < -maxXV ? -maxXV : xV;
                rig.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            if (Input.GetKey("right"))
            {
                xV += 30f;
                xV = xV > maxXV ? maxXV : xV;
                rig.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            if (Input.GetKeyDown("up") && onFloor)
            {
                yV += backgroundDimensions[1] / 6 * 5;
            }
        }
        rig.velocity = new Vector2(xV,yV);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "floor")
        {
            onFloor = true;
        }
        if (collision.gameObject.tag == "QuestionActivator")
        {
            lQ.currentQActivator = collision.gameObject;
            StartCoroutine(displayQuesion());
        }
        if (collision.gameObject.tag == "Portal")
        {
            if ((LoadQuestions.currentQuestionNum-1) % 8 == 0&& (LoadQuestions.currentQuestionNum - 1)!=0)
            {
                StartCoroutine(ActivatePortal());
            }
            else
            {
                Debug.Log("You didn't collect enough");
                StartCoroutine(displayNotEnoughText());
            }
            //StartCoroutine(ActivatePortal());
        }
        if (collision.gameObject.tag == "Bomb")
        {
            GameObject collideObject=collision.gameObject;
            RectTransform collideRT= collision.gameObject.GetComponent<RectTransform>();
            //audioManager.GetComponent<AudioManager>().Play("Explosion");
            Vector3 collideLP = collideRT.localPosition;
            float deltaY = Mathf.Abs(collideLP.y-transform.localPosition.y);
            float deltaX = (collideLP.x - transform.localPosition.x);
            LoadQuestions.overallPoints--;
            lQ.score--;
            lQ.updateScoreDisplay();
            if (deltaX>0)
            {
                xV -= 500f;
            }
            else if (deltaX <= 0)
            {
                xV += 500f;
            }
            if (deltaY > 40)
            {
                yV += 800f;
            }
            collision.gameObject.SetActive(false);
            StartCoroutine(ExplodeBomb());
            rig.velocity = new Vector2(xV, yV);
        }
    }
    IEnumerator displayNotEnoughText()
    {
        notEnoughTextCanvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        notEnoughTextCanvas.SetActive(false);
    }
    IEnumerator ExplodeBomb()
    {
        minusPointCanvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        minusPointCanvas.SetActive(false);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            onFloor = false;
        }
    }

    IEnumerator displayQuesion()
    {
        transition.SetBool("beDark",true);
        yield return new WaitForSeconds(waitTime);
        transition.SetBool("beDark", false);
        qCanvas.SetActive(true);
        gCanvas.SetActive(false);
        titleCanvas.SetActive(false);
    }
    IEnumerator ActivatePortal()
    {
        portalAnimator.SetBool("Activated", true);
        animator.SetBool("Turn", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Turn", false);
        portalAnimator.SetBool("Close", true);
    }
    IEnumerator timeOut(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
