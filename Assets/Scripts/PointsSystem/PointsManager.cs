using TMPro;
using UnityEngine;
using AudioSystem;
using System;
using UI.Menus.States;
using UI.Menus;
using System.Text;


namespace PointsSystem
{
    public class PointsManager : MonoBehaviour
    {
        private struct HittedTypeCount
        {
            public int perfects;
            public int okeys;
            public int bads;
            public int misseds;

            public HittedTypeCount(int perfects = 0, int okeys = 0, int bads = 0, int misseds = 0)
            {
                this.perfects = perfects;
                this.okeys = okeys;
                this.bads = bads;
                this.misseds = misseds;
            }

            public int GetTotalHits()
            {
                return perfects + okeys + bads;
            }
        }

        private int _points;
        private TextMeshProUGUI _pointsText;
        private HittedTypeCount _count;

        private AudioMapController _controller;
        private MapOptions _mapOptions;

        [Header("Points values")]
        [SerializeField]
        private int _perfectPoints = 300;
        [SerializeField]
        private int _okeyPoints = 200;
        [SerializeField]
        private int _badPoints = 100;

        private void Start()
        {
            _points = 0;
            _pointsText = GameObject.FindGameObjectWithTag("Points").GetComponent<TextMeshProUGUI>();
            _pointsText.gameObject.SetActive(false);
            _controller = GetComponent<AudioMapController>();

            _controller.onPlay += StartSong;
            _controller.onBeatTap += Tap;
            _controller.onBeatFail += FailTap;
            _controller.onPause += Pause;
            _controller.onUnPause += Unpause;
            _controller.onFinish += Finish;
        }

        private void Finish()
        {
            Debug.Log("FINISH CALLBACK");
            StringBuilder values = new StringBuilder();
            int maxNotes = _count.GetTotalHits() + _count.misseds;
            values.AppendLine($"Notes hited: {_count.GetTotalHits()}; Total notes: {maxNotes}");
            values.AppendLine($"Point obteined: {_points}; Max Points: {maxNotes * 300}");
            values.AppendLine($"Perfects: {_count.perfects}; Goods: {_count.okeys}");
            values.AppendLine($"Bads: {_count.bads}; Fails: {_count.misseds}");

            MenuManager.Instance.SetState(new FinishTab(values.ToString()));
        }

        private void Unpause()
        {
            _pointsText.gameObject.SetActive(true);
        }
        private void Pause()
        {
            _pointsText.gameObject.SetActive(false);
        }

        private void FailTap()
        {
            _count.misseds++;
        }

        private void StartSong(BeatMap map)
        {
            _points = 0;
            _pointsText.text = "0";
            _pointsText.gameObject.SetActive(true);
            _mapOptions = map.GetOptions();
            _count = new HittedTypeCount();
        }

        private void Tap(float beatTime, float actualTime)
        {
            float timeDifference = Mathf.Abs(beatTime - actualTime);
            if (timeDifference <= _mapOptions.perfectOffset)
            {
                _points += _perfectPoints;
                _count.perfects++;
            }
            else if (timeDifference <= _mapOptions.okeyOffset)
            {
                _points += _okeyPoints;
                _count.okeys++;
            }
            else if (timeDifference <= _mapOptions.maxOffset)
            {
                _points += _okeyPoints;
                _count.bads++;
            }

            // Uptade the text
            _pointsText.text = _points.ToString();
        }

    }
}

