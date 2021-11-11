using System.Runtime.Serialization;

namespace TaskyJ.Globals.Data.DataObjects
{
    /// <summary>
    /// The base class for domain entities.
    /// </summary>
    [DataContract]
    public abstract class BaseEntity : IValidatable
    {
        [DataMember]
        public int ID { get; set; }

        /// <summary>
        /// The validation errors
        /// </summary>
        private readonly ValidationErrors _validationErrors;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity" /> class.
        /// </summary>
        protected BaseEntity()
        {
            _validationErrors = new ValidationErrors();
        }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsValid
        {
            get
            {
                _validationErrors.Clear();
                Validate();
                return ValidationErrors.Items.Count == 0;
            }
        }

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        /// <value>
        /// The validation errors.
        /// </value>
        public virtual ValidationErrors ValidationErrors
        {
            get { return _validationErrors; }
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        protected virtual void Validate()
        {
        }

        public virtual void CopyFrom(BaseEntity source)
        {
            if (source != null)
            {
                ID = source.ID;
            }
        }

        public virtual bool Equals(BaseEntity source)
        {
            return (ID == source.ID);
        }
    }
}
