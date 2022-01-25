using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundsLoader : MonoBehaviour
{
    public GameObject mainCamera;
    private AllObjects allObject;
    private List<GameObject> backgroundImages;
    private GameObject ground;
    private float backgroundSize;
    // Start is called before the first frame update
    void Start()
    {
        updateGlobalVariables();
        backgroundSize = backgroundImages[0].GetComponent<RectTransform>().rect.width;
        positionBackground();
    }
    public void updateGlobalVariables()
    {
        allObject = mainCamera.GetComponent<AllObjects>();
        backgroundImages = allObject.backgroundObjs;
        ground = allObject.ground;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void positionBackground() {
        for(int i = 0; i < backgroundImages.Count; i++) {
            RectTransform tempRT = backgroundImages[i].GetComponent<RectTransform>();
            SetLeft(tempRT, i * (backgroundSize));
            SetRight(tempRT, -i * (backgroundSize));
        }
    }
    private void SetLeft(RectTransform rt,float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }
    private void SetRight(RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }
}
