using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qActivatorPos : MonoBehaviour
{
    public GameObject mainCamera;
    private AllObjects allObject;
    private List<GameObject> qActivators;
    public List<List<float>> positions=new List<List<float>>();
    // Start is called before the first frame update
    void Start()
    {
        allObject=mainCamera.GetComponent<AllObjects>();
        qActivators = allObject.qActivatorObjs;
        foreach (GameObject g in qActivators) {
            List<float> temp = new List<float>();
            temp.Add(g.GetComponent<RectTransform>().localPosition.x);
            temp.Add(g.GetComponent<RectTransform>().localPosition.y);
            positions.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
