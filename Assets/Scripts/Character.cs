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

    private NavMeshAgent playerNavMesh;
    private Animator playerAnimator;
    private CharacterID characterID;
    void Awake()
    {
        idTexture = new RenderTexture(idResolution.x, idResolution.y, 16);
        playerNavMesh = GetComponent<NavMeshAgent>();
        playerNavMesh.destination = Waypoints.Instance.wayPointsList[1].position;
        playerAnimator = GetComponent<Animator>();
        playerAnimator.SetBool("IsWalking", true);
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
        Snapshot();
    }

    private void Update()
    {
        if (playerNavMesh.velocity == Vector3.zero)
        {
            playerAnimator.SetBool("IsWalking", false);
        }  else
        {
            playerAnimator.SetBool("IsWalking", true);

        }
    }

    public void CanEnter(bool characterEnters)
    {
        if (characterEnters)
        {
            playerNavMesh.SetDestination(Waypoints.Instance.wayPointsList[2].position);
        }
        else
        {
            playerNavMesh.SetDestination(Waypoints.Instance.wayPointsList[0].position);
        }
        Destroy(characterID.gameObject);
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Desk")
        {
            characterID = IDSpawner.Instance.SpawnId(this.gameObject);
        } else
        {
            if(this.transform.position != other.transform.position)
            {
            }
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
