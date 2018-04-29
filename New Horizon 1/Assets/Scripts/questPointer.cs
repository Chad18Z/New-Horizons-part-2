using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questPointer : MonoBehaviour {

    Quaternion rot;
    Collider2D coll;
    [SerializeField]
    GameObject[] trigs;

    Queue triggers = new Queue();

    // Use this for initialization
    void Start()
    {

        coll = gameObject.GetComponent<Collider2D>();

        for (int i = 0; i < trigs.Length; i++)
        {
            triggers.Enqueue(trigs[i]);
        }
    }

    //
    void FixedUpdate()
    {
        if (triggers.Count > 0)
        {
            GameObject temp = (GameObject)triggers.Peek();
            Vector3 diff = transform.position - temp.transform.position;
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            rot = Quaternion.Euler(0f, 0f, rot_z + 90);
            transform.rotation = rot;
        }
    } 

    public void PopTrigger()
    {
        Debug.Log("Hit trigger");
        triggers.Dequeue();
    }

}
