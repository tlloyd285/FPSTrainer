using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnDoorTrigger : MonoBehaviour
{
    [SerializeField] private Text tutText;

    private void OnTriggerEnter(Collider other)
    {
        tutText.enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        tutText.enabled = false;
    }
}
