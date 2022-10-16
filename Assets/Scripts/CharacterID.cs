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
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public RenderTexture PhotoRenderTexture { get; set; }
        [SerializeField]
        private TextMeshProUGUI _nameField;
        [SerializeField]
        private TextMeshProUGUI _dateFied;
        [SerializeField]
        private RawImage _photoField;
    }
}
