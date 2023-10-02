using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalInterction : MonoBehaviour
{
    public GameObject goalUI;
    public GameObject goalPlayerLocation;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            other.transform.gameObject.GetComponent<PlayerController>().enabled = false;
            other.transform.position = goalPlayerLocation.transform.position;
            other.transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.rotation = goalPlayerLocation.transform.localRotation;
            goalUI.SetActive(true);
        }
    }

}
