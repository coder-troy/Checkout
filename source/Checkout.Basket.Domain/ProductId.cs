namespace Checkout.Basket.Domain
{
    public class ProductId
    {
        public ProductId(string value)
        {
            Value = value;
        }

        private bool Equals(ProductId other)
        {
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ProductId) obj);
        }

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }
        
        public string Value { get; }

        public override string ToString()
        {
            return Value;
        }
    }
}