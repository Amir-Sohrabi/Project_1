using _Scripts.Core.Base;
using UnityEngine;

namespace _Scripts.Core.Characters
{
    public class Amir : BaseCharacter
    {
        private Vector3 _moveDirection;

        private void Update()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            _moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        }

        private void FixedUpdate()
        {
            if (_moveDirection == Vector3.zero)
            {
                return;
            }

            transform.position += _moveDirection * Speed * Time.fixedDeltaTime;
        }
    }
}
