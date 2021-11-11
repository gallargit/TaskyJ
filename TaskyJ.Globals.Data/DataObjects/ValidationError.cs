using System.Collections.Generic;

namespace TaskyJ.Globals.Data.DataObjects
{
    /// <summary>
    /// Validation error.
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="propertyName">
        /// Name of the property.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public ValidationError(string propertyName, string message)
        {
            PropertyName = propertyName;
            Message = message;
        }
    }


    /// <summary>
    /// Validation errors.
    /// </summary>
    public class ValidationErrors
    {
        /// <summary>
        /// The _errors
        /// </summary>
        private List<ValidationError> _errors;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationErrors" /> class.
        /// </summary>
        public ValidationErrors()
        {
            _errors = new List<ValidationError>();
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IList<ValidationError> Items
        {
            get { return _errors; }
        }

        /// <summary>
        /// Adds the specified property name.
        /// </summary>
        /// <param name="propertyName">
        /// Name of the property.
        /// </param>
        public void Add(string propertyName)
        {
            _errors.Add(new ValidationError(propertyName, propertyName + " is required."));
        }

        /// <summary>
        /// Adds the specified property name.
        /// </summary>
        /// <param name="propertyName">
        /// Name of the property.
        /// </param>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        public void Add(string propertyName, string errorMessage)
        {
            _errors.Add(new ValidationError(propertyName, errorMessage));
        }

        /// <summary>
        /// Adds the specified error.
        /// </summary>
        /// <param name="error">
        /// The error.
        /// </param>
        public void Add(ValidationError error)
        {
            _errors.Add(error);
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public void AddRange(IList<ValidationError> errors)
        {
            _errors.AddRange(errors);
        }

        /// <summary>
        /// Clears the items.
        /// </summary>
        internal void Clear()
        {
            _errors.Clear();
        }
    }
}
