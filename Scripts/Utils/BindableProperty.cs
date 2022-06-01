using System;

namespace YanYeek
{
    public class BindableProperty<T> where T : IEquatable<T>
    {
        private T mValue;

        public T Value
        {
            get => mValue;
            set
            {
                if (!mValue.Equals(value))
                {
                    mValue = value;
                    OnValueChanged(value);
                }
            }
        }

        public event Action<T> OnValueChanged = delegate { };
    }

}