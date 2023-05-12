using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    private float typingTime = 0.05f;
    public Animator animate;
    
    void Start()
    {
        lineIndex = 0;
        animate = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            dialogueMark.SetActive(true);
            isPlayerInRange = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            dialogueMark.SetActive(false);
            isPlayerInRange = false;
        }
    }

    private void StartDialogue(){
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        StartCoroutine(ShowLine());
    }
    
    private IEnumerator ShowLine(){
        dialogueText.text = string.Empty;
        foreach(char ch in dialogueLines[lineIndex]){
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void NextDialogueLine(){
        if(lineIndex < 2){
            lineIndex++;
        }else{
            lineIndex = -1;
            return;
        }
        if(lineIndex <= dialogueLines.Length){
            StartCoroutine(ShowLine());
        }
    }

    void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.E)){
            animate.SetBool("isTalking", true);
            if(!didDialogueStart){
                StartDialogue();
            }
            else if(dialogueText.text == dialogueLines[lineIndex]){
                NextDialogueLine();
            }
        }else if(!isPlayerInRange || lineIndex == -1){
            animate.SetBool("isTalking", false);
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            lineIndex = 0;
        }
    }
}
