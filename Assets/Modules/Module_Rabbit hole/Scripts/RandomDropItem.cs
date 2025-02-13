using System.Collections;
using System.Collections.Generic;
using UnityEngine;
      [RequireComponent(typeof(Rigidbody))]
public class RandomDropItem : MonoBehaviour
{

    [Tooltip("Probability percent (0-100%) that will be the threshold for the object to spawn (A random number is generated, if this is lower than the threshold, the object will spawn)")][Range(0, 100)] public float spawnProbability;
    [Header("IMPULSE PARAMETERS")]
    [Tooltip("Value of initial spawn impulse")][Range(0, 5000)] public float spawnImpulse;
    [Tooltip("percent variation (0-100%) that will affect to impulse")][Range(0, 100)] public float impulseVariancePercent;
    [Header("TORQUE PARAMETERS")]
   [Tooltip("If true, override 'initialTorque' value with a random normalized Vector3")] public bool randomTorque;
    [Tooltip("Torque direction. Alway will be reseting to normalized vector with values between 0 and 1") ]public Vector3 initialTorque;
    public float torqueImpulse;

    private void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Collider>().enabled = false;
    }
}
