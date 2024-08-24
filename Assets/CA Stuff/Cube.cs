using System.Collections;
using UnityEngine;
using TMPro;

public class Cube : MonoBehaviour
{
    public int maxHP = 4;
    public int criticalHP = 1;
    public float recoveryTime = 2.0f; 
    public TMP_Text hpText;

    private int currentHP;
    private bool isCritical = false;

    void Start()
    {
        currentHP = maxHP;
        UpdateHPText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hits the cube.");
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        if (isCritical && currentHP == criticalHP)
        {
            Destroy(gameObject);
        }
        else
        {
            currentHP--;
            UpdateHPText();

            if (currentHP == criticalHP)
            {
                EnterCriticalState();
            }
        }
    }

    void EnterCriticalState()
    {
        isCritical = true;
        StartCoroutine(RecoverHealth());
    }

    IEnumerator RecoverHealth()
    {
        yield return new WaitForSeconds(recoveryTime);

        if (currentHP == criticalHP)
        {
            currentHP = 2; // Recover to 2 hp
            isCritical = false;
            UpdateHPText();
        }
    }

    void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + currentHP;
        }
    }
}
