using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Blink : MonoBehaviour
{
    float time = 0;
    bool tmpEnable = true;
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if (time > 0.7f && tmpEnable)
        {
            tmpEnable = false;
            GetComponent<TextMeshProUGUI>().enabled = tmpEnable;
        }
        else if (time > 1f && !tmpEnable)
        {
            tmpEnable = true;
            GetComponent<TextMeshProUGUI>().enabled = tmpEnable;
            time -= 1f;
        }

    }
}
