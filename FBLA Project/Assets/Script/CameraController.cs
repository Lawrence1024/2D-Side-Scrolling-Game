using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private GameObject player2;
    public Vector3 offset;
    private GameObject backgroundImage;
    private bool lockCenter = false;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1024, 768, true);
        player = GetComponent<AllObjects>().playerGirl;
        player2 = GetComponent<AllObjects>().playerBoy;
        backgroundImage= GetComponent<AllObjects>().backgroundObjs[0];
        offset = transform.position - player.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateOffSet() {
        if (player.activeSelf)
        {
            offset = transform.position - player.transform.position;
        }
        else if (player2.activeSelf)
        {
            offset = transform.position - player2.transform.position;
        }
    }
    private void LateUpdate()
    {
        float size = backgroundImage.GetComponent<RectTransform>().rect.width ;
        if (player.activeSelf)
        {
            if (((transform.position - player.transform.position).x < 0) || (!lockCenter && (transform.position - player.transform.position).x > 0))
            {
                lockCenter = true;
                offset = new Vector3(0, offset.y,offset.z);
            }
            if (player.transform.localPosition.x < 0 || player.transform.localPosition.x > 7f * size)
            {
                lockCenter = false;
                offset = new Vector3((transform.position - player.transform.position).x, offset.y, offset.z);

            }
            if (lockCenter)
            {
                transform.position = new Vector3(player.transform.position.x + offset.x, transform.position.y, transform.position.z);
            }
        }else if (player2.activeSelf)
        {
            if (((transform.position - player2.transform.position).x < 0) || (!lockCenter && (transform.position - player2.transform.position).x > 0))
            {
                lockCenter = true;
                offset = new Vector3(0, offset.y, offset.z);
            }
            if (player2.transform.localPosition.x < 0 || player2.transform.localPosition.x > 7f * size)
            {
                lockCenter = false;
                offset = new Vector3((transform.position - player2.transform.position).x, offset.y, offset.z);

            }
            if (lockCenter)
            {
                transform.position = new Vector3(player2.transform.position.x + offset.x, transform.position.y, transform.position.z);
            }
        }
        else
        {
            Debug.Log("No character is active");
        }
    }
}
