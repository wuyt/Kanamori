using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

namespace Kanamori.Dbg
{
    public class DbgNavigationController : MonoBehaviour
    {
        public GameObject uiBack;
        public GameObject uiNav;
        private GameController game;
        public Transform player;
        public ShowChildObject[] staticObjects;
        public Transform ssMap;
        public Transform blueBox;
        public Transform svContent;
        public Transform targetPrefab;
        public SelectButton selectButton;
        public Transform roadPrefab;
        public NavMeshSurface surface;
        public NavMeshAgent agent;
        private NavMeshPath path;
        private Transform target;
        public LineRenderer lineRenderer;
        public float refresh;

        void Start()
        {
            game = FindObjectOfType<GameController>();
            SetStaticObject();
            LoadObjects();
            LoadTarget();
            LoadRoad();
            BakePath();
            ShowNav();
        }
        private void SetStaticObject()
        {
            foreach (var item in staticObjects)
            {
                item.SetVisible((item.transform.position - player.position).magnitude <= 2);
            }
        }
        public void Close()
        {
            uiBack.SetActive(true);
            uiNav.SetActive(false);
        }
        public void ShowNav()
        {
            uiBack.SetActive(false);
            uiNav.SetActive(true);
        }
        public void Back()
        {
            game.BackDbgMenu();
        }
        private void LoadObjects()
        {
            var list = game.LoadDynamicObject();
            foreach (var item in list)
            {
                var dynamicObject = JsonUtility.FromJson<DynamicObject>(item);
                var tf = Instantiate(blueBox, ssMap);
                tf.localPosition = dynamicObject.position;
                tf.localEulerAngles = dynamicObject.rotation;
                tf.localScale = dynamicObject.scale;
                var obj = tf.GetComponent<ShowSelfObject>();
                obj.SetVisible((tf.position - player.position).magnitude <= 2);
            }
        }

        private void LoadTarget()
        {
            var list = game.LoadKeyPoint();
            foreach (var item in list)
            {
                KeyPoint point = JsonUtility.FromJson<KeyPoint>(item);

                if (point.pointType == 0)
                {
                    var target = Instantiate(targetPrefab, ssMap);
                    target.localPosition = point.position;
                    target.GetComponent<ShowChildObject>().SetVisible(false);
                    var btn = Instantiate(selectButton, svContent);
                    btn.GetComponentInChildren<Text>().text = point.name;
                    btn.target = target;
                }
            }
        }

        private void LoadRoad()
        {
            var list = game.LoadRoad();
            foreach (var item in list)
            {
                var road = JsonUtility.FromJson<Road>(item);
                var tfRoad = Instantiate(roadPrefab, ssMap.Find("Roads"));

                tfRoad.localPosition = (road.startPosition + road.endPosition) / 2;
                tfRoad.LookAt(road.startPosition);
                tfRoad.localScale = new Vector3(0.02f, 1f, (road.endPosition - road.startPosition).magnitude * 0.1f + 0.2f);
            }
        }

        private void BakePath()
        {
            agent.enabled = false;
            surface.BuildNavMesh();
            path = new NavMeshPath();
        }

        private void DisplayPath()
        {
            agent.transform.position = player.position;
            agent.enabled = true;
            agent.CalculatePath(target.position, path);
            lineRenderer.positionCount = path.corners.Length;
            lineRenderer.SetPositions(path.corners);
            agent.enabled = false;
        }

        public void SelectButtonClicked(Transform btn)
        {
            CancelInvoke("DisplayPath");
            target = btn.GetComponent<SelectButton>().target;
            InvokeRepeating("DisplayPath", 0, refresh);
            Close();
        }
    }
}

