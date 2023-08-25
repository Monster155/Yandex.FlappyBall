using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [Space]
    [SerializeField] private Transform _startPosTop;
    [SerializeField] private Transform _startPosBottom;
    [SerializeField] private Transform _finishPos;
    [Space]
    [SerializeField] private LevelMover _levelMoverPrefab;
    [SerializeField] private MovableBlock _blockPrefab;
    [SerializeField] private float _blockSize = 1f;
    [SerializeField] private Point _pointPrefab;
    [SerializeField] private float _spawnTimeDelayInSeconds = 3f;

    private int _blocksCount;

    private void Start()
    {
        _gameController.OnGameStateChanged += GameController_OnGameStateChanged;
    }

    private void CalculateBlocksCount()
    {
        int rand = Random.Range(0, 10);
        if (rand < 2)
            _blocksCount--;
        else if (rand > 6)
            _blocksCount++;

        if (_blocksCount < 1)
            _blocksCount = 1;
    }

    private void Spawn()
    {
        LevelMover mover = Instantiate(_levelMoverPrefab, Vector3.zero, Quaternion.identity, transform);
        mover.Init(_finishPos.position.x - _blocksCount * _blockSize);
        Transform container = mover.transform;

        for (int i = 0; i < _blocksCount; i++)
        {
            float blockPosX = Random.Range(0, _blocksCount * _blockSize);
            float blockPosY = Random.Range(_startPosBottom.position.y, _startPosTop.position.y);
            Instantiate(_blockPrefab, new Vector3(blockPosX, blockPosY, 0), Quaternion.identity, container);
        }

        float pointPosX = Random.Range(0, _blocksCount * _blockSize);
        float pointPosY = Random.Range(_startPosBottom.position.y, _startPosTop.position.y);
        Instantiate(_pointPrefab, new Vector3(pointPosX, pointPosY, 0), Quaternion.identity, container);

        container.position = new Vector3(_startPosTop.position.x, 0, 0);
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            CalculateBlocksCount();
            Spawn();
            yield return new WaitForSeconds(_spawnTimeDelayInSeconds);
        }
    }

    private void GameController_OnGameStateChanged(bool isGameStarted)
    {
        if (isGameStarted)
            StartCoroutine(SpawnCoroutine());
    }
}