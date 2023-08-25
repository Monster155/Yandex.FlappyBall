using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovableBlock : MonoBehaviour
{
    [SerializeField] private AnimationCurve _movementCurve;

    private float _timer;
    private float _startY;
    private float _rangeScaler;

    private void Start()
    {
        _startY = transform.position.y;
        _rangeScaler = Random.Range(1f, 2f);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        Vector3 pos = transform.position;
        pos.y = _startY + _movementCurve.Evaluate(_timer) * _rangeScaler;
        transform.position = pos;
    }
}