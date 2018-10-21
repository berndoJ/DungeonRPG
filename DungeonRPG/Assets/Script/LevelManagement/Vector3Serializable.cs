using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DungeonRPG.LevelManagement
{
    [Serializable]
    public struct Vector3Serializable
    {
        /// <summary>
        /// X
        /// </summary>
        public float x;

        /// <summary>
        /// Y
        /// </summary>
        public float y;

        /// <summary>
        /// Z
        /// </summary>
        public float z;

        /// <summary>
        /// Creates a new value of this struct.
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="z">Z</param>
        public Vector3Serializable(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Creates a new value of this struct.
        /// </summary>
        /// <param name="vector">The vector.</param>
        public Vector3Serializable(Vector3 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
            this.z = vector.z;
        }

        /// <summary>
        /// Gets the vector 3 for this value.
        /// </summary>
        /// <returns>The vector 3.</returns>
        public Vector3 GetVector3()
        {
            return new Vector3(this.x, this.y, this.z);
        }
    }
}
