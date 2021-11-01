using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace menus
{
    public class MainMenuUIController : GlobalMenuUIController
    {
        [Header("Canvas")]
        [SerializeField] Canvas MainMenuCanvas = null;
        [SerializeField] Canvas InstructionsCanvas = null;
        [SerializeField] Canvas OptionsCanvas = null;

        private void Awake()
        {
            MainMenuCanvas.gameObject.SetActive(true);
            InstructionsCanvas.gameObject.SetActive(false);
            OptionsCanvas.gameObject.SetActive(false);
        }
    }
}
