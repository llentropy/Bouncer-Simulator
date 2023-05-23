using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _characterPrefab;
 
    public GameObject _currentCharacter;

    private List<Material> _characterMaterials;

    private static readonly string[] _bodyPartNames = { "Arm", "Leg", "Head", "Body" };

    private static CharacterSpawner _instance;

    public static CharacterSpawner Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CharacterSpawner();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void Start()
    {
        var loadedMaterials = Resources.LoadAll("Materials", typeof(Material));
        _characterMaterials = new List<Material>();
        foreach (var material in loadedMaterials)
        {
            _characterMaterials.Add(material as Material);
        }
        SpawnNewCharacter();
    }

    public void SpawnNewCharacter(bool isFake = false)
    {

        StartCoroutine(WaitForSpawn(isFake));
        
    }

    private IEnumerator WaitForSpawn(bool isFake = false)
    {
        yield return new WaitForEndOfFrame();
        Vector3 characterPosition = Waypoints.Instance.wayPointsList[0].position;
        Transform newParent = this.transform;
        if (isFake)
        {
            newParent = null;
            characterPosition = Vector3.one * 800000000;
        }
        var newCharacter = Instantiate(_characterPrefab, newParent);
        newCharacter.transform.position = characterPosition;

        AssignCharacterMaterial(newCharacter);
        if (!isFake)
        {
            _currentCharacter = newCharacter;
        }
    }


    public void AssignCharacterMaterial(GameObject character)
    {
        var bodyParts = new List<Transform>();
        foreach (Transform child in character.transform)
        {
            bodyParts.Add(child);
        }
        if (bodyParts == null)
        {
            return;
        }
        foreach(var bodyPartName in _bodyPartNames)
        {
            var correspondingBodyParts = bodyParts.Where(bp => bp.name.Contains(bodyPartName));
            int randomIndex = Random.Range(0, _characterMaterials.Count);
            var chosenMaterial = _characterMaterials[randomIndex];
            foreach(var bodyPart in correspondingBodyParts)
            {
                var renderer = bodyPart.GetComponent<Renderer>();
                renderer.material = chosenMaterial;
            }
        }
    }
}
