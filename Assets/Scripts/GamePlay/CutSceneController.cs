using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class CutSceneController : MonoBehaviour
{
    [SerializeField]
    private List<Image> imageList = new List<Image>();
    [SerializeField]
    private GameObject button;

    PlayerMovement inputActions;
 

    private int clickCount = 0;


    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Awake()
    {
        inputActions = new PlayerMovement();
        inputActions.UI.Click.performed += ctx => OnClick();
    }
    public void OnClick()
    {
        
        if (clickCount < imageList.Count)
        {
            imageList[clickCount].enabled = true;
           
        }
        clickCount++;
        if (clickCount == imageList.Count + 1 )
        {
            button.SetActive(true);
        }
         

    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
