using _Scripts.Core.Base;
using UnityEngine;

namespace _Scripts.Core.Characters
{
    public class Amir : BaseCharacter
    {
        private Vector3 _moveDirection;

        private void Update()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            _moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        }

        private void FixedUpdate()
        {
            if (_moveDirection == Vector3.zero)
            {
                return;
            }

            Vector3 movement = _moveDirection * (Speed * Time.fixedDeltaTime);
            transform.position += movement;
        }
    }
}
