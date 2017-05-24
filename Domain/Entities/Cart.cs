using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }

        public void AddItem(Sweet sweet, int quantity)
        {
            CartLine line = lineCollection
                .Where(s => s.Sweet.SweetId == sweet.SweetId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine { Sweet = sweet, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void UpdateItem(Sweet sweet, int quantity)
        {
            CartLine line = lineCollection
            .Where(s => s.Sweet.SweetId == sweet.SweetId)
            .FirstOrDefault();

            if (line != null)
            {
                line.Quantity = quantity;
            }
        }

        public void RemoveLine(Sweet sweet)
        {
            lineCollection.RemoveAll(l => l.Sweet.SweetId == sweet.SweetId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Sweet.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }

    public class CartLine
    {
        public Sweet Sweet { get; set; }
        public int Quantity { get; set; }
    }
}
