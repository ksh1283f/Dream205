using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//public class WaypointManager
//{
    
namespace Valve.VR.Extras
{
    public class LaserTest : MonoBehaviour
    {
        public SteamVR_Behaviour_Pose pose;

        //public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.__actions_default_in_InteractUI;
        public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");
        public SteamVR_Action_Boolean Teleport = SteamVR_Input.GetBooleanAction("Teleport");

        public bool active = true;
        public Color color;
        public float thickness = 0.002f;
        public Color clickColor = Color.green;
        public GameObject holder;
        public GameObject pointer;
        public GameObject laserParent;
        bool isActive = false;
        public bool addRigidBody = false;
        public Transform reference;
        public event PointerEventHandler PointerIn;
        public event PointerEventHandler PointerOut;
        public event PointerEventHandler PointerClick;
        public GameObject gameManager;
        public Laser laserObj;
        [SerializeField] Transform playerTrans;
        [SerializeField] Transform playerCameraTrans;

        Transform previousContact = null;
        [SerializeField] List<Waypoint> waypoints = new List<Waypoint>();
        [SerializeField] Waypoint presentWaypoint=null;
        [SerializeField] Waypoint startWayPoint = null;
        


        private void Start()
        {
            if (pose == null)
                pose = this.GetComponent<SteamVR_Behaviour_Pose>();
            if (pose == null)
                Debug.LogError("No SteamVR_Behaviour_Pose component found on this object");

            if (interactWithUI == null)
                Debug.LogError("No ui interaction action has been set on this component.");

            waypoints = FindObjectsOfType<Waypoint>().ToList();
            laserParent.SetActive(false);
            SetCharacterOnWayPoint(startWayPoint, true);
            ShowWaypoint(presentWaypoint);
        }

        public virtual void OnPointerIn(PointerEventArgs e)
        {
            if (PointerIn != null)
                PointerIn(this, e);
        }

        public virtual void OnPointerClick(PointerEventArgs e)
        {
            Debug.LogError("oNPointerclick");
            if (PointerClick != null)
                PointerClick(this, e);
        }

        public virtual void OnPointerOut(PointerEventArgs e)
        {
            if (PointerOut != null)
                PointerOut(this, e);
        }

        void OnDrawGizmos()
        {
            float maxDistance = 100;
            RaycastHit hit;
            // Physics.Raycast (레이저를 발사할 위치, 발사 방향, 충돌 결과, 최대 거리)
            Ray raycast = new Ray(transform.position, transform.forward);
            bool isHit = Physics.Raycast(raycast, out hit, 100, (-1) - (1 << 9));

            Gizmos.color = Color.red;
            if (isHit)
            {
                Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            }
            else
            {
                Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
            }
        }

        private void Update()
        {
            if (!isActive)
            {
                isActive = true;
                this.transform.GetChild(0).gameObject.SetActive(true);
            }

            float dist = 100f;

            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit, dist, (-1) - (1 << 9));
            //if(hit.transform != null)
            //    Debug.LogError("hit tag: " + hit.transform.tag);


            

            // 텔레포트
            if (Teleport != null && Teleport.GetStateDown(pose.inputSource))
            {
                //Debug.LogError("Teleport");
                if (hit.transform == null || !hit.transform.CompareTag("WayPoint"))
                    return;

                float playerYPos = playerTrans.position.y;
                playerTrans.position = new Vector3(hit.transform.position.x, playerYPos, hit.transform.position.z);

                Waypoint hitWaypoint = hit.transform.GetComponent<Waypoint>();
                SetCharacterOnWayPoint(hitWaypoint);
            }

            if (interactWithUI != null && interactWithUI.GetStateDown(pose.inputSource))
            {
                //Debug.LogError("interactWithUI != null && interactWithUI.GetState(pose.inputSource)");
                if (bHit && hit.distance < 100f)    // 레이저에 뭔가 닿았고, 물체와 컨트롤러 사이의 거리가 100 미만일 때
                {
                    dist = hit.distance;
                   // Debug.LogError("bHit && hit.distance < 100f, dist: " + dist + " hit object: " + hit.transform.gameObject.name);
                }

                laserParent.transform.localScale = new Vector3(thickness * 15f, thickness * 15f, dist);
                if (laserParent != null && !laserParent.gameObject.activeSelf)
                    laserParent.gameObject.SetActive(true);
            }
            else if (interactWithUI != null && interactWithUI.GetStateUp(pose.inputSource))
            {
                Debug.LogError("else");
                laserParent.transform.localScale = new Vector3(thickness, thickness, dist);
                if (laserParent != null && laserParent.gameObject.activeSelf)
                {
                    laserParent.gameObject.SetActive(false);
                    laserObj.isInteracted = false;
                }
                    
            }
        }

        void ShowWaypoint(Waypoint present)
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                // 현재위치를 기준으로 앞, 뒤, 제자리만 활성화
                if(present != null && present.connectedWayPointList.Contains(waypoints[i]))
                {
                    waypoints[i].gameObject.SetActive(true);
                    waypoints[i].ActivateCollider(waypoints[i].GetHashCode() != present.GetHashCode());
                    continue;
                }

                waypoints[i].gameObject.SetActive(false);
            }
        }

        void SetCharacterOnWayPoint(Waypoint start, bool isInit = false)
        {
            if(start == null)
            {
                Debug.LogError("StartWaypoint is null");
                return;
            }

            presentWaypoint = start;
            ShowWaypoint(presentWaypoint);

            if(isInit)
                playerTrans.position = new Vector3(start.transform.position.x, playerTrans.position.y, start.transform.position.z);
        }
    }
}
