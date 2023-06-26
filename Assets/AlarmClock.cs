using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmClock : MonoBehaviour
{
    public float TimeLimit = 180;
    private float currentTime = 0;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Ticking");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime >= TimeLimit) {

            Timeout();
        }

        currentTime += Time.deltaTime;
        animator.SetFloat("TickingTime", currentTime/TimeLimit);
    }

    private void Timeout()
    {
        animator.Play("Timeout");
    }
}
