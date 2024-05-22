using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstracts
{
   public  interface IPortfolioService
    {
        void Create(Portfolio portfolio);
        void Delete(int id);
        void Update(int id, Portfolio portfolio);
        Portfolio GetPortfolio(Func<Portfolio,bool> ? func=null);
        List<Portfolio> GetAllPortfolios(Func<Portfolio, bool>? func=null);

    }
}
