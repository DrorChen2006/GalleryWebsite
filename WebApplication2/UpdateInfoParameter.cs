using System;

namespace WebApplication2
{
    public class UpdateInfoParameter
    {
        private string fieldName;
        private string newValue;
        private bool shouldUpdate;

        public UpdateInfoParameter(string fieldName, string newValue)
        {
            this.fieldName = fieldName;
            this.newValue = newValue;
            shouldUpdate = !string.IsNullOrEmpty(newValue);
        }

        public string GetFieldName()
        {
            return fieldName;
        }

        public string GetNewValue()
        {
            return newValue;
        }

        public bool ShouldUpdate()
        {
            return shouldUpdate;
        }
    }
}