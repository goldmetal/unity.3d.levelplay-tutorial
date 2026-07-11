using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField]
    TextMeshProUGUI textScore;
    [SerializeField]
    TextMeshProUGUI textHighScore;    
    [SerializeField]
    TextMeshProUGUI textCountdown;
    [SerializeField]
    TextMeshProUGUI textTime;
    [SerializeField]
    Slider sliderTime;

    [Header("Image")]
    [SerializeField]
    Image fade;
    [SerializeField]
    GameObject fill;

    [Header("Object")]
    [SerializeField]
    GameObject controller;   
    [SerializeField]
    GameObject gameOver;
    [SerializeField]
    GameObject gameOverController;
    [SerializeField]
    GameObject buttonAd;
    [SerializeField]
    AudioManager audioManager;

    WaitForSeconds wait;

    void Awake()
    {
        wait = new WaitForSeconds(2f);
    }

    public void SetTextScore(string value)
    {
        textScore.text = value;
    }
    public void SetTextHighScore(string value)
    {
        textHighScore.text = value;
    }
    public void SetTextCountdown(string value)
    {
        if (!textCountdown.gameObject.activeSelf)
        {
            fade.enabled = true;
            textCountdown.gameObject.SetActive(true);
        }

        textCountdown.text = value;
    }
    public void SetTextTime(string value)
    {
        textTime.text = value;
    }
    public void SetSlider(float value)
    {
        sliderTime.value = value;
    }

    public void SetHUD()
    {
        fade.enabled = false;
        textCountdown.gameObject.SetActive(false);
        textScore.gameObject.SetActive(true);
        textHighScore.gameObject.SetActive(true);
        textTime.gameObject.SetActive(true);
    }

    public void SetGameOver(bool isRewarded)
    {
        StartCoroutine(SetGameOverRoutine(isRewarded));        
    }

    IEnumerator SetGameOverRoutine(bool isRewarded)
    {
        fade.enabled = true;
        fill.SetActive(false);
        controller.SetActive(false);

        yield return wait;
        audioManager.PlaySfx(SFX.GameOver);
        gameOver.SetActive(true);

        yield return wait;
        audioManager.PlaySfx(SFX.GameOverControl);

        if (isRewarded)
            buttonAd.SetActive(false);

        gameOverController.SetActive(true);
    }

    public void SetGameResume()
    {
        fade.enabled = false;
        fill.SetActive(true);
        controller.SetActive(true);
        gameOver.SetActive(false);
        gameOverController.SetActive(false);
    }
}
