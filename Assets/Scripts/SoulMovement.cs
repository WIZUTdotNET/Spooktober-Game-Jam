using UnityEngine;

public class SoulMovement : BatMovement
{
    [SerializeField] private Sprite[] _sprites;
    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = _sprites[Random.Range(0, _sprites.Length)];
        
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        routes[0].GetChild(0).transform.position = new Vector2(-edgeVector.x * .76f, -edgeVector.y * .2f);
        routes[0].GetChild(1).transform.position = new Vector2(-edgeVector.x * .76f, edgeVector.y);

        routes[0].GetChild(2).transform.position = new Vector2(Random.Range(-edgeVector.x, edgeVector.x), Random.Range(0, edgeVector.y + 1));
        routes[0].GetChild(3).transform.position = new Vector2(Random.Range(-edgeVector.x, edgeVector.x), edgeVector.y + 1);
    }
}
