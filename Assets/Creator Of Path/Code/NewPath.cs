//using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewPath : MonoBehaviour
{
    private List<Vector3> points = new List<Vector3>();

    public int pointLenght;

    public Vector3 mousePos;

    public string pathName;

    public bool errors;

    public bool exit;

    public GameObject par;

    public NewPath()
    {
    }

    public void OnDrawGizmos()
    {
        //Selection.activeGameObject = base.gameObject;
        //ActiveEditorTracker.sharedTracker.isLocked = true;
        Gizmos.color = Color.green;
        if (this.pointLenght > 0 && !this.exit)
        {
            Gizmos.DrawLine(this.points[this.pointLenght - 1], this.mousePos);
        }
        if (this.pointLenght > 1)
        {
            for (int i = 0; i < this.pointLenght - 1; i++)
            {
                Gizmos.DrawLine(this.points[i], this.points[i + 1]);
            }
        }
    }

    public void PointSet(int index, Vector3 pos)
    {
        this.points.Add(pos);
        if (this.par == null)
        {
            this.par = new GameObject()
            {
                name = "New path points"
            };
            this.par.transform.parent = base.gameObject.transform;
        }
        GameObject component = GameObject.Find("Population System").GetComponent<PopulationSystemManager>().pointPrefab;
        GameObject gameObject = Object.Instantiate(component, pos, Quaternion.identity) as GameObject;
        gameObject.name = string.Concat("p", index);
        gameObject.transform.parent = this.par.transform;
    }

    public List<Vector3> PointsGet()
    {
        return this.points;
    }
}