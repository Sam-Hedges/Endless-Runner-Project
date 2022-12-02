using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public  bool isGameActive = false;

    public void StartGame() {
        isGameActive = true;
    }
}
