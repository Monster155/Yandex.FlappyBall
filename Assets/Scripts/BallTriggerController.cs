using System;
using UnityEngine;

public class BallTriggerController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private string _wallTag = "Block";
    [SerializeField] private string _pointTag = "Point";

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag.Equals(_wallTag))
        {
            _gameController.WallReached();
        }
        else if (col.transform.tag.Equals(_pointTag))
        {
            Destroy(col.gameObject);
            _gameController.PointCollected();
        }
    }
}