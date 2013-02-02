using System;

namespace PacmanGame
{
    class PropertyChangeEventArgs : EventArgs
    {
        public string EventName { get; private set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }

        public PropertyChangeEventArgs(string eventName, object oldValue, object newValue)
        {
            EventName = eventName;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
