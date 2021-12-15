using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SkipCinematic : MonoBehaviour
{

    public GameObject skipScenePress, skipSceneTimeline;
    public bool skipAllowed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextScene());
        skipAllowed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SkipScene();
        }

        if (skipAllowed = true && Input.GetKeyDown(KeyCode.Space))
        {
            skipSceneTimeline.SetActive(true);
        }
    }

    void SkipScene()
    {
        skipScenePress.SetActive(true);
        skipAllowed = true;
    }


    IEnumerator NextScene()
        {
            yield return new WaitForSeconds(55f);
            skipSceneTimeline.SetActive(true);
        }
}
