namespace MyMoney.Data.Models
{
    using System.Collections.Generic;

    using MyMoney.Data.Common.Models;

    public class TypeOfInterest : BaseDeletableModel<int>
    {
        public TypeOfInterest()
        {
            this.Deposits = new HashSet<Deposit>();
        }

        public string Name { get; set; }

        public virtual ICollection<Deposit> Deposits { get; set; }
    }
}
