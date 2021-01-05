//using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WalkPath : MonoBehaviour
{
    public GameObject[] peoplePrefabs;

    [HideInInspector]
    public List<Vector3> pathPoint = new List<Vector3>();

    [HideInInspector]
    public List<GameObject> pathPointTransform = new List<GameObject>();

    [HideInInspector]
    public Vector3[,] points;

    [HideInInspector]
    public List<Vector3> CalcPoint = new List<Vector3>();

    public int numberOfWays;

    [HideInInspector]
    public int[] pointLength = new int[10];

    public float lineSpacing;

    public float density;

    public bool loopPath;

    public WalkPath.EnumDir direction;

    public WalkPath.EnumMove _moveType;

    public float moveSpeed = 1f;

    [HideInInspector]
    public bool[] _forward;

    [HideInInspector]
    public bool isWalk;

    [HideInInspector]
    public GameObject par;

    public WalkPath()
    {
    }

    private void Awake()
    {
        this.DrawCurved(false);
    }

    public void DrawCurved(bool withDraw)
    {
        if (this.numberOfWays < 1)
        {
            this.numberOfWays = 1;
        }
        if (this.lineSpacing < 0.6f)
        {
            this.lineSpacing = 0.6f;
        }
        if (this.density < 0.1f)
        {
            this.density = 0.1f;
        }
        if (this.moveSpeed < 0.1f)
        {
            this.moveSpeed = 0.1f;
        }
        this._forward = new bool[this.numberOfWays];
        this.isWalk = (this._moveType.ToString() == "Walk" ? true : false);
        for (int i = 0; i < this.numberOfWays; i++)
        {
            if (this.direction.ToString() == "Forward")
            {
                this._forward[i] = true;
            }
            else if (this.direction.ToString() == "Backward")
            {
                this._forward[i] = false;
            }
            else if (this.direction.ToString() == "HugLeft")
            {
                if ((i + 2) % 2 != 0)
                {
                    this._forward[i] = false;
                }
                else
                {
                    this._forward[i] = true;
                }
            }
            else if (this.direction.ToString() == "HugRight")
            {
                if ((i + 2) % 2 != 0)
                {
                    this._forward[i] = true;
                }
                else
                {
                    this._forward[i] = false;
                }
            }
            else if (this.direction.ToString() == "WeaveLeft")
            {
                if (i == 1 || i == 2 || (i - 1) % 4 == 0 || (i - 2) % 4 == 0)
                {
                    this._forward[i] = false;
                }
                else
                {
                    this._forward[i] = true;
                }
            }
            else if (this.direction.ToString() == "WeaveRight")
            {
                if (i == 1 || i == 2 || (i - 1) % 4 == 0 || (i - 2) % 4 == 0)
                {
                    this._forward[i] = true;
                }
                else
                {
                    this._forward[i] = false;
                }
            }
        }
        if (this.pathPoint.Count < 2)
        {
            return;
        }
        this.points = new Vector3[this.numberOfWays + 2, this.pathPoint.Count + 2];
        this.pointLength[0] = this.pathPoint.Count + 2;
        for (int j = 0; j < this.pathPoint.Count; j++)
        {
            this.points[0, j + 1] = this.pathPointTransform[j].transform.position;
        }
        this.points[0, 0] = this.points[0, 1];
        this.points[0, this.pointLength[0] - 1] = this.points[0, this.pointLength[0] - 2];
        for (int k = 0; k < this.pointLength[0]; k++)
        {
            if (k != 0 && withDraw)
            {
                Gizmos.color = (this._forward[0] ? Color.green : Color.red);
                Gizmos.DrawLine(this.points[0, k], this.points[0, k - 1]);
            }
        }
        if (this.loopPath && withDraw)
        {
            Gizmos.color = (this._forward[0] ? Color.green : Color.red);
            Gizmos.DrawLine(this.points[0, 1], this.points[0, this.pointLength[0] - 2]);
        }
        for (int l = 1; l < this.numberOfWays; l++)
        {
            if (this.numberOfWays > 1)
            {
                if (this.loopPath)
                {
                    Vector3 vector3 = this.points[0, this.pointLength[0] - 2] - this.points[0, 1];
                    Vector3 vector31 = this.points[0, 1] - this.points[0, 2];
                    Vector3 vector32 = vector31;
                    Vector3 vector33 = vector3;
                    float single = Mathf.DeltaAngle(Mathf.Atan2(vector32.x, vector32.z) * 57.29578f, Mathf.Atan2(vector33.x, vector33.z) * 57.29578f);
                    if (l % 2 == 0)
                    {
                        vector32 = vector32.normalized * (float)((float)l * 0.5f * this.lineSpacing);
                    }
                    else if (l % 2 == 1)
                    {
                        vector32 = vector32.normalized * (float)((float)(l + 1) * 0.5f * this.lineSpacing);
                    }
                    vector32 = Quaternion.Euler(0f, 90f + single / 2f, 0f) * vector32;
                    Vector3 vector34 = Vector3.zero;
                    if (l % 2 == 1)
                    {
                        vector34 = this.points[0, 1] - vector32;
                    }
                    else if (l % 2 == 0)
                    {
                        vector34 = this.points[0, 1] + vector32;
                    }
                    vector34.y = this.points[0, 1].y;
                    this.points[l, 1] = vector34;
                    this.points[l, 0] = vector34;
                    Vector3 vector35 = this.points[0, this.pointLength[0] - 2] - this.points[0, 1];
                    Vector3 vector36 = this.points[0, this.pointLength[0] - 3] - this.points[0, this.pointLength[0] - 2];
                    Vector3 vector37 = vector36;
                    Vector3 vector38 = vector35;
                    float single1 = Mathf.DeltaAngle(Mathf.Atan2(vector37.x, vector37.z) * 57.29578f, Mathf.Atan2(vector38.x, vector38.z) * 57.29578f);
                    if (l % 2 == 0)
                    {
                        vector37 = vector37.normalized * (float)((float)l * 0.5f * this.lineSpacing);
                    }
                    else if (l % 2 == 1)
                    {
                        vector37 = vector37.normalized * (float)((float)(l + 1) * 0.5f * this.lineSpacing);
                    }
                    vector37 = Quaternion.Euler(0f, 90f + single1 / 2f, 0f) * vector37;
                    Vector3 vector39 = Vector3.zero;
                    if (l % 2 == 1)
                    {
                        vector39 = this.points[0, this.pointLength[0] - 2] - vector37;
                    }
                    else if (l % 2 == 0)
                    {
                        vector39 = this.points[0, this.pointLength[0] - 2] + vector37;
                    }
                    vector39.y = this.points[0, this.pointLength[0] - 2].y;
                    this.points[l, this.pointLength[0] - 2] = vector39;
                    this.points[l, this.pointLength[0] - 1] = vector39;
                }
                else
                {
                    Vector3 vector310 = this.points[0, 2] - this.points[0, 1];
                    Vector3 vector311 = vector310;
                    vector311 = Quaternion.Euler(0f, -90f, 0f) * vector311;
                    if (l % 2 == 0)
                    {
                        vector311 = vector311.normalized * (float)((float)l * 0.5f * this.lineSpacing);
                    }
                    else if (l % 2 == 1)
                    {
                        vector311 = vector311.normalized * (float)((float)(l + 1) * 0.5f * this.lineSpacing);
                    }
                    Vector3 vector312 = Vector3.zero;
                    if (l % 2 == 1)
                    {
                        vector312 = this.points[0, 1] - vector311;
                    }
                    else if (l % 2 == 0)
                    {
                        vector312 = this.points[0, 1] + vector311;
                    }
                    vector312.y = this.points[0, 1].y;
                    this.points[l, 0] = vector312;
                    this.points[l, 1] = vector312;
                    Vector3 vector313 = this.points[0, this.pointLength[0] - 3] - this.points[0, this.pointLength[0] - 2];
                    Vector3 vector314 = vector313;
                    vector314 = Quaternion.Euler(0f, 90f, 0f) * vector314;
                    if (l % 2 == 0)
                    {
                        vector314 = vector314.normalized * (float)((float)l * 0.5f * this.lineSpacing);
                    }
                    else if (l % 2 == 1)
                    {
                        vector314 = vector314.normalized * (float)((float)(l + 1) * 0.5f * this.lineSpacing);
                    }
                    Vector3 vector315 = Vector3.zero;
                    if (l % 2 == 1)
                    {
                        vector315 = this.points[0, this.pointLength[0] - 2] - vector314;
                    }
                    else if (l % 2 == 0)
                    {
                        vector315 = this.points[0, this.pointLength[0] - 2] + vector314;
                    }
                    vector315.y = this.points[0, this.pointLength[0] - 2].y;
                    this.points[l, this.pointLength[0] - 2] = vector315;
                    this.points[l, this.pointLength[0] - 1] = vector315;
                }
                for (int m = 2; m < this.pointLength[0] - 2; m++)
                {
                    Vector3 vector316 = this.points[0, m] - this.points[0, m + 1];
                    Vector3 vector317 = this.points[0, m - 1] - this.points[0, m];
                    Vector3 vector318 = vector317;
                    Vector3 vector319 = vector316;
                    float single2 = Mathf.DeltaAngle(Mathf.Atan2(vector318.x, vector318.z) * 57.29578f, Mathf.Atan2(vector319.x, vector319.z) * 57.29578f);
                    if (l % 2 == 0)
                    {
                        vector318 = vector318.normalized * (float)((float)l * 0.5f * this.lineSpacing);
                    }
                    else if (l % 2 == 1)
                    {
                        vector318 = vector318.normalized * (float)((float)(l + 1) * 0.5f * this.lineSpacing);
                    }
                    vector318 = Quaternion.Euler(0f, 90f + single2 / 2f, 0f) * vector318;
                    Vector3 vector320 = Vector3.zero;
                    if (l % 2 == 1)
                    {
                        vector320 = this.points[0, m] - vector318;
                    }
                    else if (l % 2 == 0)
                    {
                        vector320 = this.points[0, m] + vector318;
                    }
                    vector320.y = this.points[0, m].y;
                    this.points[l, m] = vector320;
                    if (withDraw)
                    {
                        Gizmos.color = (this._forward[l] ? Color.green : Color.red);
                        Gizmos.DrawLine(this.points[l, m - 1], this.points[l, m]);
                    }
                }
                if (withDraw)
                {
                    Gizmos.color = (this._forward[l] ? Color.green : Color.red);
                    Gizmos.DrawLine(this.points[l, this.pointLength[0] - 2], this.points[l, this.pointLength[0] - 3]);
                }
                if (withDraw && this.loopPath)
                {
                    Gizmos.color = (this._forward[l] ? Color.green : Color.red);
                    Gizmos.DrawLine(this.points[l, 1], this.points[l, this.pointLength[0] - 2]);
                }
            }
        }
    }

    public Vector3 getNextPoint(int w, int index)
    {
        return this.points[w, index];
    }

    public int getPointsTotal(int w)
    {
        return this.pointLength[w];
    }

    public Vector3 getStartPoint(int w)
    {
        return this.points[w, 1];
    }

    public void OnDrawGizmos()
    {
        this.DrawCurved(true);
    }

    public void SpawnOnePeople(int w, bool forward, float mSpeed)
    {
        string str;
        int num = Random.Range(0, (int)this.peoplePrefabs.Length);
        GameObject gameObject = base.gameObject;
        gameObject = (forward ? Object.Instantiate(this.peoplePrefabs[num], this.points[w, 1], Quaternion.identity) as GameObject : Object.Instantiate(this.peoplePrefabs[num], this.points[w, this.pointLength[0] - 2], Quaternion.identity) as GameObject);
        MovePath movePath = gameObject.AddComponent<MovePath>();
        gameObject.transform.parent = this.par.transform;
        movePath.walkPath = base.gameObject;
        str = (!this.isWalk ? "run" : "walk");
        if (forward)
        {
            movePath.MyStart(w, 1, str, this.loopPath, forward, mSpeed);
            gameObject.transform.LookAt(this.points[w, 2]);
            return;
        }
        movePath.MyStart(w, this.pointLength[0] - 2, str, this.loopPath, forward, mSpeed);
        gameObject.transform.LookAt(this.points[w, this.pointLength[0] - 3]);
    }

    public void SpawnPeople()
    {
        int num;
        string str;
        if (this.par == null)
        {
            this.par = new GameObject();
            this.par.transform.parent = base.gameObject.transform;
            this.par.name = "people";
        }
        num = (this.loopPath ? this.pointLength[0] - 1 : this.pointLength[0] - 2);
        for (int i = 0; i < this.numberOfWays; i++)
        {
            float single = this.moveSpeed + Random.Range(this.moveSpeed * -0.15f, this.moveSpeed * 0.15f);
            for (int j = 1; j < num; j++)
            {
                bool flag = false;
                if (this.direction.ToString() == "Forward")
                {
                    flag = true;
                }
                else if (this.direction.ToString() == "Backward")
                {
                    flag = false;
                }
                else if (this.direction.ToString() == "HugLeft")
                {
                    flag = ((i + 2) % 2 != 0 ? false : true);
                }
                else if (this.direction.ToString() == "HugRight")
                {
                    flag = ((i + 2) % 2 != 0 ? true : false);
                }
                else if (this.direction.ToString() == "WeaveLeft")
                {
                    flag = (i == 1 || i == 2 || (i - 1) % 4 == 0 || (i - 2) % 4 == 0 ? false : true);
                }
                else if (this.direction.ToString() == "WeaveRight")
                {
                    flag = (i == 1 || i == 2 || (i - 1) % 4 == 0 || (i - 2) % 4 == 0 ? true : false);
                }
                Vector3 vector3 = Vector3.zero;
                vector3 = (!this.loopPath || j != num - 1 ? this.points[i, j + 1] - this.points[i, j] : this.points[i, 1] - this.points[i, num]);
                float single1 = vector3.magnitude;
                int num1 = (int)(this.density / 5f * single1);
                if (num1 < 1)
                {
                    num1 = 1;
                }
                float single2 = single1 / (float)num1;
                float single3 = 0f;
                for (int k = 0; k < num1; k++)
                {
                    float single4 = 0f;
                    single4 = (k != 0 ? Random.Range(single3, (float)(k + 1) * single2 - 0.5f) : Random.Range(single3, single2 - 0.5f));
                    single3 = single4 + 0.5f;
                    int num2 = Random.Range(0, (int)this.peoplePrefabs.Length);
                    GameObject gameObject = base.gameObject;
                    gameObject = (!flag ? Object.Instantiate(this.peoplePrefabs[num2], this.points[i, j] + (vector3 * (single4 / single1)), Quaternion.identity) as GameObject : Object.Instantiate(this.peoplePrefabs[num2], this.points[i, j] + (vector3 * (single4 / single1)), Quaternion.identity) as GameObject);
                    MovePath movePath = gameObject.AddComponent<MovePath>();
                    gameObject.transform.parent = this.par.transform;
                    movePath.walkPath = base.gameObject;
                    str = (!this.isWalk ? "run" : "walk");
                    movePath.MyStart(i, j, str, this.loopPath, flag, single);
                }
            }
        }
    }

    public enum EnumDir
    {
        Forward,
        Backward,
        HugLeft,
        HugRight,
        WeaveLeft,
        WeaveRight
    }

    public enum EnumMove
    {
        Walk,
        Run
    }
}