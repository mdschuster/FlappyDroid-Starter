using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{

    private float despawnPosition = -20f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= despawnPosition)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (GameManager.instance().moving == false) return;
        Vector3 pos = this.transform.position;
        pos.x -= 0.1f;
        this.transform.position = pos;
    }
}
