using System.Collections;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [SerializeField] protected Transform[] routes;
    private float _tParam;
    private Vector2 _position;
    public float speed;
    private bool _continue;

    private void Start()
    {
        _tParam = 0f;
        speed = 0.5f;
        _continue = true;
    }

    private void Update()
    {
        if (_continue)
            StartCoroutine(GoByRoute());
    }

    private IEnumerator GoByRoute()
    {
        _continue = false;
        while (_tParam < 1)
        {
            _tParam += Time.deltaTime * speed;
            Vector2 p0 = routes[0].GetChild(0).position;
            Vector2 p1 = routes[0].GetChild(1).position;
            Vector2 p2 = routes[0].GetChild(2).position;
            Vector2 p3 = routes[0].GetChild(3).position;
            _position = Mathf.Pow(1 - _tParam, 3) * p0 +
                        3 * Mathf.Pow(1 - _tParam, 2) * _tParam * p1 +
                        3 * (1 - _tParam) * Mathf.Pow(_tParam, 2) * p2 +
                        Mathf.Pow(_tParam, 3) * p3;

            transform.position = _position;
            yield return new WaitForEndOfFrame();
        }
        Destroy(transform.parent.gameObject);
    }
}