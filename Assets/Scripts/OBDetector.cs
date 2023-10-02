using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            other.transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.position = GameManager.instance.checkPoint.transform.position;
        }
    }
}
