using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public Vector2Value playerInitPos;

    public GameObject transitionIn;
    public GameObject transitionOut;

    public float minWait;


    public void Awake()
    {
        if(transitionIn != null)
        {
            GameObject panel = Instantiate(transitionIn, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1); 
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            //other.GetComponent<PlayerMovementScript>().setState(PlayerState.stagger);
            playerInitPos.initialValue = playerPosition;
            StartCoroutine(FadeCo());
        }
    }

    public IEnumerator FadeCo()
    {
        if (transitionOut != null)
        {
           Instantiate(transitionIn, Vector3.zero, Quaternion.identity);
        }
       
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneToLoad);
        
        while(!asyncOp.isDone)
        {
            yield return null;
        }
    }
}
