using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossMenu : MonoBehaviour
{
    public void PlayGame()
  {
      SceneManager.LoadScene("SampleScene");
  }

  public void MainMenu()
  {
    SceneManager.LoadScene("Menu");
  }

  
}
