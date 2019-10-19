using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

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
        [SerializeField] Teleport teleport;

        Transform previousContact = null;
        [SerializeField] PlayerBody playerBody;
        [SerializeField] List<Waypoint> waypoints = new List<Waypoint>();
        [SerializeField] Waypoint presentWaypoint = null;
        [SerializeField] Waypoint startWayPoint = null;
        [SerializeField] Waypoint limitUseLaserWaypoint = null;
        Collider laserCol;

        private void Start()
        {
            if (pose == null)
                pose = this.GetComponent<SteamVR_Behaviour_Pose>();
            if (pose == null)
                Debug.LogError("No SteamVR_Behaviour_Pose component found on this object");

            if (interactWithUI == null)
                Debug.LogError("No ui interaction action has been set on this component.");

            if (playerBody == null)
                Debug.LogError("playerBody is null");

            teleport = Valve.VR.InteractionSystem.Teleport.instance;
            if (laserObj != null)
                laserCol = laserObj.GetComponent<Collider>();

            if (teleport != null)
            {
                Debug.Log("OnPlayerTeleport is connected!");
                teleport.OnPlayerTeleport += OnPlayerTeleport;
            }
            playerBody.OnMovedPlayer = OnMovedPlayer;

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

            if (interactWithUI != null && interactWithUI.GetStateDown(pose.inputSource))
            {
                // 특정 웨이포인트에 있을때만 인터랙션이 가능한지 체크
                if (limitUseLaserWaypoint != null)
                {
                    laserCol.enabled = presentWaypoint == limitUseLaserWaypoint;
                }

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

        void OnPlayerTeleport(TeleportArea area)
        {
            Waypoint wayPoint = area.parent.GetComponent<Waypoint>();
            if (wayPoint == null)
            {
                Debug.LogError("Teleport area parent's  waypoint is null");
                return;
            }

            SetCharacterOnWayPoint(wayPoint);
        }

        void OnMovedPlayer(Waypoint wayPoint)
        {
            //SetCharacterOnWayPoint(wayPoint);
        }

        void ShowWaypoint(Waypoint present)
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                // 현재위치를 기준으로 앞, 뒤, 제자리만 활성화
                if (present != null && present.connectedWayPointList.Contains(waypoints[i]))
                {
                    waypoints[i].gameObject.SetActive(true);
                    //waypoints[i].ActivateCollider(waypoints[i].GetHashCode() != present.GetHashCode());
                    continue;
                }

                waypoints[i].gameObject.SetActive(false);
            }
        }

        void SetCharacterOnWayPoint(Waypoint start, bool isInit = false)
        {
            if (start == null)
            {
                Debug.LogError("StartWaypoint is null");
                return;
            }

            presentWaypoint = start;
            ShowWaypoint(presentWaypoint);
            float playerHeight = playerTrans.position.y;
            playerTrans.position = new Vector3(presentWaypoint.transform.position.x,
                                               playerHeight,
                                               presentWaypoint.transform.position.z);

            if (isInit)
                playerTrans.position = new Vector3(start.transform.position.x, playerTrans.position.y, start.transform.position.z);
        }
    }
}
