using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disappear : MonoBehaviour
{
    public float delay = 10f;

    void Start()
    {
        Invoke("OnTimeout", delay);
    }

    void OnTimeout()
    {
        this.GetComponent<Image>().CrossFadeAlpha(0.0f, 1f, false);
    }
}
