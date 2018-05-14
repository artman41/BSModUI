﻿using System;
using BSModUI.Misc;
using IllusionPlugin;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRUI;
using Toggle = UnityEngine.UI.Toggle;

namespace BSModUI.UI
{
    /**
     * ModUI Framework implementation ideas:
     *
     * - Mod references BSModUI.dll
     * - Inherits ModGui baseclass
     * - Calls Setup UI base function with name and ver
     * - Toggle is added by default with override for custom page
     * - 
     */

    class ModSelection
    {
        public Toggle Toggle;
        public TextMeshProUGUI Text;
        public string PrettyName;

        public ModSelection(Toggle toggle, TextMeshProUGUI text, string prettyName)
        {
            Toggle = toggle;
            Text = text;
            PrettyName = prettyName;
        }
    }

    class Mod
    {
        public string Version { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public IPlugin GetPlugin { get; set; }
        // TODO: prop for Image
    }

    class ModMenuMasterViewController : VRUINavigationController
    {
        private ModMenuUi _modMenuUi = FindObjectOfType<ModMenuUi>();


        private Button _backButton;

        public ModsListViewController _modsListViewController;
        public SongDetailViewController _modDetailsViewController;
        public bool _modDetailsPushed = false;

        private ModMenuMasterViewController _modList;

        protected override void DidActivate()
        {
            // Find ModMenuUi object and create back button on screen

            _modMenuUi = FindObjectOfType<ModMenuUi>();
            try
            {

                _backButton = _modMenuUi.CreateBackButton(rectTransform);
                (_backButton.transform as RectTransform).anchorMin = new Vector2(0, 0);
                (_backButton.transform as RectTransform).anchorMin = new Vector2(0, 0);
                (_backButton.transform as RectTransform).anchoredPosition = new Vector2(0, 0.5f);
                _backButton.onClick.AddListener(delegate ()
                {
                    DismissModalViewController(null, false);
                });
                if (_modsListViewController == null)
                {
                    _modsListViewController = _modMenuUi.CreateViewController<ModsListViewController>();
                }


                _modsListViewController.rectTransform.anchorMin = new Vector2(0.3f, 0f);
                _modsListViewController.rectTransform.anchorMax = new Vector2(0.7f, 1f);

                PushViewController(_modsListViewController, true);

                Utils.Log("View Controller activated");



                base.DidActivate();
            }
            catch (Exception ex)
            {
                Utils.Log(ex.StackTrace + ex.Message, Utils.Severity.Error);
            }
        }


        protected override void DidDeactivate()
        {
            _modDetailsPushed = false;
        }




    }
}