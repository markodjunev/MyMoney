namespace MyMoney.Data.Models
{
    using System.Collections.Generic;

    using MyMoney.Data.Common.Models;

    public class TypeOfDeposit : BaseDeletableModel<int>
    {
        public TypeOfDeposit()
        {
            this.Deposits = new HashSet<Deposit>();
        }

        public string Name { get; set; }

        public virtual ICollection<Deposit> Deposits { get; set; }
    }
}
