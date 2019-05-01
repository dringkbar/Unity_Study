using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour {
    public static bool isPaused;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    
	// Use this for initialization
	public void Start () {
        isPaused = false;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);

        UpdateQualityLabel();
        UpdateVolumeLable();
	}
	
    public void IncreaseQuality()
    {
        QualitySettings.IncreaseLevel();
        UpdateQualityLabel();   
    }

    public void DecreaseLevel()
    {
        QualitySettings.DecreaseLevel();
        UpdateQualityLabel();
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        UpdateVolumeLable();
    }

    public void UpdateQualityLabel()
    {
        int currentQuality = QualitySettings.GetQualityLevel();
        string qualityName = QualitySettings.names[currentQuality];

        optionsMenu.transform.Find("Quality Level").GetComponent<UnityEngine.UI.Text>().text = "Quality Level - " + qualityName;
    }

    public void UpdateVolumeLable()
    {
        optionsMenu.transform.Find("Master Volume").GetComponent<UnityEngine.UI.Text>().text = "Master Volume - " + (AudioListener.volume * 100).ToString("f2") + "%";
    }

    // Update is called once per frame
    public void Update () {
        if (Input.GetKeyUp("escape"))
        {
            if (!optionsMenu.activeInHierarchy)
            {
                isPaused = !isPaused;
                Time.timeScale = (isPaused) ? 0 : 1;
                pauseMenu.SetActive(isPaused);
            }
            else OpenPauseMenu();
        }
	}

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void OpenPauseMenu()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
