using System;

namespace VpNet
{
    /// <summary>
    ///     Represents a structure which encapsulates a fully qualified location; in that it contains both a world and a position.
    /// </summary>
    public struct Location : IEquatable<Location>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Location" /> struct.
        /// </summary>
        /// <param name="worldName">The name of the world.</param>
        /// <param name="position">The position.</param>
        public Location(string worldName, Vector3 position)
            : this(new World(worldName), position)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Location" /> struct.
        /// </summary>
        /// <param name="worldName">The name of the world.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        public Location(string worldName, Vector3 position, Vector3 rotation)
            : this(new World(worldName), position, rotation)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Location" /> struct.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="position">The position.</param>
        public Location(World world, Vector3 position)
            : this(world, position, Vector3.Zero)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Location" /> struct.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        public Location(World world, Vector3 position, Vector3 rotation)
        {
            World = world;
            Position = position;
            Rotation = rotation;
        }

        /// <summary>
        ///     Gets or sets the world.
        /// </summary>
        /// <value>The world.</value>
        public World World { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public Vector3 Position { get; set; }
        
        /// <summary>
        ///     Gets or sets the rotation.
        /// </summary>
        /// <value>The rotation.</value>
        public Vector3 Rotation { get; set; }

        /// <inheritdoc />
        public bool Equals(Location other) => Equals(World, other.World) && Position.Equals(other.Position);

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is Location other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((World != null ? World.GetHashCode() : 0) * 397) ^ Position.GetHashCode();
            }
        }

        public static bool operator ==(Location left, Location right) => left.Equals(right);

        public static bool operator !=(Location left, Location right) => !left.Equals(right);
    }
}
