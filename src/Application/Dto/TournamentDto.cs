using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class TournamentDto
    {
        public string TournamentName { get; set; }
        public DateTimeOffset TournamentStartDate { get; set; }

        public DateTimeOffset TournamentEndDate { get; set; }
    }
}
