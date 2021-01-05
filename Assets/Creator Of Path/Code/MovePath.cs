//using System;
using UnityEngine;

[System.Serializable]
public class MovePath : MonoBehaviour
{
    [SerializeField]
    public Vector3 startPos;

    [SerializeField]
    public Vector3 finishPos;

    [SerializeField]
    public int w;

    [SerializeField]
    public int targetPoint;

    [SerializeField]
    public int targetPointsTotal;

    [SerializeField]
    public string animName;

    [SerializeField]
    public float moveSpeed;

    [SerializeField]
    public bool loop;

    [SerializeField]
    public bool forward;

    [SerializeField]
    public GameObject walkPath;

    public MovePath()
    {
    }

    public void MyStart(int _w, int _i, string anim, bool _loop, bool _forward, float _moveSpeed)
    {
        this.forward = _forward;
        this.moveSpeed = _moveSpeed;
        WalkPath component = this.walkPath.GetComponent<WalkPath>();
        this.w = _w;
        this.targetPointsTotal = component.getPointsTotal(0) - 2;
        this.loop = _loop;
        this.animName = anim;
        if (!this.loop)
        {
            if (!this.forward)
            {
                this.targetPoint = _i;
                this.finishPos = component.getNextPoint(this.w, _i);
                return;
            }
            this.targetPoint = _i + 1;
            this.finishPos = component.getNextPoint(this.w, _i + 1);
            return;
        }
        if (_i >= this.targetPointsTotal || _i <= 0)
        {
            if (this.forward)
            {
                this.targetPoint = 1;
                this.finishPos = component.getNextPoint(this.w, 1);
                return;
            }
            this.targetPoint = this.targetPointsTotal;
            this.finishPos = component.getNextPoint(this.w, this.targetPointsTotal);
            return;
        }
        if (!this.forward)
        {
            this.targetPoint = _i;
            this.finishPos = component.getNextPoint(this.w, _i);
            return;
        }
        this.targetPoint = _i + 1;
        this.finishPos = component.getNextPoint(this.w, _i + 1);
    }

    private void Start()
    {
        Vector3 vector3 = new Vector3(this.finishPos.x, base.transform.position.y, this.finishPos.z);
        base.transform.LookAt(vector3);
        base.GetComponent<Animator>().CrossFade(this.animName, 0.1f, 0, Random.Range(0f, 1f));
        if (this.animName == "walk")
        {
            base.GetComponent<Animator>().speed = this.moveSpeed * 1.2f;
            return;
        }
        if (this.animName == "run")
        {
            base.GetComponent<Animator>().speed = this.moveSpeed / 3f;
        }
    }

    private void Update()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(base.transform.position + new Vector3(0f, 2f, 0f), -base.transform.up, out raycastHit))
        {
            this.finishPos.y = raycastHit.point.y;
            base.transform.position = new Vector3(base.transform.position.x, raycastHit.point.y, base.transform.position.z);
        }
        Vector3 vector3 = new Vector3(this.finishPos.x, base.transform.position.y, this.finishPos.z);
        WalkPath component = this.walkPath.GetComponent<WalkPath>();
        if (Vector3.Distance(base.transform.position, this.finishPos) < 0.2f && this.animName == "walk" && (this.loop || !this.loop && this.targetPoint > 0 && this.targetPoint < this.targetPointsTotal))
        {
            if (!this.forward)
            {
                vector3 = (this.targetPoint <= 0 ? component.getNextPoint(this.w, this.targetPointsTotal) : component.getNextPoint(this.w, this.targetPoint - 1));
                vector3.y = base.transform.position.y;
            }
            else
            {
                vector3 = (this.targetPoint >= this.targetPointsTotal ? component.getNextPoint(this.w, 0) : component.getNextPoint(this.w, this.targetPoint + 1));
                vector3.y = base.transform.position.y;
            }
        }
        if (Vector3.Distance(base.transform.position, this.finishPos) < 0.5f && this.animName == "run" && (this.loop || !this.loop && this.targetPoint > 0 && this.targetPoint < this.targetPointsTotal))
        {
            if (!this.forward)
            {
                vector3 = (this.targetPoint <= 0 ? component.getNextPoint(this.w, this.targetPointsTotal) : component.getNextPoint(this.w, this.targetPoint - 1));
                vector3.y = base.transform.position.y;
            }
            else
            {
                vector3 = (this.targetPoint >= this.targetPointsTotal ? component.getNextPoint(this.w, 0) : component.getNextPoint(this.w, this.targetPoint + 1));
                vector3.y = base.transform.position.y;
            }
        }
        Vector3 vector31 = vector3 - base.transform.position;
        if (vector31 != Vector3.zero)
        {
            Quaternion quaternion = Quaternion.identity;
            if (this.animName == "walk")
            {
                quaternion = Quaternion.Lerp(base.transform.rotation, Quaternion.LookRotation(vector31), Time.deltaTime * 4f * this.moveSpeed);
            }
            else if (this.animName == "run")
            {
                quaternion = Quaternion.Lerp(base.transform.rotation, Quaternion.LookRotation(vector31), Time.deltaTime * 1.3f * this.moveSpeed);
            }
            base.transform.rotation = quaternion;
        }
        if (base.transform.position != this.finishPos)
        {
            base.transform.position = Vector3.MoveTowards(base.transform.position, this.finishPos, Time.deltaTime * 1f * this.moveSpeed);
            return;
        }
        if (base.transform.position == this.finishPos && this.forward)
        {
            if (this.targetPoint != this.targetPointsTotal)
            {
                this.targetPoint++;
                this.finishPos = component.getNextPoint(this.w, this.targetPoint);
                return;
            }
            if (this.targetPoint == this.targetPointsTotal)
            {
                if (this.loop)
                {
                    this.finishPos = component.getStartPoint(this.w);
                    this.targetPoint = 0;
                    return;
                }
                component.SpawnOnePeople(this.w, this.forward, this.moveSpeed);
                Object.Destroy(base.gameObject);
                return;
            }
        }
        else if (base.transform.position == this.finishPos && !this.forward)
        {
            if (this.targetPoint > 0)
            {
                this.targetPoint--;
                this.finishPos = component.getNextPoint(this.w, this.targetPoint);
                return;
            }
            if (this.targetPoint == 0)
            {
                if (this.loop)
                {
                    this.finishPos = component.getNextPoint(this.w, this.targetPointsTotal);
                    this.targetPoint = this.targetPointsTotal;
                    return;
                }
                component.SpawnOnePeople(this.w, this.forward, this.moveSpeed);
                Object.Destroy(base.gameObject);
            }
        }
    }
}