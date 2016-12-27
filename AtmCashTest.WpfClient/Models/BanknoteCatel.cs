using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmCashTest.WpfClient.Models
{
    using System.ComponentModel.DataAnnotations;

    using AtmCashTest.Core;

    using Catel.Data;

    public class BanknoteCatel : ModelBase, IBanknote
    {
        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
        public int Id
        {
            get { return GetValue<int>(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        /// <summary>
        /// Register the Id property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IdProperty = RegisterProperty("Id", typeof(int), null);

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public int Nominal
        {
            get { return GetValue<int>(NominalProperty); }
            set { SetValue(NominalProperty, value); }
        }

        /// <summary>
        /// Register the Nominal property so it is known in the class.
        /// </summary>
        public static readonly PropertyData NominalProperty = RegisterProperty("Nominal", typeof(int), null);

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Nominal must be positive")]
        public int Count
        {
            get { return GetValue<int>(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        /// <summary>
        /// Register the Count property so it is known in the class.
        /// </summary>
        public static readonly PropertyData CountProperty = RegisterProperty("Count", typeof(int), null);

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
