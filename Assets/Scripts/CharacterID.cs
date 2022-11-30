using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CharacterID : MonoBehaviour
    {
        public string Name;
        public DateTime Birthday;
        public bool isPhotoFake = false;

        public void Initialize(string name, DateTime birthday, RenderTexture photoId, bool givenIsPhotoFake)
        {
            Name = name;
            _nameField.text = name;
            Birthday = birthday;
            _dateField.text = birthday.ToString("dd/MM/yyy");
            _photoField.texture = photoId;
            isPhotoFake = givenIsPhotoFake;

        }
        public RenderTexture PhotoRenderTexture { get; set; }
        [SerializeField]
        private TextMeshProUGUI _nameField;
        [SerializeField]
        private TextMeshProUGUI _dateField;
        [SerializeField]
        private RawImage _photoField;
    }
}
