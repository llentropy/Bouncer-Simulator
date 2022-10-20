using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _characterPrefab;
    [SerializeField]
    private IDSpawner _idSpawner;
 
    public GameObject _currentCharacter;

    private List<Transform> _characterBodyParts;

    private List<Material> _characterMaterials;

    private static readonly string[] _bodyPartNames = { "Arm", "Leg", "Head", "Body" };
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

    public void SpawnNewCharacter()
    {
        if(_currentCharacter != null)
        {
            Destroy(_currentCharacter);
        }
        _currentCharacter = Instantiate(_characterPrefab, this.transform);
        _characterBodyParts = new List<Transform>();
        foreach(Transform child in _currentCharacter.transform)
        {
            _characterBodyParts.Add(child);
        }
        AssignCharacterMaterial();
        var camera = _currentCharacter.transform.GetComponentInChildren<Camera>();
      
        _idSpawner.SpawnId(camera);
    }

    private void AssignCharacterMaterial()
    {
        if(_currentCharacter == null)
        {
            return;
        }
        foreach(var bodyPartName in _bodyPartNames)
        {
            var correspondingBodyParts = _characterBodyParts.Where(bp => bp.name.Contains(bodyPartName));
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
