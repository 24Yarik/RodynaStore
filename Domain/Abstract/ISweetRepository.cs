using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface ISweetRepository
    {
        IEnumerable<Sweet> Sweets { get; }
        void SaveSweet(Sweet sweet);
    }
}
