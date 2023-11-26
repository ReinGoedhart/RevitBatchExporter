using RevitBatchExporter.Frontend.Models;
using RevitBatchExporter.Frontend.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Stores
{
    public class ErrorMessagesStore
    {
        public event Action errorMessagesChanged;

        private List<string> _currentErrorMessages;
        public List<string> CurrentErrorMessages
        {
            get => _currentErrorMessages;
            set
            {
                _currentErrorMessages = value;
                OnCurrentErrorMessagesChanged();
            }
        }
        public ErrorMessagesStore()
        {
            CurrentErrorMessages = new List<string>();
        }
        public void AddErrorMessage(string errorMessage)
        {
            CurrentErrorMessages.Add(errorMessage);
            OnCurrentErrorMessagesChanged();
        }

        public void ClearErrorMessages()
        {
            CurrentErrorMessages.Clear();
        }

        public void OnCurrentErrorMessagesChanged()
        {
            errorMessagesChanged?.Invoke();
        }
    }
}
