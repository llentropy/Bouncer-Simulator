using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IDSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject _idPrefab;
    private GameObject _currentId;
    private List<string> names;

    public void Start()
    {
        TextAsset namesFile = (TextAsset)Resources.Load("names", typeof(TextAsset));
        names = new List<string>(namesFile.text.Split('\n'));
    }
    public string GenerateName()
    {
        string name = $"{names[Random.Range(0, names.Count)]}\t{names[Random.Range(0, names.Count)]}";
        return name;
    }

    public void SpawnId(RenderTexture photoTexture)
    {
        if( _currentId != null)
        {
            Destroy(_currentId);
        }

        _currentId = Instantiate(_idPrefab, this.transform);
        var idObject = _currentId.GetComponent<CharacterID>();
        idObject.Initialize( GenerateName(), DateTime.Today, photoTexture );
        
    }
}
