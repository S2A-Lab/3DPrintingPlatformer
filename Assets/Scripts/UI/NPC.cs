using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitIcon;
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
    public GameObject interactableIcon;

    void Start()
    {
        interactableIcon.SetActive(false);
    }

    public void ToggleInteractIcon(bool isOn)
    {
        
        interactableIcon.SetActive(isOn);    
    }
    public bool CanInteract()
    {
        interactableIcon.SetActive(true);
        return !isDialogueActive;
    }

    public void Interact()
    {
        

        if (dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
        {
           
            return;
        }

        if (isDialogueActive)
        {
            
            NextLine();
        }
        else
        {
           
            StartDialogue();
        }

    }
    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;
        nameText.SetText(dialogueData.npcName);
        portraitIcon.sprite = dialogueData.icon; 
        dialoguePanel.SetActive(true);
        PauseController.SetPause(true);

        //TypeLine
        StartCoroutine(TypeLine());

    }
    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue(); 

        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");
        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }
        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex
         && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false); 
    }
}
