//using System;
using UnityEngine;

public class PeopleController : MonoBehaviour
{
    [HideInInspector]
    public float timer;

    [HideInInspector]
    public string[] animNames;

    public PeopleController()
    {
    }

    public void SetAnimClip(string animName)
    {
        base.GetComponent<Animator>().CrossFade(animName, 0.1f, 0, Random.Range(0f, 1f));
    }

    public void SetTarget(Vector3 _target)
    {
        Vector3 vector3 = new Vector3(_target.x, base.transform.position.y, _target.z);
        base.transform.LookAt(vector3);
    }

    private void Start()
    {
        this.Tick();
    }

    private void Tick()
    {
        this.timer = 0f;
        int num = Random.Range(0, (int)this.animNames.Length);
        this.SetAnimClip(this.animNames[num]);
        this.timer = Random.Range(3f, 5f);
    }

    private void Update()
    {
        if (this.timer < 0f)
        {
            this.Tick();
            return;
        }
        this.timer -= Time.deltaTime;
    }
}