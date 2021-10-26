using System;
using System.Collections;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    private float tParam;
    private Vector2 position;
    public float speed;
    private bool contunue;

    private void Start()
    {
        tParam = 0f;
        speed = 0.5f;
        contunue = true;
    }

    private void Update()
    {
        if(contunue)
            StartCoroutine(GoByRoute());
    }

    private IEnumerator GoByRoute()
    {
        contunue = false;
        while (tParam< 1)
        {
            tParam += Time.deltaTime * speed;
            Vector2 p0 = routes[0].GetChild(0).position;
            Vector2 p1 = routes[0].GetChild(1).position;
            Vector2 p2 = routes[0].GetChild(2).position;
            Vector2 p3 = routes[0].GetChild(3).position;
            position = Mathf.Pow(1 - tParam, 3) * p0 +
                       3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                       3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                       Mathf.Pow(tParam, 3) * p3;

            transform.position = position;
            yield return new WaitForEndOfFrame();
        }
    }    
}