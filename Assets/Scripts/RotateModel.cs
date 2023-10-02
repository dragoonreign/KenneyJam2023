using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public GameObject m_PlayerController;
    [SerializeField] private float _turnSpeed = 360;

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    private void Look() {
        if (m_PlayerController.GetComponent<PlayerController>()._input == Vector3.zero) return;

        var rot = Quaternion.LookRotation(m_PlayerController.GetComponent<PlayerController>()._input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
    }
}
