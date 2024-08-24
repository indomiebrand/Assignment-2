using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CA3 : MonoBehaviour
{
    public AudioClip interactSound; 
    public float interactDistance = 5.0f;
    public TMP_Text interactionTxt;

    private bool isHovering = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        Hover();
    }

    private void Hover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                interactionTxt.gameObject.SetActive(true);
                isHovering = true;
            }
            else
            {
                interactionTxt.gameObject.SetActive(false);
                isHovering = false;
            }
        }
        else
        {
            interactionTxt.gameObject.SetActive(false);
            isHovering = false;
        }
    }

    private void Interact()
    {
        if (interactSound != null)
        {
            AudioSource.PlayClipAtPoint(interactSound, transform.position);
        }

        Destroy(gameObject);
        interactionTxt.gameObject.SetActive(false);
    }
}

