using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relocate : MonoBehaviour
{
    //public GameObject ground;
    //public GameObject mainCharacter;
    public GameObject canvas;
    //public List<GameObject> canvases;
    public GameObject[] objs;
    public Camera mainCamera;
    private Vector2 cD;
    private Ratio r;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Ratio>();
        canvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        cD = canvas.GetComponent<RectTransform>().sizeDelta;
        foreach (GameObject g in objs)
        {
            modifyPosition(g);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void modifyPosition(GameObject obj)
    {
        RectTransform tempRT = obj.GetComponent<RectTransform>();
        float[] ratios = r.getRatios(obj.name);
        tempRT.anchoredPosition = new Vector2((float)(cD.x * ratios[0]), cD.y * ratios[1]);
    }
}
