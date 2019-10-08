using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restart;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _scoreText.text = "Score: " + "0";
        _gameOver.gameObject.SetActive(false);
        _restart.gameObject.SetActive(false);

        if (_gameManager == null)
        {
            Debug.LogError("Game manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int Score)
    {
        _scoreText.text = "Score: " + Score.ToString();
    }

    public void UpdateLives(int CurrentLives)
    {
        _livesImg.sprite = _livesSprites[CurrentLives];
        if (CurrentLives == 0)
        {
            SetGameOver();
        }
    }

    private void SetGameOver()
    {
        _gameOver.gameObject.SetActive(true);
        _restart.gameObject.SetActive(true);
        _gameManager.GameOver();
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOver.text="GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
