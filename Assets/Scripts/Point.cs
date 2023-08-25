using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private string _ballTag = "Ball";
    [SerializeField] private float _speed = 0.1f;

    private Transform _ballTransform;

    private void Update()
    {
        if (_ballTransform != null)
        {
            Vector3 direction = _ballTransform.position - transform.position;
            _rigidbody.AddRelativeForce(direction.normalized * _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag.Equals(_ballTag))
        {
            _ballTransform = col.transform;
        }
    }
}