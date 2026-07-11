using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game")]
    [Range(30, 120)]
    public int frameCount;
    public bool isPlaying;
    public bool isRewarded;

    [Header("Score")]
    public int score;
    public int highScore;
    public int answerScore;
    [Header("Time")]
    public float playTime;
    public float maxPlayTime;
    public float answerTime;

    [Header("Object")]
    [SerializeField]
    InputManager inputManager;
    [SerializeField]
    UIManager uiManager;
    [SerializeField]
    AudioManager audioManager;
    [SerializeField]
    Scroller scroller;
    [SerializeField]
    ParticleSystem particle;
    [SerializeField]
    CinemachineImpulseSource impulse;

    WaitForSeconds wait;
    

    void Awake()
    {
        Application.targetFrameRate = frameCount;
        highScore = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;
        wait = new WaitForSeconds(1f);
        playTime = maxPlayTime;
        inputManager.OnInput += OnUserInput;
    }

    void Start()
    {
        scroller.InitBlocks();
        StartCoroutine(CountdownRoutine());
    }

    void Update()
    {
        if (!isPlaying)
            return;

        playTime -= Time.deltaTime;
        uiManager.SetTextTime(Mathf.CeilToInt(playTime).ToString());
        uiManager.SetSlider(playTime / maxPlayTime);

        if (playTime < 0)
        {
            playTime = 0;
            isPlaying = false;
            GameOver();
        }
    }

    IEnumerator CountdownRoutine()
    {
        audioManager.PlaySfx(SFX.Countdown);
        uiManager.SetTextCountdown("3");
        yield return wait;
        uiManager.SetTextCountdown("2");
        yield return wait;
        uiManager.SetTextCountdown("1");
        yield return wait;
        uiManager.SetTextCountdown("시작!");
        yield return wait;
        uiManager.SetHUD();
        uiManager.SetTextScore(score.ToString());
        uiManager.SetTextHighScore(highScore.ToString());
        audioManager.PlayBgm();
        isPlaying = true;        
    }

    void OnUserInput(int userID)
    {
        if (!isPlaying)
            return;

        Answer(userID);
    }

    public void Answer(int userID)
    {
        Block block = scroller.GetBlock();
        bool isSuccess = block.answerID == userID;

        if (isSuccess)
        {
            block.Success();
            particle.Play();
            scroller.Scroll();
            AddScore();
        }
        else
        {
            audioManager.PlaySfx(SFX.Fail);
            impulse.GenerateImpulse();
            block.Fail();
        }
    }

    public void AddScore()
    {
        playTime = Mathf.Min(playTime + 0.3f, maxPlayTime);
        score += 50;
        uiManager.SetTextScore(score.ToString());

        if (score > highScore)
        {
            highScore = score;
            uiManager.SetTextHighScore(highScore.ToString());
        }           
    }

    void GameOver()
    {
        audioManager.PauseBgm();
        uiManager.SetGameOver(isRewarded);
        PlayerPrefs.SetInt("HighScore", highScore);
    }
    
    public void GameRestart()
    {
        SceneManager.LoadScene(0);
    }

    public void GameRewarded()
    {
        playTime = 6f;
        audioManager.PlaySfx(SFX.Reward);
        audioManager.PlayBgm();
        uiManager.SetGameResume();
        isPlaying = true;
        isRewarded = true;
    }
}
