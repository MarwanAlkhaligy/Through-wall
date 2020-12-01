using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIHandler : MonoBehaviour
{
    internal static GameUIHandler instance = null;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private GameObject gamePlayUI = null;
    [SerializeField] private GameObject gameMainMenu = null;
    [SerializeField] private GameObject gameOverUI = null;
    [SerializeField] private TextMeshProUGUI highScoreText = null;
    [SerializeField] private AudioSource audio = null;
    [SerializeField] private TextMeshProUGUI musicTMpro = null;
    internal int gameScore = 0;
    private void Awake() 
    {
        if(instance == null) {
            instance = this;
        }else if(instance != this) {
            Destroy(gameObject);
        }
        if(!PlayerPrefs.HasKey("HighScore")) {
            PlayerPrefs.SetInt("Music", 1);
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }
    private void Start()
    {
        if(PlayerPrefs.GetInt("Music") == 1) {
            audio.enabled = true;
        } else if(PlayerPrefs.GetInt("Music") == 0){
            audio.enabled = false;
        }
        highScoreText.SetText( "HighScore: "+ PlayerPrefs.GetInt("HighScore").ToString());
    }
    private void Update() 
    {
        scoreText.SetText(gameScore.ToString());
    }
    public void PLayButton()
    {
        gameMainMenu.SetActive(false);
        gameOverUI.SetActive(false);
        gamePlayUI.SetActive(true);
        PlayerController.instance.isGameStarted = true;
        PlayerController.instance.animator.SetTrigger("Run");
    }
    public void RetryButton() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MusicButton()
    {
        if(PlayerPrefs.GetInt("Music") == 1){
            PlayerPrefs.SetInt("Music", 0);
            audio.enabled = false;
            musicTMpro.SetText("Music-Off");
        } else {
            PlayerPrefs.SetInt("Music", 1);
            audio.enabled = true;
            musicTMpro.SetText("Music-On");
        }
    }
    internal void GameOver()
    {
        gamePlayUI.SetActive(false);
        gameOverUI.SetActive(true);
        PlayerController.direction = Direction.None;
        if(gameScore > PlayerPrefs.GetInt("HighScore")){
            PlayerPrefs.SetInt("HighScore", gameScore);
        }
    }
}
