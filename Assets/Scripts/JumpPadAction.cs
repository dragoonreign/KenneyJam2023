using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadAction : MonoBehaviour
{
    public float padForce = 10f;

    private void OnCollisionEnter(Collision other) {
        if (other.transform.gameObject.name == "Player")
        {
            other.transform.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * padForce, ForceMode.VelocityChange);
        }
    }
}
