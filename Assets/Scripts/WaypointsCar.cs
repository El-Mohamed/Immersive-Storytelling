using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsCar : MonoBehaviour
{
    public GameObject[] waypoints;

    public GameObject Cop;
    public GameObject[] CopWaypoints;
    int current = 0;
    float rotSpeed;
    public float speed;
    float WPradius = (float)1;
    Animator animator;
    int lastcurrent;
    public float damping;

    bool copSpawned = false;

    private void Start()
    {
    }

    void Update()
    {
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < 6)
        {
            if (speed != 1f)
            {
                speed-=0.02f;
            }
            if (speed < 1f)
            {
                speed = 1f;
            }
        }

            if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
            {
            current++;
            if (current == waypoints.Length && !copSpawned)
            {
                StartCoroutine(SpawnCop());
            }

            if (current >= waypoints.Length)
            {
                current = waypoints.Length;
            }
        }
        else
        {
            var rotation = Quaternion.LookRotation(waypoints[current].transform.position - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        }
        if (current == waypoints.Length)
        {
            current -= 1;
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }

    IEnumerator SpawnCop()
    {
        Debug.Log("Coroutine Started");
        if (copSpawned)
            yield break;
        copSpawned = true;
        yield return new WaitForSeconds(3f);
        GameObject CopInstantiate = Instantiate(Cop);
        Vector3 pos = transform.position;
        pos.x -= 0.38f;
        pos.z -= 1.2f;
        CopInstantiate.gameObject.transform.position = pos;
        CopInstantiate.GetComponent<Waypoints>().waypoints = CopWaypoints;
    }
}
