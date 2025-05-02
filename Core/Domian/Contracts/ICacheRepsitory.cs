   using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Contracts
{
    public interface ICacheRepsitory
    {
        Task SetAsync(string key, object value, TimeSpan duration);
        Task<string?> GetAsync(string key);
    }
}
