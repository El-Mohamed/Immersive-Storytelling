using System;
using System.Collections.Generic;
using UnityEngine;

public class StandingPeopleConcert : MonoBehaviour
{
    [HideInInspector]
    public GameObject planePrefab;

    [HideInInspector]
    public GameObject circlePrefab;

    [HideInInspector]
    public GameObject surface;

    [HideInInspector]
    public Vector2 planeSize = new Vector2(1f, 1f);

    public GameObject[] peoplePrefabs = new GameObject[0];

    [HideInInspector]
    private List<Vector3> spawnPoints = new List<Vector3>();

    [HideInInspector]
    public GameObject target;

    [HideInInspector]
    public int peopleCount;

    [HideInInspector]
    public bool isCircle;

    [HideInInspector]
    public float circleDiametr = 1f;

    [HideInInspector]
    public bool showSurface = true;

    public StandingPeopleConcert.TestEnum SurfaceType;

    [HideInInspector]
    public GameObject par;

    public StandingPeopleConcert()
    {
    }

    private Vector2 GetRealPeopleModelSize()
    {
        Bounds component = this.peoplePrefabs[1].GetComponent<MeshRenderer>().bounds;
        Vector3 vector3 = component.size;
        return new Vector2(vector3.x, vector3.z);
    }

    private Vector2 GetRealPlaneSize()
    {
        Vector3 component = this.surface.GetComponent<MeshRenderer>().bounds.size;
        return new Vector2(component.x, component.z);
    }

    private bool IsRandomPositionFree(Vector3 pos)
    {
        for (int i = 0; i < this.spawnPoints.Count; i++)
        {
            if (this.spawnPoints[i].x - 0.6f < pos.x && this.spawnPoints[i].x + 1f > pos.x && this.spawnPoints[i].z - 0.5f < pos.z && this.spawnPoints[i].z + 0.6f > pos.z)
            {
                return false;
            }
        }
        return true;
    }

    public void OnDrawGizmos()
    {
        if (this.isCircle)
        {
            this.surface.transform.localScale = new Vector3(this.circleDiametr, this.circleDiametr, 1f);
            return;
        }
        this.surface.transform.localScale = new Vector3(this.planeSize.x, this.planeSize.y, 1f);
    }

    public void PopulateButton()
    {
        this.RemoveButton();
        GameObject gameObject = new GameObject();
        this.par = gameObject;
        gameObject.transform.parent = base.gameObject.transform;
        gameObject.name = "people";
        this.spawnPoints.Clear();
        this.SpawnPeople(this.peopleCount);
    }

    private Vector3 RandomCirclePosition()
    {
        Vector3 vector3 = new Vector3();
        Vector3 vector31 = this.surface.transform.position;
        float realPlaneSize = this.GetRealPlaneSize().x / 2f;
        for (int i = 0; i < 10; i++)
        {
            float single = UnityEngine.Random.@value * realPlaneSize;
            float single1 = UnityEngine.Random.@value * 360f;
            vector3.x = vector31.x + single * Mathf.Sin(single1 * 0.0174532924f);
            vector3.y = vector31.y;
            vector3.z = vector31.z + single * Mathf.Cos(single1 * 0.0174532924f);
            if (this.IsRandomPositionFree(vector3))
            {
                return vector3;
            }
        }
        return Vector3.zero;
    }

    private Vector3 RandomRectanglePosition()
    {
        Vector3 vector3 = new Vector3(0f, 0f, 0f);
        for (int i = 0; i < 10; i++)
        {
            vector3.x = this.surface.transform.position.x - this.GetRealPlaneSize().x / 2f + 0.3f + UnityEngine.Random.Range(0f, this.GetRealPlaneSize().x - 0.6f);
            vector3.z = this.surface.transform.position.z - this.GetRealPlaneSize().y / 2f + 0.3f + UnityEngine.Random.Range(0f, this.GetRealPlaneSize().y - 0.6f);
            vector3.y = this.surface.transform.position.y;
            if (this.IsRandomPositionFree(vector3))
            {
                return vector3;
            }
        }
        return Vector3.zero;
    }

    public void RemoveButton()
    {
        if (this.par != null)
        {
            UnityEngine.Object.DestroyImmediate(this.par);
        }
    }

    public void SpawnCircleSurface()
    {
        if (this.surface != null)
        {
            UnityEngine.Object.DestroyImmediate(this.surface);
        }
        GameObject vector3 = UnityEngine.Object.Instantiate(this.circlePrefab, base.transform.position, Quaternion.identity) as GameObject;
        this.isCircle = true;
        vector3.transform.eulerAngles = new Vector3(vector3.transform.eulerAngles.x - 90f, vector3.transform.eulerAngles.y, vector3.transform.eulerAngles.z);
        Transform transforms = vector3.transform;
        transforms.position = transforms.position + new Vector3(0f, 0.01f, 0f);
        vector3.transform.parent = base.transform;
        vector3.name = "surface";
        this.surface = vector3;
    }

    private void SpawnPeople(int _peopleCount)
    {
        Vector3 vector3;
        RaycastHit raycastHit;
        for (int i = 0; i < _peopleCount; i++)
        {
            int num = UnityEngine.Random.Range(0, (int)this.peoplePrefabs.Length);
            vector3 = (this.isCircle ? this.RandomCirclePosition() : this.RandomRectanglePosition());
            if (vector3 != Vector3.zero)
            {
                GameObject gameObject = UnityEngine.Object.Instantiate(this.peoplePrefabs[num], vector3, Quaternion.identity) as GameObject;
                if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), -gameObject.transform.up, out raycastHit))
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, raycastHit.point.y, gameObject.transform.position.z);
                }
                gameObject.AddComponent<PeopleController>();
                this.spawnPoints.Add(gameObject.transform.position);
                if (this.target == null)
                {
                    gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.rotation.x, base.transform.rotation.y, gameObject.transform.rotation.z);
                }
                else
                {
                    gameObject.GetComponent<PeopleController>().SetTarget(this.target.transform.position);
                }
                PeopleController component = gameObject.GetComponent<PeopleController>();
                string[] strArrays = new string[] { "idle1", "idle2", "cheer", "claphands" };
                component.animNames = strArrays;
                gameObject.transform.parent = this.par.transform;
            }
        }
    }

    public void SpawnRectangleSurface()
    {
        if (this.surface != null)
        {
            UnityEngine.Object.DestroyImmediate(this.surface);
        }
        GameObject vector3 = UnityEngine.Object.Instantiate(this.planePrefab, base.transform.position, Quaternion.identity) as GameObject;
        this.surface = vector3;
        this.isCircle = false;
        vector3.transform.eulerAngles = new Vector3(vector3.transform.eulerAngles.x - 90f, vector3.transform.eulerAngles.y, vector3.transform.eulerAngles.z);
        Transform transforms = vector3.transform;
        transforms.position = transforms.position + new Vector3(0f, 0.01f, 0f);
        vector3.transform.parent = base.transform;
        vector3.name = "surface";
    }

    public enum TestEnum
    {
        Rectangle,
        Circle
    }
}