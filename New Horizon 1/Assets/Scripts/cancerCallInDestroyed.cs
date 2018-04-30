using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cancerCallInDestroyed : MonoBehaviour {

    void OnDestroy()
    {
        Camera.main.GetComponent<GameManager>().DecrementEnemyCount();
    }
}
