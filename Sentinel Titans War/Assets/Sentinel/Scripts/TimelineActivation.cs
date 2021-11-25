using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineActivation : MonoBehaviour
{
    private PlayableDirector director;
    public float timelineDelay;
    //public GameObject controlPanel;

    private void Awake()
    {
        director = GetComponentInChildren<PlayableDirector>();
        //director.played += Director_Played;
        //director.stopped += Director_Stopped;
    }

    /*private void Director_Stopped(PlayableDirector obj)
    {
        controlPanel.SetActive(true);
    }*/

    /*private void Director_Played(PlayableDirector obj)
    {
        controlPanel.SetActive(false);
    }*/

    public void StartTimelineCountDown()
    {
        StartCoroutine(TutorialTimeline());
    }

    IEnumerator TutorialTimeline()
    {
        yield return new WaitForSeconds(timelineDelay);
        director.Play();
    }
}
