namespace GeekLearning.Events.Configuration.Queue
{
    using System;
    using System.Collections.Generic;

    public class QueueOptions : IQueueOptions
    {
        private const string MissingPropertyErrorMessage = "{0} should be defined.";

        public string ProviderName { get; set; }

        public string ProviderType { get; set; }

        public string Name { get; set; }

        public IEnumerable<IOptionError> Validate(bool throwOnError = true)
        {
            var optionsErrors = new List<OptionError>();

            if (string.IsNullOrEmpty(this.Name))
            {
                this.PushMissingPropertyError(optionsErrors, nameof(this.Name));
            }

            if (string.IsNullOrEmpty(this.ProviderName) && string.IsNullOrEmpty(this.ProviderType))
            {
                optionsErrors.Add(new OptionError
                {
                    PropertyName = "Providers",
                    ErrorMessage = $"You should set either a {nameof(this.ProviderType)} or a {nameof(this.ProviderName)} for each Store."
                });
            }

            return optionsErrors;
        }

        protected void PushMissingPropertyError(List<OptionError> optionErrors, string propertyName)
        {
            optionErrors.Add(new OptionError
            {
                PropertyName = propertyName,
                ErrorMessage = string.Format(MissingPropertyErrorMessage, propertyName)
            });
        }
    }
}
