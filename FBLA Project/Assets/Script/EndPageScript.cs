using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndPageScript : MonoBehaviour
{
    public GameObject questionPageObj;
    public void Start()
    {
        
    }
    public void endGame(int points)
    {
          gameObject.SetActive(true);
          questionPageObj.SetActive(false);
          gameObject.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text =points.ToString();
    }
    public void quit()
    {
        Application.Quit();
    }
}
