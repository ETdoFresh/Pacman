using System;

namespace PacmanGame
{
    class PropertyChangeEventArgs : EventArgs
    {
        public string EventName { get; private set; }
        public object OldValue { get; private set; }
        public object NewValue { get; private set; }

        public PropertyChangeEventArgs(string eventName, object oldValue, object newValue)
        {
            EventName = eventName;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
