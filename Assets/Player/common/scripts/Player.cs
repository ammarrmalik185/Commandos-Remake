using System.Collections.Generic;
using EPOOutline;
using Inventory.items.basicPistol;
using Managers.GameManager;
using Player.characters;
using Player.Enums;
using UI;
using UnityEngine;
using UnityEngine.AI;

namespace Player.common.scripts{
    public class Player : MonoBehaviour{

        public Dictionary<PlayerModifier, float> movementSpeeds;

        public Inventory.scripts.Inventory inventory;
        
        public float currentHealth;
        public float maxHealth;
        [HideInInspector] public bool isDead;

        internal ICharacterData characterData;

        private float playerDamage;
        private PlayerAnimationController controller;
        private Outlinable outline;

        public GameObject playerSelectionIndicator;

        [HideInInspector] public bool isSelected;
        [HideInInspector] public bool isAttacking;
        private bool isShownOnUi;
        public bool IsShownOnUi{
            get => isShownOnUi;
            set{
                if (value && !isShownOnUi){
                    SetHealthToUI();
                    UIManager.getInstance()
                        .characterIcon
                        .sprite = characterData.displayImage;
                }
                isShownOnUi = value;
            }
        }
        private Transform attackTarget;

        private bool isMoving;
        private Vector3 movementTarget;

        private bool hover;

        private NavMeshAgent _agent;
        private float attackRange;
        private RaycastHit _hit;

        private void Start(){
            movementSpeeds = new Dictionary<PlayerModifier, float>{
                {PlayerModifier.Normal, 3.5f},
                {PlayerModifier.Crouch, 2f},
                {PlayerModifier.Prone, 1f},
            };
            
            inventory = new Inventory.scripts.Inventory(this);
            characterData = GetComponent<ICharacter>().CharacterData;
            
            inventory.PutItemInInventory(1, 1, new basicPistol());
            
            attackRange = 10;
            playerDamage = 10;
            outline = GetComponent<Outlinable>();
            _agent = GetComponent<NavMeshAgent>();
            controller = GetComponent<PlayerAnimationController>();
            // currentHealth = 100;
            // maxHealth = 100;
        }
        
        private void Update(){
            if (!GameManager.getInstance().canPlayerMove) return;

            if (hover)
                SetOnHover(false);
            // controller.speed = (currentHealth / maxHealth + 1) * 2;

            if (isMoving){
                _agent.speed = movementSpeeds[controller.CurrentPlayerModifier];
                _agent.SetDestination(movementTarget);
                if (Vector3.Distance(transform.position, movementTarget) <= 0.5){
                    isMoving = false;
                    _agent.ResetPath();
                }
                controller.CurrentPlayerState = PlayerState.Walking;
            }
            else{
                controller.CurrentPlayerState = PlayerState.Idle;
            }

            if (!isAttacking) return;
            var position = transform.position;
            Physics.Raycast(position, (attackTarget.position - position), out _hit, attackRange + 1);

            if (Vector3.Distance(attackTarget.position, transform.position) <= attackRange && _hit.transform.CompareTag("Enemy")){
                _agent.SetDestination(transform.position);
                controller.PlayAttackAnimation();
                var lookAt = attackTarget.position;
                lookAt.y = 0;
                transform.LookAt(lookAt);
                attackTarget.GetComponent<Enemy.common.scripts.Enemy>().takeDamage(playerDamage * Time.deltaTime);
                if (!attackTarget.GetComponent<Enemy.common.scripts.Enemy>().isDead) return;
                isAttacking = false;
                controller.StopAttackAnimation();
            }else{
                _agent.SetDestination(attackTarget.position);
            }
        }

        public void MoveToDestination(Vector3 movement){
            movementTarget = movement;
            isMoving = true;
        }
    
        public void AttackTarget(Transform enemy){
            Debug.Log("Attack target set");
            isAttacking = true;
            attackTarget = enemy;
        }
    
        public void takeDamage(float damage){
            if (damage >= currentHealth){
                isDead = true;
                currentHealth = 0;
                Destroy(gameObject);
            }else{
                currentHealth -= damage;
            }
            if (IsShownOnUi)
                SetHealthToUI();
        }

        private void SetHealthToUI(){
            UIManager.getInstance()
                .playerHealthBar
                .SetValue(currentHealth, maxHealth);
        }
        
        public void SetSelection(bool selection){
            isSelected = selection;
            playerSelectionIndicator.SetActive(selection);
            if (selection)
                SetSelected();
            else
                SetUnSelected();
        }

        public void SetOnHover(bool isHovering){
            if (isHovering)
                SetHover();
            else{
                UnHover();
            }
        }

        private void SetHover(){
            if (isSelected) return;
            hover = true;
            outline.FrontParameters.Color = Color.yellow;
            outline.BackParameters.Color = Color.yellow;
        }

        private void UnHover(){
            
            if (isSelected) return;
            hover = false;
            outline.FrontParameters.Color = new Color(0, 0, 0, 0);
            outline.BackParameters.Color = Color.cyan;
        }

        private void SetSelected(){
            outline.FrontParameters.Color = Color.blue;
            outline.BackParameters.Color = Color.blue;
        }

        private void SetUnSelected(){
            outline.FrontParameters.Color = new Color(0, 0, 0, 0);
            outline.BackParameters.Color = Color.cyan;
        }
    }
}
