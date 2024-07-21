using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMesh;

    [SerializeField]
    private TextMeshProUGUI _textGameOver;

    [SerializeField]
    private Image _liveImg;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private TextMeshProUGUI _textRestart;


    private SpawnManager _spawnManager;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _textRestart.gameObject.SetActive(false);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _textGameOver.gameObject.SetActive(false);
        _textMesh.text = "Score: " + 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowGameOver(int ScoreValue)
    {
        _gameManager.GameOver();
        _textGameOver.gameObject.SetActive(true);
        _textRestart.gameObject.SetActive(true);
        _textGameOver.text = "Game is Over!!!" + "\n" + "Your Score is: " + ScoreValue;
        StartCoroutine(FlickerTest(_spawnManager.SpawningStatus()));

    }


    public void ChangeScoreText(int scoreValue)
    {
        _textMesh.text = "Score:" + scoreValue;
    }


    public void ChangeDamageImg(int damageImg)
    {
        _liveImg.sprite = _liveSprites[damageImg];
    }

    IEnumerator FlickerTest(bool continueSpawning)
    {
        Debug.Log(continueSpawning);
        while (!continueSpawning)
        {
            _textGameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _textGameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
