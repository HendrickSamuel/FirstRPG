using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;

namespace PlayerScripts
{
    public class PlayerClueScript : MonoBehaviour
    {
        [SerializeField]
        internal EventSystem _events;

        [SerializeField] private Sprite _question;
        [SerializeField] private Sprite _exclamation;
        [SerializeField] private Sprite _empty;
        [SerializeField] private Sprite[] _confused;

        private bool isActive = false;

        private SpriteRenderer _renderer;

        // Start is called before the first frame update
        void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.enabled = isActive;
            _events.OnClueSet += _events_OnClueSet;
        }

        private void _events_OnClueSet(object sender, EventSystem.ClueEventArgs e)
        {
            if (e.clue == 0)
                DisableClue();
            else
            if (e.clue == 1)
                SetQuestionMark();
            else
            if (e.clue == 2)
                SetExclamationMark();
            else
            if (e.clue == 3)
                SetEmptyMark();
        }

        private void DisableClue()
        {
            _renderer.enabled = false;
            isActive = false;
        }

        private void SetQuestionMark()
        {
            _renderer.sprite = _question;
            _renderer.enabled = !isActive;
            isActive = !isActive;
        }

        private void SetExclamationMark()
        {
            _renderer.sprite = _exclamation;
            _renderer.enabled = !isActive;
            isActive = !isActive;
        }

        private void SetEmptyMark()
        {
            _renderer.sprite = _empty;
            _renderer.enabled = !isActive;
            isActive = !isActive;
        }
    }
}
