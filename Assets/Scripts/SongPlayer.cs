using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongPlayer : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> audioClips;
    private AudioSource audioSource;

    private void Awake()
    {
        var loadedClips = Resources.LoadAll<AudioClip>("Songs");

    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        int selectedIndex =  Random.Range(0, audioClips.Count);
        var selectedClip = audioClips[selectedIndex];
        audioSource.PlayOneShot(selectedClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
