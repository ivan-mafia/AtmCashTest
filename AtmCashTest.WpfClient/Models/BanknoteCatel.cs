// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BanknoteCatel.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the BanknoteCatel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.WpfClient.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AtmCashTest.Core;

    using Catel.Data;

    /// <summary>
    /// The banknote for <c>catel</c> and <c>mvvm</c>.
    /// </summary>
    public class BanknoteCatel : ModelBase, IBanknote
    {
        #region Public Fields
        /// <summary>
        /// Register the Id property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IdProperty = RegisterProperty("Id", typeof(int));

        /// <summary>
        /// Register the Nominal property so it is known in the class.
        /// </summary>
        public static readonly PropertyData NominalProperty = RegisterProperty("Nominal", typeof(int));

        /// <summary>
        /// Register the Count property so it is known in the class.
        /// </summary>
        public static readonly PropertyData CountProperty = RegisterProperty("Count", typeof(int));
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public int Id
        {
            get { return this.GetValue<int>(IdProperty); }
            set { this.SetValue(IdProperty, value); }
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public int Nominal
        {
            get { return this.GetValue<int>(NominalProperty); }
            set { this.SetValue(NominalProperty, value); }
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        // ReSharper disable once LocalizableElement
        [Range(0, int.MaxValue, ErrorMessage = "Nominal must be positive")]
        public int Count
        {
            get { return this.GetValue<int>(CountProperty); }
            set { this.SetValue(CountProperty, value); }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// The validate fields.
        /// </summary>
        /// <param name="validationResults">
        /// The validation results.
        /// </param>
        protected override void ValidateFields(List<IFieldValidationResult> validationResults)
        {
            if (this.Count < 0)
            {
                var validMsg = (string)System.Windows.Application.Current.FindResource("CountValidation");
                validationResults.Add(FieldValidationResult.CreateError("Count", validMsg));
            }
        }
        #endregion
    }
}
