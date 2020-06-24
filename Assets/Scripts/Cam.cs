using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Camera scriptCam = Camera.current;
    public bool active = true;
    public float stepFactor = 1;
    public float stepper = 1;
    public float timer = 0.3f;
    public float upperLimit = 20;
    public float lowerLimit = 2;
    private float currFoV;
    private bool zoomIn = true;
    // Start is called before the first frame update
    void Start()
    {
        // Get FoV
        currFoV = scriptCam.fieldOfView;
        StartCoroutine(UpdateFoV(timer));
    }

    IEnumerator UpdateFoV(float timeOut)
    {
        while (active)
        {
            yield return new WaitForSeconds(timeOut);

            if (zoomIn && (currFoV > upperLimit))
            {
                zoomIn = false;
            }
            if (!zoomIn && (currFoV < lowerLimit))
            {
                zoomIn = true;
            }

            switch (zoomIn)
            {
                case true:
                    currFoV = currFoV + stepper * stepFactor;
                    break;
                case false:
                    currFoV = currFoV - stepper * stepFactor;
                    break;
            }

            // Update cam fov
            scriptCam.fieldOfView = currFoV;
        }
    }
}
