using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchMovement : MonoBehaviour
{
    public GameObject girlCharacter;
    private RectTransform gRT;
    private RectTransform rT;
    // Start is called before the first frame update
    void Start()
    {
        gRT = girlCharacter.GetComponent<RectTransform>();
        rT = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Mathf.Abs(rT.localPosition.x - gRT.localPosition.x)>5f || Mathf.Abs(rT.localPosition.y - gRT.localPosition.y) > 5f)
        //{
        //    rT.localPosition = gRT.localPosition;
        //}
        
        
    }
}
