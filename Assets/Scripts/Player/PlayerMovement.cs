using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [FormerlySerializedAs("Controller")] 
        public CharacterController2D controller;
        
        [FormerlySerializedAs("Animator")] 
        public Animator animator;
        
        [FormerlySerializedAs("RigidBody")] 
        public Rigidbody2D rigidBody;
        
        [FormerlySerializedAs("RunSpeed")] 
        public float runSpeed = 40f;
        
        private float _horizontalMove = 0f;
        private bool _jump;
        
        private void Update()
        {
            _horizontalMove = Input.GetAxis("Horizontal") * runSpeed;
            //Input.GetAxis("Horizontal") retorna 1 ou -1
            //RunSpeed é multiplicado para definir a velocidade do personagem
            
            //Dessa forma passando a velocidade para o animador, ele poderá animar corretamente o sprite
            animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));
            animator.SetFloat("VerticalSpeed", rigidBody.velocity.y);
               
            //Verifica se o botão de pular foi acionado
            if (Input.GetButtonDown("Jump"))
            {
                _jump = true; //Seta a vatiavel de pular para true
                animator.SetBool("IsJumping", true);
            }
        }

        //Método especifico para tratar física, pois é chamado de forma ordenada
        private void FixedUpdate()
        {
            //Time.fixedDeltaTime retorna o tempo desde a ultima vez que FixedUpdate foi chamado.
            //Deixa a movimentação consistente
            controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
            _jump = false;
        }
    }
}