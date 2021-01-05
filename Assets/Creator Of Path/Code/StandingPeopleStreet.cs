//using System;
using System.Collections.Generic;
using UnityEngine;

public class StandingPeopleStreet : MonoBehaviour
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
    public List<Vector3> spawnPoints = new List<Vector3>();

    [HideInInspector]
    public int peopleCount;

    [HideInInspector]
    public bool isCircle;

    [HideInInspector]
    public float circleDiametr = 1f;

    [HideInInspector]
    public bool showSurface = true;

    public StandingPeopleStreet.TestEnum SurfaceType;

    [HideInInspector]
    public GameObject par;

    public StandingPeopleStreet()
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

    private bool IsRandomPositionFree(Vector3 pos, Vector3 helpPoint1, Vector3 helpPoint2)
    {
        for (int i = 0; i < this.spawnPoints.Count; i++)
        {
            if (this.spawnPoints[i].x - 0.5f < pos.x && this.spawnPoints[i].x + 0.5f > pos.x && this.spawnPoints[i].z - 0.5f < pos.z && this.spawnPoints[i].z + 0.5f > pos.z)
            {
                return false;
            }
        }
        if (helpPoint1 != Vector3.zero)
        {
            if (helpPoint1.x - 0.6f < pos.x && helpPoint1.x + 0.6f > pos.x && helpPoint1.z - 0.6f < pos.z && helpPoint1.z + 0.6f > pos.z)
            {
                return false;
            }
            if (!this.isCircle)
            {
                if (helpPoint1.x + 0.3f <= this.surface.transform.position.x - this.GetRealPlaneSize().x / 2f && helpPoint1.x - 0.3f >= this.surface.transform.position.x + this.GetRealPlaneSize().x / 2f && helpPoint1.z + 0.3f <= this.surface.transform.position.z - this.GetRealPlaneSize().y / 2f && helpPoint1.z - 0.3f >= this.surface.transform.position.z + this.GetRealPlaneSize().y / 2f)
                {
                    return false;
                }
            }
            else if (Vector3.Distance(helpPoint1, this.surface.transform.position) >= this.GetRealPlaneSize().x / 2f - 0.3f)
            {
                return false;
            }
        }
        if (helpPoint2 != Vector3.zero)
        {
            if (helpPoint2.x - 0.6f < pos.x && helpPoint2.x + 0.6f > pos.x && helpPoint2.z - 0.6f < pos.z && helpPoint2.z + 0.6f > pos.z)
            {
                return false;
            }
            if (!this.isCircle)
            {
                if (helpPoint2.x + 0.3f <= this.surface.transform.position.x - this.GetRealPlaneSize().x / 2f && helpPoint2.x - 0.3f >= this.surface.transform.position.x + this.GetRealPlaneSize().x / 2f && helpPoint2.z + 0.3f <= this.surface.transform.position.z - this.GetRealPlaneSize().y / 2f && helpPoint2.z - 0.3f >= this.surface.transform.position.z + this.GetRealPlaneSize().y / 2f)
                {
                    return false;
                }
            }
            else if (Vector3.Distance(helpPoint2, this.surface.transform.position) >= this.GetRealPlaneSize().x / 2f - 0.3f)
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
            float single = Random.@value * realPlaneSize;
            float single1 = Random.@value * 360f;
            vector3.x = vector31.x + single * Mathf.Sin(single1 * 0.0174532924f);
            vector3.y = vector31.y;
            vector3.z = vector31.z + single * Mathf.Cos(single1 * 0.0174532924f);
            if (Vector3.Distance(vector3, vector31) < this.GetRealPlaneSize().x / 2f - 0.3f && this.IsRandomPositionFree(vector3, Vector3.zero, Vector3.zero))
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
            vector3.x = this.surface.transform.position.x - this.GetRealPlaneSize().x / 2f + 0.3f + Random.Range(0f, this.GetRealPlaneSize().x - 0.6f);
            vector3.z = this.surface.transform.position.z - this.GetRealPlaneSize().y / 2f + 0.3f + Random.Range(0f, this.GetRealPlaneSize().y - 0.6f);
            vector3.y = this.surface.transform.position.y;
            if (this.IsRandomPositionFree(vector3, Vector3.zero, Vector3.zero))
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
            Object.DestroyImmediate(this.par);
        }
        this.par = null;
    }

    public void SpawnCircleSurface()
    {
        if (this.surface != null)
        {
            Object.DestroyImmediate(this.surface);
        }
        GameObject vector3 = Object.Instantiate(this.circlePrefab, base.transform.position, Quaternion.identity) as GameObject;
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
        Vector3 vector31;
        RaycastHit raycastHit1;
        RaycastHit raycastHit2;
        Vector3 vector32;
        RaycastHit raycastHit3;
        RaycastHit raycastHit4;
        RaycastHit raycastHit5;
        int num = Random.Range(0, _peopleCount / 3) * 3;
        int num1 = Random.Range(0, (_peopleCount - num) / 2) * 2;
        int num2 = _peopleCount - num - num1;
        for (int i = 0; i < num2; i++)
        {
            int num3 = Random.Range(0, (int)this.peoplePrefabs.Length);
            vector3 = (this.isCircle ? this.RandomCirclePosition() : this.RandomRectanglePosition());
            if (vector3 != Vector3.zero)
            {
                GameObject gameObject = Object.Instantiate(this.peoplePrefabs[num3], vector3, Quaternion.identity) as GameObject;
                if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), -gameObject.transform.up, out raycastHit))
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, raycastHit.point.y, gameObject.transform.position.z);
                }
                gameObject.AddComponent<PeopleController>();
                this.spawnPoints.Add(gameObject.transform.position);
                gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.rotation.x, (float)Random.Range(1, 359), gameObject.transform.rotation.z);
                gameObject.GetComponent<PeopleController>().animNames = new string[] { "idle1", "idle2" };
                gameObject.transform.parent = this.par.transform;
            }
        }
        for (int j = 0; j < num1 / 2; j++)
        {
            vector31 = (this.isCircle ? this.RandomCirclePosition() : this.RandomRectanglePosition());
            if (vector31 != Vector3.zero)
            {
                Vector3 vector33 = Vector3.zero;
                Vector3 vector34 = Vector3.zero;
                int num4 = 0;
                while (num4 < 100)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        vector33 = vector31 + new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f));
                        if (this.IsRandomPositionFree(vector33, Vector3.zero, Vector3.zero))
                        {
                            break;
                        }
                        vector33 = Vector3.zero;
                    }
                    for (int l = 0; l < 10; l++)
                    {
                        vector34 = vector31 + new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f));
                        if (this.IsRandomPositionFree(vector34, vector33, Vector3.zero))
                        {
                            break;
                        }
                        vector34 = Vector3.zero;
                    }
                    if (!(vector33 != Vector3.zero) || !(vector34 != Vector3.zero))
                    {
                        vector33 = Vector3.zero;
                        vector34 = Vector3.zero;
                        num4++;
                    }
                    else
                    {
                        this.spawnPoints.Add(vector33);
                        this.spawnPoints.Add(vector34);
                        break;
                    }
                }
                if (vector33 != Vector3.zero && vector34 != Vector3.zero)
                {
                    int num5 = Random.Range(0, (int)this.peoplePrefabs.Length);
                    GameObject gameObject1 = base.gameObject;
                    gameObject1 = Object.Instantiate(this.peoplePrefabs[num5], vector33, Quaternion.identity) as GameObject;
                    if (Physics.Raycast(gameObject1.transform.position + new Vector3(0f, 2f, 0f), -gameObject1.transform.up, out raycastHit1))
                    {
                        gameObject1.transform.position = new Vector3(gameObject1.transform.position.x, raycastHit1.point.y, gameObject1.transform.position.z);
                    }
                    gameObject1.AddComponent<PeopleController>();
                    gameObject1.GetComponent<PeopleController>().animNames = new string[] { "talk1", "talk2", "listen" };
                    gameObject1.transform.parent = this.par.transform;
                    num5 = Random.Range(0, (int)this.peoplePrefabs.Length);
                    GameObject gameObject2 = base.gameObject;
                    gameObject2 = Object.Instantiate(this.peoplePrefabs[num5], vector34, Quaternion.identity) as GameObject;
                    if (Physics.Raycast(gameObject2.transform.position + new Vector3(0f, 2f, 0f), -gameObject2.transform.up, out raycastHit2))
                    {
                        gameObject2.transform.position = new Vector3(gameObject2.transform.position.x, raycastHit2.point.y, gameObject2.transform.position.z);
                    }
                    gameObject2.AddComponent<PeopleController>();
                    gameObject2.GetComponent<PeopleController>().animNames = new string[] { "talk1", "talk2", "listen" };
                    gameObject2.transform.parent = this.par.transform;
                    gameObject2.GetComponent<PeopleController>().SetTarget(gameObject1.transform.position);
                    gameObject1.GetComponent<PeopleController>().SetTarget(gameObject2.transform.position);
                }
            }
        }
        for (int m = 0; m < num / 3; m++)
        {
            vector32 = (this.isCircle ? this.RandomCirclePosition() : this.RandomRectanglePosition());
            if (vector32 != Vector3.zero)
            {
                int num6 = Random.Range(0, (int)this.peoplePrefabs.Length);
                Vector3 vector35 = Vector3.zero;
                Vector3 vector36 = Vector3.zero;
                Vector3 vector37 = Vector3.zero;
                int num7 = 0;
                while (num7 < 100)
                {
                    for (int n = 0; n < 10; n++)
                    {
                        vector35 = vector32 + new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f));
                        if (this.IsRandomPositionFree(vector35, Vector3.zero, Vector3.zero))
                        {
                            break;
                        }
                        vector35 = Vector3.zero;
                    }
                    for (int o = 0; o < 10; o++)
                    {
                        if (vector35 == Vector3.zero)
                        {
                            vector36 = Vector3.zero;
                        }
                        else
                        {
                            vector36 = vector32 + new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f));
                            if (this.IsRandomPositionFree(vector36, vector35, Vector3.zero))
                            {
                                break;
                            }
                            vector36 = Vector3.zero;
                        }
                    }
                    for (int p = 0; p < 10; p++)
                    {
                        if (!(vector36 != Vector3.zero) || !(vector35 != Vector3.zero))
                        {
                            vector37 = Vector3.zero;
                        }
                        else
                        {
                            vector37 = vector32 + new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f));
                            if (this.IsRandomPositionFree(vector37, vector35, vector36))
                            {
                                break;
                            }
                            vector37 = Vector3.zero;
                        }
                    }
                    if (!(vector35 != Vector3.zero) || !(vector36 != Vector3.zero) || !(vector37 != Vector3.zero))
                    {
                        vector35 = Vector3.zero;
                        vector36 = Vector3.zero;
                        vector37 = Vector3.zero;
                        num7++;
                    }
                    else
                    {
                        this.spawnPoints.Add(vector35);
                        this.spawnPoints.Add(vector36);
                        this.spawnPoints.Add(vector37);
                        break;
                    }
                }
                if (vector35 != Vector3.zero)
                {
                    if (vector35 != Vector3.zero)
                    {
                        GameObject gameObject3 = Object.Instantiate(this.peoplePrefabs[num6], vector35, Quaternion.identity) as GameObject;
                        if (Physics.Raycast(gameObject3.transform.position + new Vector3(0f, 2f, 0f), -gameObject3.transform.up, out raycastHit3))
                        {
                            gameObject3.transform.position = new Vector3(gameObject3.transform.position.x, raycastHit3.point.y, gameObject3.transform.position.z);
                        }
                        gameObject3.AddComponent<PeopleController>();
                        gameObject3.GetComponent<PeopleController>().SetTarget(vector32);
                        gameObject3.GetComponent<PeopleController>().animNames = new string[] { "talk1", "talk2", "listen" };
                        gameObject3.transform.parent = this.par.transform;
                    }
                    num6 = Random.Range(0, (int)this.peoplePrefabs.Length);
                    if (vector35 != Vector3.zero)
                    {
                        GameObject gameObject4 = Object.Instantiate(this.peoplePrefabs[num6], vector36, Quaternion.identity) as GameObject;
                        if (Physics.Raycast(gameObject4.transform.position + new Vector3(0f, 2f, 0f), -gameObject4.transform.up, out raycastHit4))
                        {
                            gameObject4.transform.position = new Vector3(gameObject4.transform.position.x, raycastHit4.point.y, gameObject4.transform.position.z);
                        }
                        gameObject4.AddComponent<PeopleController>();
                        gameObject4.GetComponent<PeopleController>().SetTarget(vector32);
                        gameObject4.GetComponent<PeopleController>().animNames = new string[] { "talk1", "talk2", "listen" };
                        gameObject4.transform.parent = this.par.transform;
                    }
                    num6 = Random.Range(0, (int)this.peoplePrefabs.Length);
                    if (vector35 != Vector3.zero)
                    {
                        GameObject gameObject5 = Object.Instantiate(this.peoplePrefabs[num6], vector37, Quaternion.identity) as GameObject;
                        if (Physics.Raycast(gameObject5.transform.position + new Vector3(0f, 2f, 0f), -gameObject5.transform.up, out raycastHit5))
                        {
                            gameObject5.transform.position = new Vector3(gameObject5.transform.position.x, raycastHit5.point.y, gameObject5.transform.position.z);
                        }
                        gameObject5.AddComponent<PeopleController>();
                        gameObject5.GetComponent<PeopleController>().SetTarget(vector32);
                        gameObject5.GetComponent<PeopleController>().animNames = new string[] { "talk1", "talk2", "listen" };
                        gameObject5.transform.parent = this.par.transform;
                    }
                }
            }
        }
    }

    public void SpawnRectangleSurface()
    {
        if (this.surface != null)
        {
            Object.DestroyImmediate(this.surface);
        }
        GameObject vector3 = Object.Instantiate(this.planePrefab, base.transform.position, Quaternion.identity) as GameObject;
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