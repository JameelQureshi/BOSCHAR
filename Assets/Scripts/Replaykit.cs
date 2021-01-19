using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Apple.ReplayKit;

public class Replaykit : MonoBehaviour
{
    public Button recordButton;
    public GameObject meshIcon;
    public GameObject walkIcon;
    public GameObject slider;
    public GameObject hamburger;
    public GameObject dot;
    public GameObject progress;
    public GameObject time;
   

    private bool inPreview = false;
    private string lastError = "";
    private float startTime = 0;
    private bool starting = false;

    void OnGUI()
    {
        if (!ReplayKit.APIAvailable)
        {
            // hide recording feature
            recordButton.gameObject.SetActive(false);

            return;
        } else {
            recordButton.gameObject.SetActive(true);

            if(ReplayKit.isRecording) {
                TimeSpan delta = new TimeSpan(0, 0, (int)Math.Round(Time.time - startTime));
                time.GetComponent<Text>().text= delta.ToString(@"mm\:ss");
                float seconds = ((Time.time - startTime) / 60) % 1;
                progress.GetComponent<Image>().fillAmount = seconds;
            }
        }

        if (ReplayKit.recordingAvailable)
        {
            if (!inPreview)
            {
                ReplayKit.Preview();
                inPreview = true;
            }
        } else {
            inPreview = false;
        }
    }

    private IEnumerator TakeSSAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "My-bosch-experience.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        //new NativeShare().AddFile(filePath).SetSubject("Check out my Bosch Experience").Share();
    }

    public void Screenshot() {
        Debug.Log("Screenshot");

        StartCoroutine(TakeSSAndShare());
    }

    public void PrepareScreenRecording() {
        Debug.Log("Prepare Screen Recording");
        dot.SetActive(true);
    }

    public void CancelScreenRecording() {
        Debug.Log("Cancel Screen Recording");
        dot.SetActive(false);
    }

    public void StartRecording()
    {
        Debug.Log("StartRecording");

        if (!ReplayKit.APIAvailable)
        {
            dot.SetActive(false);
            return;
        }

      

        try
        {
            if (!ReplayKit.isRecording)
            {
                if (ReplayKit.recordingAvailable) {
                    ReplayKit.Discard();
                }

               

                ReplayKit.StartRecording();
                startTime = Time.time;

                progress.GetComponent<Image>().fillAmount = 0;
                progress.SetActive(true);
                time.SetActive(true);
                meshIcon.SetActive(false);
                walkIcon.SetActive(false);
                slider.SetActive(false);
                hamburger.SetActive(false);

               // recordToggle.disable();
                starting = false;
            }
        }
        catch (Exception e)
        {
            lastError = e.ToString();

            Debug.Log(lastError);

            progress.SetActive(false);
            dot.SetActive(false);
            time.SetActive(false);
            meshIcon.SetActive(true);
            walkIcon.SetActive(true);
            slider.SetActive(true);
            hamburger.SetActive(true);
           // recordToggle.enable();
        }
    }

    public void StopRecording()
    {
        Debug.Log("StopRecording");

        if (!ReplayKit.APIAvailable)
        {
            dot.SetActive(false);
            return;
        }

       // ToggleImage recordToggle = recordButton.GetComponent<ToggleImage>();

        try
        {
            if (ReplayKit.isRecording)
            {
                ReplayKit.StopRecording();

                progress.SetActive(false);
                dot.SetActive(false);
                time.SetActive(false);
                meshIcon.SetActive(true);
                walkIcon.SetActive(true);
                slider.SetActive(true);
                hamburger.SetActive(true);
               // recordToggle.enable();
            }
        }
        catch (Exception e)
        {
            lastError = e.ToString();

            Debug.Log(lastError);

            progress.SetActive(false);
            dot.SetActive(false);
            time.SetActive(false);
            meshIcon.SetActive(true);
            walkIcon.SetActive(true);
            slider.SetActive(true);
            hamburger.SetActive(true);
           // recordToggle.enable();
        }
    }

    //public void ToggleRecording()
    //{
    //    if (!ReplayKit.APIAvailable)
    //    {
    //        return;
    //    }

    //   // ToggleImage recordToggle = recordButton.GetComponent<ToggleImage>();

    //    try
    //    {
    //        if (!recordToggle.getStatus() && !ReplayKit.isRecording)
    //        {
    //            if (ReplayKit.recordingAvailable) {
    //                ReplayKit.Discard();
    //            }

             

    //            ReplayKit.StartRecording();

    //            progress.GetComponent<Image>().fillAmount = 0;
    //            progress.SetActive(true);
    //            time.SetActive(true);
    //            meshIcon.SetActive(false);
    //            walkIcon.SetActive(false);
    //            slider.SetActive(false);
    //            hamburger.SetActive(false);
    //        }
    //        else if (recordToggle.getStatus() && ReplayKit.isRecording)
    //        {
    //            ReplayKit.StopRecording();

    //            progress.SetActive(false);
    //            time.SetActive(false);
    //            meshIcon.SetActive(true);
    //            walkIcon.SetActive(true);
    //            slider.SetActive(true);
    //            hamburger.SetActive(true);
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        lastError = e.ToString();

    //        Debug.Log(lastError);

    //        progress.SetActive(false);
    //        time.SetActive(false);
    //        meshIcon.SetActive(true);
    //        walkIcon.SetActive(true);
    //        slider.SetActive(true);
    //        hamburger.SetActive(true);
    //    }
    //}

    public void ClickHandler()
    {
        Debug.Log("Click");

        if (ReplayKit.APIAvailable && ReplayKit.isRecording)
        {
            // Stop
            StopRecording();
        }
        else
        {
            // Screenshot
            Screenshot();
        }
    }

    public void LongPressHandler()
    {
        Debug.Log("LongPress");

        if (ReplayKit.APIAvailable && !ReplayKit.isRecording)
        {
            // Prepare
            PrepareScreenRecording();
        }
    }

    public void LongPressUpHandler()
    {
        Debug.Log("LongPressUp");
        if (ReplayKit.APIAvailable && !ReplayKit.isRecording)
        {
            // Start
            starting = true;
            StartRecording();
        }
    }

    public void LongPressExitHandler()
    {
        Debug.Log("LongPressExit");
        if (ReplayKit.APIAvailable && !ReplayKit.isRecording && !starting)
        {
            // Restore
            CancelScreenRecording();
        }
    }
}