namespace MyMoney.Data.Models
{
    using System.Collections.Generic;

    using MyMoney.Data.Common.Models;

    public class TypeOfPaymentOfInterest : BaseDeletableModel<int>
    {
        public TypeOfPaymentOfInterest()
        {
            this.Deposits = new HashSet<Deposit>();
        }

        public string Name { get; set; }

        public virtual ICollection<Deposit> Deposits { get; set; }
    }
}
