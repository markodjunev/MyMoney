namespace MyMoney.Data.Models
{
    using System.Collections.Generic;

    using MyMoney.Data.Common.Models;

    public class OpportunityForCredit : BaseDeletableModel<int>
    {
        public OpportunityForCredit()
        {
            this.Deposits = new HashSet<Deposit>();
        }

        public string Name { get; set; }

        public virtual ICollection<Deposit> Deposits { get; set; }
    }
}
