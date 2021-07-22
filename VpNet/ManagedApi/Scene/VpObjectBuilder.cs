using System;

namespace VpNet
{
    /// <summary>
    ///     Constructs an object to be created, loaded, or modified.
    /// </summary>
    public sealed class VpObjectBuilder
    {
        private readonly bool _isObjectLoad;
        private DateTimeOffset? _time;
        private User _owner;

        internal VpObjectBuilder(bool isObjectLoad = false)
        {
            _isObjectLoad = isObjectLoad;
        }

        /// <summary>
        ///     Gets or sets the new action of the object.
        /// </summary>
        /// <value>The new action of the object, or <see langword="null" /> to leave unchanged..</value>
        public string Action { get; set; }

        /// <summary>
        ///     Gets or sets the new rotation angle of the object.
        /// </summary>
        /// <value>The new rotation angle of the object, or <see langword="null" /> to leave unchanged..</value>
        public double? Angle { get; set; }

        /// <summary>
        ///     Gets or sets the new data of the object.
        /// </summary>
        /// <value>The new data of the object, or <see langword="null" /> to leave unchanged.</value>
        public byte[] Data { get; set; }

        /// <summary>
        ///     Gets or sets the new description of the object.
        /// </summary>
        /// <value>The new description of the object, or <see langword="null" /> to leave unchanged..</value>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the new model of the object.
        /// </summary>
        /// <value>The new model of the object, or <see langword="null" /> to leave unchanged..</value>
        public string Model { get; set; }

        /// <summary>
        ///     Gets or sets the new owner of the object.
        /// </summary>
        /// <value>The owner of the object, or <see langword="null" /> to leave unchanged.</value>
        /// <exception cref="NotSupportedException">An attempt was made to modify the owner outside of an object load.</exception>
        public User Owner
        {
            get => _owner;
            set
            {
                if (!_isObjectLoad)
                {
                    throw new NotSupportedException("Object owner can only be set for an object load.");
                }

                _owner = value;
            }
        }

        /// <summary>
        ///     Gets or sets the new position of the object.
        /// </summary>
        /// <value>The new position of the object, or <see langword="null" /> to leave unchanged..</value>
        public Vector3? Position { get; set; }

        /// <summary>
        ///     Gets or sets the new rotation of the object.
        /// </summary>
        /// <value>The new rotation of the object, or <see langword="null" /> to leave unchanged..</value>
        public Vector3? Rotation { get; set; }

        /// <summary>
        ///     Gets or sets the new time of the object.
        /// </summary>
        /// <value>The new time of the object, or <see langword="null" /> to leave unchanged.</value>
        /// <exception cref="NotSupportedException">An attempt was made to modify the time outside of an object load.</exception>
        public DateTimeOffset? Time
        {
            get => _time;
            set
            {
                if (!_isObjectLoad)
                {
                    throw new NotSupportedException("Object time can only be set for an object load.");
                }

                _time = value;
            }
        }

        /// <summary>
        ///     Gets or sets the new type of the object.
        /// </summary>
        /// <value>The new type of the object, or <see langword="null" /> to leave unchanged.</value>
        public int? Type { get; set; }

        public VpObjectBuilder WithAction(string value)
        {
            Action = value;
            return this;
        }

        /// <summary>
        ///     Sets the new angle of the object.
        /// </summary>
        /// <param name="value">The new angle, or <see langword="null" /> to leave unchanged.</param>
        /// <returns>The current builder instance.</returns>
        public VpObjectBuilder WithAngle(double? value)
        {
            Angle = value;
            return this;
        }

        /// <summary>
        ///     Sets the new data of the object.
        /// </summary>
        /// <param name="value">The new data, or <see langword="null" /> to leave unchanged.</param>
        /// <returns>The current builder instance.</returns>
        public VpObjectBuilder WithData(byte[] value)
        {
            Data = value;
            return this;
        }

        /// <summary>
        ///     Sets the new description of the object.
        /// </summary>
        /// <param name="value">The new description, or <see langword="null" /> to leave unchanged.</param>
        /// <returns>The current builder instance.</returns>
        public VpObjectBuilder WithDescription(string value)
        {
            Description = value;
            return this;
        }

        /// <summary>
        ///     Sets the new model of the object.
        /// </summary>
        /// <param name="value">The new model, or <see langword="null" /> to leave unchanged.</param>
        /// <returns>The current builder instance.</returns>
        public VpObjectBuilder WithModel(string value)
        {
            Model = value;
            return this;
        }

        /// <summary>
        ///     Sets the new owner of the object.
        /// </summary>
        /// <param name="value">The new owner, or <see langword="null" /> to leave unchanged.</param>
        /// <returns>The current builder instance.</returns>
        /// <exception cref="NotSupportedException">An attempt was made to modify the owner outside of an object load.</exception>
        public VpObjectBuilder WithOwner(User value)
        {
            Owner = value;
            return this;
        }

        /// <summary>
        ///     Sets the new position of the object.
        /// </summary>
        /// <param name="value">The new position, or <see langword="null" /> to leave unchanged.</param>
        /// <returns>The current builder instance.</returns>
        public VpObjectBuilder WithPosition(Vector3? value)
        {
            Position = value;
            return this;
        }

        /// <summary>
        ///     Sets the new rotation of the object.
        /// </summary>
        /// <param name="value">The new rotation, or <see langword="null" /> to leave unchanged.</param>
        /// <returns>The current builder instance.</returns>
        public VpObjectBuilder WithRotation(Vector3? value)
        {
            Rotation = value;
            return this;
        }

        /// <summary>
        ///     Sets the new time of the object.
        /// </summary>
        /// <param name="value">The new time, or <see langword="null" /> to leave unchanged.</param>
        /// <returns>The current builder instance.</returns>
        /// <exception cref="NotSupportedException">An attempt was made to modify the time outside of an object load.</exception>
        public VpObjectBuilder WithTime(DateTimeOffset? value)
        {
            Time = value;
            return this;
        }

        /// <summary>
        ///     Sets the new type of the object.
        /// </summary>
        /// <param name="value">The new type, or <see langword="null" /> to leave unchanged.</param>
        /// <returns>The current builder instance.</returns>
        public VpObjectBuilder WithType(int? value)
        {
            Type = value;
            return this;
        }
    }
}
