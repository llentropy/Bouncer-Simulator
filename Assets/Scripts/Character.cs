using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public RenderTexture idTexture { get; private set; }
    [SerializeField]
    private Vector2Int idResolution = new Vector2Int(50, 50);
    [SerializeField]
    private float fakePhotoProbability = 0.9f;

    public bool IsPhotoFake { get; private set; }

    private bool IsExiting = false;

    private NavMeshAgent playerNavMesh;
    private Animator playerAnimator;
    private CharacterID characterID;
    void Awake()
    {
        idTexture = new RenderTexture(idResolution.x, idResolution.y, 16);

        IsPhotoFake = Random.Range(0.0f, 1.0f) < fakePhotoProbability;
        if (IsPhotoFake)
        {
            StartCoroutine(ReasignMaterials());
        }

    }

    private IEnumerator ReasignMaterials()
    {
        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<CharacterSpawner>().AssignCharacterMaterial(this.gameObject);
    }

    private void Start()
    {
        StartCoroutine(WaitForSnapshot());
    }

    private IEnumerator WaitForSnapshot()
    {
        yield return new WaitForSeconds(0.5f);
        Snapshot();
        playerNavMesh = GetComponent<NavMeshAgent>();
        playerNavMesh.destination = Waypoints.Instance.wayPointsList[1].position;
        playerAnimator = GetComponent<Animator>();
        playerAnimator.SetBool("IsWalking", true);
    }

    private void Update()
    {
        if(playerNavMesh == null)
        {
            return;
        }
        if (playerNavMesh.velocity == Vector3.zero)
        {
            playerAnimator.SetBool("IsWalking", false);
        }
        else
        {
            playerAnimator.SetBool("IsWalking", true);

        }
    }

    public void CanEnter(bool characterEnters)
    {
        IsExiting = true;
        if (characterEnters)
        {
            playerNavMesh.SetDestination(Waypoints.Instance.wayPointsList[2].position);
            playerAnimator.SetInteger("Happiness", 1);

        }
        else
        {
            playerNavMesh.SetDestination(Waypoints.Instance.wayPointsList[0].position);
            playerAnimator.SetInteger("Happiness", -1);
        }
        if(characterID != null)
        {
            Destroy(characterID.gameObject);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Desk")
        {
            characterID = IDSpawner.Instance.SpawnId(this.gameObject);
        }

        if ((other.gameObject.name == "Start" || other.gameObject.name == "Finish") && IsExiting)
        {
            if (!ScoreManager.Instance.gameEnded)
            {
                CharacterSpawner.Instance.SpawnNewCharacter();
            }
            Destroy(this.gameObject);
        }
    }

    

    void Snapshot()
    {
        var characterCamera = this.transform.GetComponentInChildren<Camera>();
        // If RenderTexture.active is set any rendering goes into this RenderTexture
        // instead of the GameView
        RenderTexture.active = idTexture;
        characterCamera.targetTexture = idTexture;

        // renders into the renderTexture
        characterCamera.Render();

        // reset the RenderTexture.active so nothing else is rendered into our RenderTexture      
        RenderTexture.active = null;
        characterCamera.targetTexture = null;
        Destroy(characterCamera.gameObject);
        var rawImage = this.transform.GetComponentInChildren<RawImage>();
        rawImage.texture = idTexture;

    }
}
