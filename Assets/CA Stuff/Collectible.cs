using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float SpeedIncrease; 
    public float JumpHeightIncrease;

    public void Collected(GameObject player)
    {
        Debug.Log("Collected!");

        StarterAssets.FirstPersonController controller = player.GetComponent<StarterAssets.FirstPersonController>();
        if (controller != null)
        {
            controller.MoveSpeed += SpeedIncrease;
            controller.JumpHeight += JumpHeightIncrease;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collected(other.gameObject);

            Destroy(gameObject);
        }
    }
}

