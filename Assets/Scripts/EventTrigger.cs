using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public GameObject targetObject; // the object to hide
    public GameObject showObject; // the object to show

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (targetObject != null)
            {
                targetObject.SetActive(false); // hide the target object
            }

            if (showObject != null)
            {
                showObject.SetActive(true); // show the new object
            }

            // Hide the trigger object itself
            gameObject.SetActive(false);
        }
    }
}

