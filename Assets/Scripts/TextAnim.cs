using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnim : MonoBehaviour
{

    public TextMeshProUGUI text1;


    void Start()
    {
        StartCoroutine(CycleText());
    }

    IEnumerator CycleText()
    {
        //Play animation
        text1.text = "Loading";

        //Wait
        yield return new WaitForSeconds(1f);

        //Play animation
        text1.text = "Loading.";

        //Wait
        yield return new WaitForSeconds(1f);

        //Play animation
        text1.text = "Loading..";

        //Wait
        yield return new WaitForSeconds(1f);

        //Play animation
        text1.text = "Loading...";

        //Wait
        yield return new WaitForSeconds(1f);
    }
}
