using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class AlarmClock : MonoBehaviour
{
    public float TimeLimit = 180;
    private float currentTime = 0;
    private bool timedOut = false;

    private Animator animator;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip clip;

    public UnityEvent AlarmTimeOutEvent;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Ticking");

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime >= TimeLimit && !timedOut) {

            Timeout();
        }

        currentTime += Time.deltaTime;
        animator.SetFloat("TickingTime", currentTime/TimeLimit);
    }

    private void Timeout()
    {
        timedOut = true;
        animator.Play("Timeout");
        audioSource.Play();
        AlarmTimeOutEvent.Invoke();
    }

    public void ResetGameIfTimedOut()
    {
        if (timedOut)
        {
            SceneManager.LoadScene("VR Scene");
        }
    }
}
