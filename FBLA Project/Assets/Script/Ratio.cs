using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratio : MonoBehaviour
{
    private float[] ratios=new float[] {0,0};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float[] getRatios(string name)
    {
        
        switch (name)
        {
            case "Main Character":
                return new float[] { -0.3825f, 0.6f};
            case "Main Character 2":
                return new float[] { -0.3825f, 0.6f };
            case "Ground":
                return new float[] { 0f, -0.3775f };
            case "Starting Menu":
                return new float[] { 0f, 0f};
            case "Questions":
                return new float[] { 0f, 0f };
            case "End Page":
                return new float[] { 0f, 0f };

            default:
                Debug.Log("Ratio: "+name);
                break;
        }
        return ratios;
    }

}
