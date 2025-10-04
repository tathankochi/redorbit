using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class Dialog0 : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public string[] dialogLines;
    public int currentDialogIndex = 0;
    public float textSpeed = 0.05f;
    void Start()
    {
        // dialogText = GetComponentInChildren<TextMeshProUGUI>();
        currentDialogIndex = 0;
        // StartDialog();

    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (dialogText.text == dialogLines[currentDialogIndex])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogText.text = dialogLines[currentDialogIndex];
            }
        }
    }
    public void StartDialog()
    {
        dialogText.text = "";
        StartCoroutine(TypeLine());
    }
    void NextLine()
    {
        if (currentDialogIndex < dialogLines.Length - 1)
        {
            currentDialogIndex++;
            dialogText.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogText.text = "";
            gameObject.SetActive(false);
        }
    }
    IEnumerator TypeLine()
    {
        foreach (char c in dialogLines[currentDialogIndex].ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    public void ResetDialog()
    {
        currentDialogIndex = 0;
        dialogText.text = "";
    }
}
