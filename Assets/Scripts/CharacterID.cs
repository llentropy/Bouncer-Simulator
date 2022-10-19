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
    internal class CharacterID : MonoBehaviour
    {

        public void Initialize(string name, DateTime birthday, RenderTexture photoId)
        {
            _nameField.text = name;
            _dateField.text = birthday.ToString("dd/MM/yyy");
            _photoField.texture = photoId;

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
