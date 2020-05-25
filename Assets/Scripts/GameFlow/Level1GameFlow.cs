using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1GameFlow : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(RunGameFlow());
    }

    private IEnumerator RunGameFlow()
    {
        NarrativeText.Instance.ShowText("Pangolin Farts...");
        yield return new WaitForSeconds(2);
        // show text
        // animate pangolin
        // spawn fart
        // give control to player
    }
}
