using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehindMesh : MonoBehaviour
{
    public Camera cam;
    public GameObject playerFeet;
    MeshRenderer hitObject;
    List<string> hitObjectsName = new List<string>();

    RaycastHit hit;
    RaycastHit[] hits;
    Dictionary<string, RaycastHit> disableMeshDictionary = new Dictionary<string, RaycastHit>();
    Dictionary<string, RaycastHit> enableMeshDictionary = new Dictionary<string, RaycastHit>();

    Color tempcolor;
    Color tempcolorNormal;

    void Update()
    {
        float rayDistance = (playerFeet.transform.position - cam.transform.position).magnitude - .5f;
        hits = Physics.RaycastAll(cam.transform.position, cam.transform.forward, rayDistance);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance);

        UpdateRayHits();
    }

    void UpdateRayHits()
    {
        DoCheckDict();
        
        // if (hits.Length == 0)
        // {
        //     //make all mesh visible
        //     OnRayNotHittingTransform();
        //     ResetDictionary();
        // }

        if (hits.Length > 0)
        {
            //make some mesh invisible
            OnRayHitTransform();
        }
    }

    void DoCheckDict()
    {
        if (enableMeshDictionary.Count == 0) return;
        
        for (int i = 0; i < hits.Length; i++)
        {
            hit = hits[i];

            if (enableMeshDictionary.ContainsKey(hit.transform.gameObject.name))
            {
                enableMeshDictionary.Remove(hit.transform.gameObject.name);
            }
        }
        
        // foreach( string s in enableMeshDictionary.Keys )
        // {
        //     disableMeshDictionary.Remove(s);
        //     // DoEnableMesh(s);
        //     // DoResetColor();
        // }

        //grab all the hits gameobject name
        foreach( RaycastHit v in enableMeshDictionary.Values )
        {
            DoEnableMesh(v.transform);
            // DoResetColor();
        }
    }

    void OnRayHitTransform()
    {
        for (int i = 0; i < hits.Length; i++)
        {
            hit = hits[i];

            if (hit.collider.tag == null) return;

            //check if the ray hit object is not a player and is a block
            if (hit.collider.tag != "Player" && hit.collider.tag == "Block")
            {
                //Update mesh alpha.
                UpdateColor();

                //Disable mesh
                DoDisableMesh(hit.transform);

                //Update mesh alpha.
                // DoFadeColor();

                //what is this line of code?
                hitObject = hit.transform.gameObject.GetComponent<MeshRenderer>();

                //Add new key if dictionary doesnt have it.
                if (!enableMeshDictionary.ContainsKey(hit.transform.gameObject.name))
                {
                    // Debug.Log("Adding new key");

                    // disableMeshDictionary.Add(hit.transform.gameObject.name, hit);
                    enableMeshDictionary.Add(hit.transform.gameObject.name, hit);
                }
            }
        }
    }

    void ResetDictionary()
    {
        disableMeshDictionary.Clear();
        enableMeshDictionary.Clear();
    }

    void OnRayNotHittingTransform()
    {
        foreach( RaycastHit v in disableMeshDictionary.Values )
        {
            DoEnableMesh(v.transform);
        }
    }

    void UpdateColor()
    {
        tempcolor = hit.transform.gameObject.GetComponent<MeshRenderer>().materials[1].color;
        tempcolor.a = .5f;

        tempcolorNormal = hit.transform.GetComponent<MeshRenderer>().materials[1].color;
        tempcolorNormal.a = 1f;
    }

    void DoEnableMesh(Transform transform)
    {
        transform.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    void DoDisableMesh(Transform transform)
    {
        transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    void DoFadeColor()
    {
        hitObject.materials[1].color = tempcolor;
    }

    void DoResetColor()
    {
        hitObject.materials[1].color = tempcolorNormal;
    }
}
