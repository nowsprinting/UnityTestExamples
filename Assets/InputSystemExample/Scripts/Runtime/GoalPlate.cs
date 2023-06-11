// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;

namespace InputSystemExample
{
    [RequireComponent(typeof(Collider))]
    public class GoalPlate : MonoBehaviour
    {
        public bool IsGoal { get; private set; } = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GetComponent<MeshRenderer>().material.color = Color.red;
                IsGoal = true;
            }
        }
    }
}
