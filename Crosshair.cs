using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private RectTransform crosshair;

    public float restingSize;
    public float maxSize;
    public float speed;
    private float currentSize;
    GameObject topLine;
    GameObject bottomLine;
    GameObject leftLine;
    GameObject rightLine;

    private void Start()
    {
        crosshair = GetComponent<RectTransform>();

    }

    private void Update()
    {
        //expand crosshair while moving
        if (isMoving)
        {
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);
        } else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }

        crosshair.sizeDelta = new Vector2(currentSize, currentSize);


        //crosshair hide when ADSing
        if (isADS && !ADSReleased)
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
        if (ADSReleased)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }   

        //expand crosshair on fire
        if (isFiring)
        {
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * (speed*10));
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }

        
    }

    bool isMoving
    {
        get
        {
        
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                return true;
            }
            else
                return false;
        }
    }
    bool isADS
    {
        get
        {
            if (Input.GetMouseButton(1))
            
                return true;
            
            else
                return false;
        }
    }

    bool ADSReleased
    {
        get
        {
            if (Input.GetMouseButtonUp(1))
                return true;
            else if (Input.GetMouseButtonDown(1))
                return false;
            else return false;
        }
    }
    bool isFiring
    {
        get
        {
            if (Input.GetMouseButtonDown(0))
                return true;
            else
                return false;
        }
    }

    private void crosshairFire()
    {
        currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);
        
        return;
    }

}
