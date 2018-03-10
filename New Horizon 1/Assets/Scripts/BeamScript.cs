using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamScript : MonoBehaviour
{
    [SerializeField]
    Cytoblast cytoBeam;

    GameObject playerTransform;
    // Use this for initialization
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("cytoMount");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            // UNCOMMENT THE LINES BELOW TO FIRE CYTOBEAM
            //if (GameObject.FindGameObjectWithTag("cyto") != null) { Destroy(GameObject.FindGameObjectWithTag("cyto")); }

            //Vector2 direction = (Vector2)transform.position - (Vector2)GameObject.FindGameObjectWithTag("Player").transform.position;
            //RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 100f);
            //if (hit.collider != null)
            //{
            //    GameObject beam = Instantiate(cytoBeam);
            //    Vector2 endpoint = Vector2.zero;
            //    LineRenderer line = beam.GetComponent<LineRenderer>();
            //    Origin of the line
            //    line.SetPosition(0, transform.position);
            //    The endpoint of the line
            //    line.SetPosition(1, ((Vector2)transform.position + (direction * Vector2.Distance((Vector2)transform.position, (Vector2)hit.point))));
            //    line.enabled = true;
            //}


            // UNCOMMENT THE LINES BELOW TO FIRE CYTOBLASTVe
            
        }
    }
}






