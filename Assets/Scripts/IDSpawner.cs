using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class IDSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject _idPrefab;
    public GameObject _currentId;
    [SerializeField]
    private DateTime baseDate = new DateTime(1985, 01, 01);
   
    [SerializeField]
    private CharacterSpawner _characterSpawner;

    private List<string> names;

    private static IDSpawner _instance;

    public static IDSpawner Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new IDSpawner();
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

    public void Start()
    {
        
        TextAsset namesFile = (TextAsset)Resources.Load("names", typeof(TextAsset));
        names = new List<string>(namesFile.text.Split('\n'));
    }
    public string GenerateName()
    {
        string name = $"{names[Random.Range(0, names.Count)]}\n{names[Random.Range(0, names.Count)]}";
        return name;
    }

    public DateTime GenerateBirthday()
    {
        var randomDays = Random.Range(0, 6000);
        var date = baseDate.AddDays(randomDays);
        return date;
    }

    public CharacterID SpawnId(GameObject character)
    {
        var texture = character.GetComponent<Character>().idTexture;
        var characterCamera = character.transform.GetComponentInChildren<Camera>();

        if (_currentId != null)
        {
            Destroy(_currentId);
        }

        _currentId = Instantiate(_idPrefab, this.transform);
        var idObject = _currentId.GetComponent<CharacterID>();
        idObject.Initialize(GenerateName(), GenerateBirthday(), texture, character.GetComponent<Character>().IsPhotoFake);
        return idObject;
    }
}
