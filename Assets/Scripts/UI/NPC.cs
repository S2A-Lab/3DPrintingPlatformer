using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    private GameController gameController;
    private TMP_Text dialogueText, nameText;
    private Image portraitIcon;
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
    public GameObject interactableIcon;

     public AudioClip soundClip; // Assign in Inspector
    public AudioSource audioSource;


    void Start()
    {
        interactableIcon.SetActive(false);
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        dialogueText = gameController.dialogueText;
        nameText = gameController.nameText;
        portraitIcon = gameController.portraitIcon; 
        audioSource.clip = soundClip;
        
    }

    public void ToggleInteractIcon(bool isOn)
    {

        interactableIcon.SetActive(isOn);
        
        if (isOn == false)
        {
            EndDialogue();
        }   
    }
    public bool CanInteract()
    {
        interactableIcon.SetActive(true);
        return !isDialogueActive;
    }

    public void Interact()
    {
        

        if (dialogueData == null)
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


        gameController.ToggleDialogue(true); 
  

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
            audioSource.Play();
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
         gameController.ToggleDialogue(false); 
    }
}
