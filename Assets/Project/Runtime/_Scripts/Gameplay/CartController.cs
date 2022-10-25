using Project.Runtime._Scripts.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineDollyCart))]
public class CartController : MonoBehaviour
{
    [SerializeField] private TileSpawner tileSpawner;
    [SerializeField][Range(0f, 0.1f)] private float laneRange = 0.1f;
    [HideInInspector] public Tile currentTile;
    private float speed;
    private Coroutine deleteTile;
    [HideInInspector] public CinemachineDollyCart cart;
    private List<GameObject> tiles;
    private bool isMoving = false;
    [SerializeField][Range(0, 3)] public int currentLane = 0;
    private float tileDistance;

    private Vector2 movementInputVector;
    private bool isMovementPressed;

    private void Start()
    {
        cart = GetComponent<CinemachineDollyCart>();
        speed = cart.m_Speed;
        tiles = tileSpawner.GetTiles();
        isMoving = true;
        currentTile = tiles[0].GetComponent<Tile>();
        tileDistance = currentTile.lanes[currentLane].PathLength;
    }

    private void Update()
    {
        if (IsMoving()) return;

        SetCurrentPath(cart.m_Path, currentTile.lanes[currentLane]);
        
        if (CheckWithinRange(cart.m_Position, tileDistance, laneRange))
        {
            deleteTile = StartCoroutine(DeleteTile(tiles[0]));
            tiles.RemoveAt(0);

            currentTile = tiles[0].GetComponent<Tile>();
            CinemachinePath newLane = currentTile.lanes[currentLane];

            cart.m_Path = newLane;
            tileDistance = newLane.PathLength;
            cart.m_Position = 0;
        }
    }
    
    public void OnMovementInput(InputAction.CallbackContext context)
    {
        movementInputVector = context.ReadValue<Vector2>();
        isMovementPressed = movementInputVector != Vector2.zero;

        if (isMovementPressed) {

            if (movementInputVector.x < 0f) {
                currentLane = IncrementCurrentLane(-1);
            }
            else if (movementInputVector.x > 0f) {
                currentLane = IncrementCurrentLane(1);
            }
            
        }
    }

    private int IncrementCurrentLane(int value) {

        int updatedLane = currentLane + value;

        if (updatedLane < 0) {
            updatedLane = 0;
        }
        else if (updatedLane > 3) {
            updatedLane = 3;
        }

        return updatedLane;

    }
    
    private void SetCurrentPath(CinemachinePathBase cartPath, CinemachinePathBase currentLane)
    {
        if (cartPath != currentLane) { cart.m_Path = currentLane; }
        return;
    }

    public bool IsMoving()
    {
        if (!isMoving)
        {
            cart.m_Speed = 0f;
            return true;
        }
        return false;
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
