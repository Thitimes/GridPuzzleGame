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
    private TextMeshProUGUI buttonTxt;

 

    private int clickCount = 0;

    private bool IsStart = false;

    private void Start()
    {
       
    }
    public void OnClick()
    {
        if (clickCount < imageList.Count)
        {
            imageList[clickCount].enabled = true;
            clickCount++;
        }
        if (IsStart == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (clickCount == imageList.Count )
        {
            IsStart = true;
            buttonTxt.text = "Start";
        }
      
    }
}
