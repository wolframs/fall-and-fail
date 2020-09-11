using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Wolfram:
/// Stellt Funktionalität bereit, um Kamera FoV laufend zu manipulieren und ggf. auf aktueller Position basiert
/// zu verlangsamen und zu beschleunigen (smoothing).
/// </summary>
public class Cam : MonoBehaviour
{
    public Camera scriptCam;
    public bool active = true;
    public bool smoothen = true;
    public float smoothingAddend = 0.1f;
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
                zoomIn = false;
            if (!zoomIn && (currFoV < lowerLimit))
                zoomIn = true;

            switch (zoomIn)
            {
                case true:
                    currFoV = ChangeFoV(currFoV, Direction.ZoomIn);
                    break;
                case false:
                    currFoV = ChangeFoV(currFoV, Direction.ZoomOut);
                    break;
            }

            // Update cam fov
            scriptCam.fieldOfView = currFoV;
        }
    }

    private float ChangeFoV(float currFoV, Direction direction)
    {
        float _stepper = stepper;

        if (smoothen)
        {
            if (IsCloserTo(currFoV, upperLimit, lowerLimit) == 1 )
            {
                // Näher am oberen Limit
                _stepper += smoothingAddend * DistanceToMaxFoV(currFoV, upperLimit);
            }
            else
            {
                // Näher am unteren Limit
                _stepper += smoothingAddend * DistanceToMaxFoV(currFoV, lowerLimit);
            }
        }

        switch(direction)
        {
            case Direction.ZoomIn:
                currFoV = currFoV + _stepper * stepFactor;
                break;
            case Direction.ZoomOut:
                currFoV = currFoV - _stepper * stepFactor;
                break;
        }
        
        return currFoV;
    }

    private enum Direction
    {
        ZoomIn,
        ZoomOut
    }

    private int IsCloserTo(float x, float compareValue1, float compareValue2)
    {
        if (
            (Mathf.Abs(compareValue1 - x))
            <
            (Mathf.Abs(compareValue2 - x))
            )
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    private float DistanceToMaxFoV(float x, float targetMaxDistance)
    {
        return (Mathf.Abs(x - targetMaxDistance));
    }
}
