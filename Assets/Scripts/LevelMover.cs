using UnityEngine;

public class LevelMover : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    private float _finishPosX;

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
        
        if(transform.position.x < _finishPosX)
            Destroy(gameObject);
    }
    public void Init(float finishPosX)
    {
        _finishPosX = finishPosX;
    }
}