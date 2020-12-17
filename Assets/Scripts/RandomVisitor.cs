using UnityEngine;

public class RandomVisitor : MonoBehaviour
{
    private float _deltaX;
    private float _deltaZ;
    private bool _move;
    private float _elapsedTime;
    private float _waitTime;
    private float _waitElapsed;
    public float VisitorSpeed;
    public float MoveDuration;

    private void Start()
    {
        _deltaX = Random.Range(-10f, 10f);
        _deltaZ = Random.Range(-10f, 10f);
    }

    private void Update()
    {
        if (_elapsedTime < MoveDuration && _move)
        {
            _elapsedTime += Time.deltaTime;
            transform.position += new Vector3(_deltaX, 0, _deltaZ) * (Time.deltaTime * VisitorSpeed);
        }
        else if (_move)
        {
            _move = false;
            _waitTime = Random.Range(1, 3);
            _waitElapsed = 0;
        }
        else if (!_move)
        {
            if (_waitElapsed < _waitTime)
                _waitElapsed += Time.deltaTime;
            else
            {
                _move = true;
                _waitElapsed = 0;
                _elapsedTime = 0;
                _deltaX = Random.Range(-2.0f, 2.0f);
                _deltaZ = Random.Range(-2.0f, 2.0f);
            }
                            
        }
        
        
    }



}