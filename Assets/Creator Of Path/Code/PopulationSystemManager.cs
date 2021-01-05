using System;
using UnityEditor;
using UnityEngine;

public class PopulationSystemManager : MonoBehaviour
{
    [SerializeField]
    private GameObject planePrefab;

    [SerializeField]
    private GameObject circlePrefab;

    public GameObject pointPrefab;

    [HideInInspector]
    public bool isConcert;

    [HideInInspector]
    public bool isStreet;

    [HideInInspector]
    public Vector3 mousePos;

    public PopulationSystemManager()
    {
    }

    public void Concert(Vector3 pos)
    {
        this.isConcert = false;
        GameObject gameObject = new GameObject();
        gameObject.transform.position = pos;
        gameObject.name = "Audience";
        //Selection.activeGameObject = gameObject;
        gameObject.AddComponent<StandingPeopleConcert>();
        StandingPeopleConcert component = gameObject.GetComponent<StandingPeopleConcert>();
        component.planePrefab = this.planePrefab;
        component.circlePrefab = this.circlePrefab;
        component.SpawnRectangleSurface();
        //Selection.activeGameObject = gameObject;
        //ActiveEditorTracker.sharedTracker.isLocked = false;
    }

    public void Street(Vector3 pos)
    {
        this.isStreet = false;
        GameObject gameObject = new GameObject();
        gameObject.transform.position = pos;
        gameObject.name = "Talking people";
        //Selection.activeGameObject = gameObject;
        gameObject.AddComponent<StandingPeopleStreet>();
        StandingPeopleStreet component = gameObject.GetComponent<StandingPeopleStreet>();
        component.planePrefab = this.planePrefab;
        component.circlePrefab = this.circlePrefab;
        component.SpawnRectangleSurface();
        //Selection.activeGameObject = gameObject;
        //ActiveEditorTracker.sharedTracker.isLocked = false;
    }
}