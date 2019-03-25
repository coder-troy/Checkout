namespace Checkout.Basket.Domain
{
    public class SessionId
    {
        public SessionId(string value)
        {
            Value = value;
        }
        
        private bool Equals(SessionId other)
        {
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((SessionId) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
        
        public string Value { get; }
    }
}