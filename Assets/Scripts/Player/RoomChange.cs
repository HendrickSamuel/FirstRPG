using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomChange : MonoBehaviour
{
    
    public Vector2 playerChange;

    private CameraMovement cam;

    private MapData zoneinfo;
    public GameObject room;

     

    void Start()
    {
        zoneinfo = room.GetComponent<MapData>();
        cam = Camera.main.GetComponent<CameraMovement>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && other.isTrigger)
        {
            cam.minPos = zoneinfo.minCameraChange;
            cam.maxPos = zoneinfo.maxCameraChange;
            other.transform.position += (Vector3)playerChange;

            if(zoneinfo.dispalyText)
            {
                StartCoroutine(showTextCo());
            }
        }
    }

    private IEnumerator showTextCo()
    {
        zoneinfo.text.SetActive(true);
        zoneinfo.placeText.text = zoneinfo.placeName;
        yield return new WaitForSeconds(4f);
        zoneinfo.text.SetActive(false);
    }
}
