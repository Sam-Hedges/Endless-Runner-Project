using Project.Runtime._Scripts.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineDollyCart))]
public class CartController : MonoBehaviour
{
    [SerializeField] private TileSpawner tileSpawner;
    [SerializeField][Range(0f, 0.1f)] private float laneRange = 0.1f;
    private float speed;
    private Coroutine deleteTile;
    private CinemachineDollyCart cart;
    private List<GameObject> tiles;
    private bool isMoving = false;
    [SerializeField][Range(0, 3)] private int currentLane = 0;
    private float tileDistance;


    private void Start()
    {
        cart = GetComponent<CinemachineDollyCart>();
        speed = cart.m_Speed;
        tiles = tileSpawner.GetTiles();
        isMoving = true;
        tileDistance = tiles[0].GetComponent<Tile>().lanes[currentLane].PathLength;
    }

    private void Update()
    {
        if (!isMoving)
        {
            cart.m_Speed = 0f;
            return;
        }

        Debug.Log(cart.m_Position);
        Debug.Log(tileDistance);
        if (CheckWithinRange(cart.m_Position, tileDistance, laneRange))
        {
            deleteTile = StartCoroutine(DeleteTile(tiles[0]));
            tiles.RemoveAt(0);

            CinemachinePath newLane = tiles[0].GetComponent<Tile>().lanes[currentLane];

            cart.m_Path = newLane;
            tileDistance = newLane.PathLength;
            cart.m_Position = 0;
        }
    }

    private IEnumerator DeleteTile(GameObject obj)
    {
        yield return new WaitForSeconds(5);

        Destroy(obj);

        yield break;
    }

    private bool CheckWithinRange(float x, float y, float range)
    {
        return Mathf.Abs(x - y) <= range;
    }

}
