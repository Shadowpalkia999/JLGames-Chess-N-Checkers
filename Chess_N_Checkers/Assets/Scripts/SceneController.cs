using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject creditsPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        creditsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnChessButtonClick()
    {
        SceneManager.LoadScene("ChessScene");
    }
    public void OnCheckersButtonClick()
    {
        SceneManager.LoadScene("CheckersScene");
    }
    public void OnCreditsButtonClick()
    {
        creditsPanel.SetActive(true);
    }
    public void OnBackButtonClick()
    {
        creditsPanel.SetActive(false);
    }
    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}