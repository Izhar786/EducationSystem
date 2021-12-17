using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.Models
{
    public class CandidateModel
    {
        public int CandidateId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cnic { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
