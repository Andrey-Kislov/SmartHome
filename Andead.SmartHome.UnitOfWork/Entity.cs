namespace Andead.SmartHome.UnitOfWork
{
    public abstract class Entity
    {
        protected Entity() { }

        public int Id { get; set; }

        public override bool Equals(object other)
        {
            return Equals(other as Entity);
        }

        public bool Equals(Entity other)
        {
            if ((object)other == null)
            {
                return false;
            }

            if ((object)this == other)
            {
                return true;
            }

            if (other.Id == Id)
            {
                return other.GetType() == GetType();
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (!object.Equals(left, null))
            {
                return left.Equals(right);
            }

            return object.Equals(right, null);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
