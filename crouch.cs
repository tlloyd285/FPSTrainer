using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouch : MonoBehaviour
{
    CharacterController playerheight;
    CapsuleCollider playerCol;
    float originalHeight;
    float characterOGHeight;
    public float reducedHeight;
    public float characterReducedHeight;
    // Start is called before the first frame update
    void Start()
    {
        playerheight = GetComponent<CharacterController>();
        playerCol = GetComponent<CapsuleCollider>();
        originalHeight = playerCol.height;
        characterOGHeight = playerheight.height;
        
    }

    // Update is called once per frame
    void Update()
    {
        // crouch;
        if (Input.GetKeyDown(KeyCode.C))
        {
            Crouch();
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            GoUp();
        }
    }
    //Reduce Height
    void Crouch()
    {
        playerCol.height = reducedHeight;
        characterOGHeight = characterReducedHeight;
    }
    //reset Height
    void GoUp()
    {
        playerCol.height = originalHeight;
        playerheight.height = originalHeight;
    }
}
