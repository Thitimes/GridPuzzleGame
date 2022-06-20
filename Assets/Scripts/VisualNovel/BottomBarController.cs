using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BottomBarController : MonoBehaviour
{
    public TextMeshProUGUI barText;
    public StoryScene currentScene;
    public GameObject currentImage;

    private int sentenceIndex = -1;
    private int spriteIndex = -1;
    private State state = State.COMPLETED;

    private Animator animator;
    private bool isHidden = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        ClearText();
    }

    public void Hide()
    {
        if (isHidden == false)
        {
            animator.SetTrigger("Hide");
            isHidden = true;
        }
    }
    public void Show()
    {
        if (isHidden == true)
        {
            animator.SetTrigger("Show");
            isHidden = false;
        }
    }
    public void ClearText()
    {
        barText.text = "";
    }


    private enum State
    {
            PLAYING,COMPLETED
    }
   public void PlayScene(StoryScene scene)
    {
        currentScene = scene;
        sentenceIndex = -1;
        spriteIndex = -1;
    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }
    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public void PlayNextSentence()
    {
        if (!IsLastSentence())
        {
            StartCoroutine(TypeText(currentScene.sentences[++sentenceIndex].text));
        }
    }

   IEnumerator TypeText(string text)
    {
        barText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;
        currentImage.GetComponent<Image>().sprite = currentScene.sentences[++spriteIndex].currentSprite;
        while(state != State.COMPLETED)
        {
            barText.text += text[wordIndex];
            yield return new WaitForSeconds(0.005f);
            if(++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }


    }
}
