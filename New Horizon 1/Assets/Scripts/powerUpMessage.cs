using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpMessage : MonoBehaviour {

    private void OnDestroy()
    {
        Camera.main.GetComponent<GameManager>().TurnOffMessage();
    }
}
