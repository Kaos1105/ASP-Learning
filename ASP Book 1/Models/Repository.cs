using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Book_1.Models
{
    public class Repository
    {
        private static List<GuestResponese> responses = new List<GuestResponese>();
        public static IEnumerable<GuestResponese> Responses
        {
            get { return responses; }
        }
        public static void AddResponse(GuestResponese response)
        {
            responses.Add(response);
        }
    }
}
