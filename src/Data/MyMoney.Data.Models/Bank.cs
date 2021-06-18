namespace MyMoney.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MyMoney.Data.Common.Models;

    public class Bank : BaseDeletableModel<string>
    {
        public Bank()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Deposits = new HashSet<Deposit>();
        }

        public string Name { get; set; }

        public virtual ICollection<Deposit> Deposits { get; set; }
    }
}
