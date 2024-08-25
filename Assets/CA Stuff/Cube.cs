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
    private Renderer cubeRenderer;

    public Color colourAtMaxHP = Color.green;
    public Color colourAt3HP = Color.yellow;
    public Color colourAt2HP = new Color(1f, 0.5f, 0f); 
    public Color colourAtCriticalHP = Color.red;

    void Start()
    {
        currentHP = maxHP;
        cubeRenderer = GetComponent<Renderer>();
        UpdateHPText();
        UpdateCubeColor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

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
            UpdateCubeColor();

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
            StartCoroutine(LerpColor(colourAtCriticalHP, colourAt2HP));
            currentHP = 2; 
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

    void UpdateCubeColor()
    {
        switch (currentHP)
        {
            case 4:
                cubeRenderer.material.color = colourAtMaxHP;
                break;
            case 3:
                cubeRenderer.material.color = colourAt3HP;
                break;
            case 2:
                cubeRenderer.material.color = colourAt2HP;
                break;
            case 1:
                cubeRenderer.material.color = colourAtCriticalHP;
                break;
        }
    }

    IEnumerator LerpColor(Color fromColor, Color toColor)
    {
        float duration = recoveryTime; 
        float time = 0;
        while (time < duration)
        {
            cubeRenderer.material.color = Color.Lerp(fromColor, toColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        cubeRenderer.material.color = toColor; 
    }
}
