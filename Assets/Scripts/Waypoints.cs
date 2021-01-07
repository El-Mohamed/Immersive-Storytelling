using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public GameObject[] waypoints;
    int current = 0;
    float rotSpeed;
    public float speed;
    float WPradius = (float)0.1;
    Animator animator;
    int lastcurrent;

    public GameObject Cell;
    Animator CellAnimation;

    bool unlocked = false;

    bool cellUnlocked = false;

    bool geluid = false;


    public float damping;

    private void Start()
    {
        animator = GetComponent<Animator>();

        CellAnimation = Cell.GetComponent<Animator>();

        
    }

    void Update()
    {
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            animator.SetFloat("Vertical", 0);
            current++;
            if(current == waypoints.Length && !unlocked)
            {
                StartCoroutine(UnlockAnimation());
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

            //SmoothLook(waypoints[current].transform.position);
            //transform.LookAt(waypoints[current].transform);
            animator.SetFloat("Vertical", (float)0.2);
        }
        if (current == waypoints.Length)
        {
            current -= 1;
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);

    }

    //void SmoothLook(Vector3 newDirection)
    //{
    //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDirection), Time.deltaTime);
    //}

    IEnumerator UnlockAnimation()
    {
        Debug.Log("Coroutine Started");
        if (unlocked)
            yield break;
        
        animator.SetBool("Unlock", true);
        CellAnimation.SetBool("UnlockCell", true);
        unlocked = true;
        StartCoroutine(UnlockCellAnim());
        yield return new WaitForSeconds(4.5f);
        
        animator.SetBool("Unlock", false);
    }


    IEnumerator UnlockCellAnim()
    {
        if (cellUnlocked)
            yield break;
        cellUnlocked = true;
        yield return new WaitForSeconds(2f);
        Cell.GetComponent<AudioSource>().Play();
        CellAnimation.SetTrigger("OpenClose");
        //animator.SetBool("Unlock", false);
    }
}
