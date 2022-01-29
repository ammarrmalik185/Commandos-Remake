using System;
using System.Collections.Generic;
using Enemy.vision;
using Player.common.scripts;
using Player.Enums;
using UI;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.common.scripts{

    public class Enemy : MonoBehaviour{
        private EnemyState state;
        private float stateTimer;
        private float currentHealth;
        private float maxHealth;
        [SerializeField] private float enemyDamage;
        [SerializeField] private GameObject alertDisplay;
        
        [HideInInspector] public bool isDead;
        [HideInInspector] public Transform follow_target;
        public event Action onDeath = () => {};
        
        private PlayerAnimationController controller;
        private EnemyVision vision;
        private NavMeshAgent _agent;
        private Transform target;
        private IBarController healthBar;

        private const float alertTime = 1f;

        private static List<Enemy> allEnemies;

        // Start is called before the first frame update
        private void Start(){
            allEnemies ??= new List<Enemy>();
            _agent = GetComponent<NavMeshAgent>();
            controller = GetComponent<PlayerAnimationController>();
            vision = GetComponent<EnemyVision>();
            healthBar = GetComponentInChildren<IBarController>();
            
            vision.onSeeTarget += enemyIsSeen;
            //vision.onDeath += () => { Debug.Log("Death"); };
            //vision.onDetectTarget += (VisionTarget t, int s) => { Debug.Log("onDetectTarget"); };
            //vision.onTouchTarget += (VisionTarget t) => { Debug.Log("onTouchTarget"); };
            //vision.onAlert += (Vector3 t) => { Debug.Log("onAlert"); };
            
            currentHealth = 10000;
            maxHealth = 100;
            allEnemies.Add(this);
        }

        // Update is called once per frame
        private void Update(){
            stateTimer += Time.deltaTime;

            switch (state){
                case EnemyState.Idle:
                    HandleState_Idle();
                    break;
                case EnemyState.Patrol:
                    HandleState_Patrol();
                    break;
                case EnemyState.Alert:
                    HandleState_Alert();
                    break;
                case EnemyState.Chase:
                    HandleState_Chase();
                    break;
                case EnemyState.Confused:
                    HandleState_Confused();
                    break;
                case EnemyState.Wait:
                    HandleState_Wait();
                    break;
                case EnemyState.Attacking:
                    HandleState_Attack();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private void HandleState_Attack(){
            alertDisplay.SetActive(false);
            _agent.ResetPath();
            if (target == null || 
                target.gameObject.GetComponent<Player.common.scripts.Player>() == null || 
                target.gameObject.GetComponent<Player.common.scripts.Player>().isDead){
                ChangeState(EnemyState.Confused);
                return; 
            }
            target.GetComponent<Player.common.scripts.Player>()
                .takeDamage(enemyDamage * Time.deltaTime);
            controller.PlayAttackAnimation();
        }

        private void HandleState_Wait(){
            Debug.Log("Wait");
        }

        private void HandleState_Confused(){
            controller.StopAttackAnimation();
            Debug.Log("Confused");
            alertDisplay.SetActive(false);
        }

        private void HandleState_Chase(){
            _agent.SetDestination(target.position);
            controller.CurrentPlayerState = PlayerState.Walking;
            Debug.Log("Chase");
        }

        private void HandleState_Alert(){
            if (stateTimer > alertTime){
                ChangeState(EnemyState.Chase);
            }
            var lookAt = target.position;
            lookAt.y = 0;
            transform.LookAt(lookAt);
            alertDisplay.SetActive(true);
            Debug.Log("Alert");
        }

        private void HandleState_Patrol(){
            Debug.Log("Patrol");
        }

        private void HandleState_Idle(){
            Debug.Log("Idle");
        }

        public static List<Enemy> GetAll(){
            return allEnemies;
        }

        public void ChangeState(EnemyState newState){
            Debug.Log("New State: " + newState);
            stateTimer = 0f;
            state = newState;
        }

        public EnemyState GetState(){
            return state;
        }

        public float GetStateTimer(){
            return stateTimer;
        }
        
        public void takeDamage(float damage){
            if (damage >= currentHealth){
                isDead = true;
                currentHealth = 0;
                onDeath.Invoke();
                Destroy(gameObject);
            }else{
                currentHealth -= damage;
            }
            healthBar.SetValue(currentHealth, maxHealth);
        }

        public void SetAlertTarget(Vector3 alertTarget){
            // Debug.Log("SetAlertTarget");
        }

        public void Alert(Vector3 alertTarget){
            
            // Debug.Log("Alert");
        }

        public void StopMove(){
            // Debug.Log("StopMove");
        }

        public bool HasReachedTarget(){
            if (target != null)
                return Vector3.Distance(transform.position, target.position) < 1f;
            return false;
        }

        public void SetFollowTarget(GameObject followObject){
            // Debug.Log("SetFollowTarget");
        }

        public void Follow(GameObject followObject){
            // Debug.Log("Follow");
        }

        private void enemyIsSeen(VisionTarget newTarget, int distance){
            Debug.Log("enemy is seen at :" + distance);
            switch (distance){
                case 2:
                    if (state != EnemyState.Attacking && state != EnemyState.Chase){
                        ChangeState(EnemyState.Alert);
                        target = newTarget.gameObject.transform;
                    }
                    break;
                case 1:
                    ChangeState(EnemyState.Attacking);
                    target = newTarget.transform;
                    break;
            }
        }
    }
}
