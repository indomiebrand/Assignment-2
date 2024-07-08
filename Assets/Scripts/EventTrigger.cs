using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public GameObject targetObject; // the object to hide
    public GameObject showObject; // the object to show
    public AudioClip triggerSound; // the sound to play

    public BoxCollider boxCollider;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = triggerSound;

        // gets the BoxCollider component
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            Debug.LogWarning("No BoxCollider found on the GameObject.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (targetObject != null)
            {
                targetObject.SetActive(false); // hides the target object
            }

            if (showObject != null)
            {
                showObject.SetActive(true); // shows the new object
            }

            // plays the trigger sound when the trigger object disappears
            if (triggerSound != null)
            {
                audioSource.Play();
            }

            // disables the BoxCollider component
            if (boxCollider != null)
            {
                boxCollider.enabled = false;
            }
        }
    }
}
