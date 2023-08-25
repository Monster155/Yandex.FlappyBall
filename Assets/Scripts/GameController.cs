using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public event Action<bool> OnGameStateChanged;

    [SerializeField] private Rigidbody2D _ballRigidbody;
    [SerializeField] private ParticleSystem _ballParticleSystem;
    [SerializeField] private BallTriggerController _ballTriggerController;
    [SerializeField] private float _forcePower = 10f;
    [Space]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _startText;

    private int _score = 0;
    private bool _isGameStarted;
    public bool IsGameStarted
    {
        get => _isGameStarted;
        private set
        {
            if (_isGameStarted != value)
            {
                OnGameStateChanged?.Invoke(value);
                GameStateChanged(value);
            }
            _isGameStarted = value;
        }
    }

    private void Start()
    {
        IsGameStarted = false;
        GameStateChanged(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            IsGameStarted = true;

            _ballRigidbody.AddForce(new Vector2(0, _forcePower));
        }
    }

    private void GameStateChanged(bool isGameStarted)
    {
        _startText.SetActive(!isGameStarted);
        _ballRigidbody.simulated = isGameStarted;

        if (isGameStarted)
            _ballParticleSystem.Play();
        else
            _ballParticleSystem.Pause();
    }

    public void WallReached()
    {
        SceneManager.LoadScene(0);
    }

    public void PointCollected()
    {
        _score++;
        _scoreText.text = _score.ToString();
    }
}